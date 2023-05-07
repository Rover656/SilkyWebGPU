﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.SourceGenerators
{
    [Generator]
    public class WGPUExtensionSourceGenerator : ISourceGenerator
    {
        /// <summary>
        /// The name of the generated class
        /// </summary>
        private const string ClassName = "MethodExtensions";
        
        /// <summary>
        /// The interface which provides a method to get a disposable managed chainable.
        /// </summary>
        private const string ChainableSafetyInterface = "IManagedChainable";

        /// <summary>
        /// Suffix applied to the unmanaged versions of chainable objects.
        /// </summary>
        private const string ChainableUnmanagedSuffix = "Unmanaged";

        /// <summary>
        /// Setting this to false obliterates any raw pointers. They will all be replaced by the native pointer type, or hidden to allow other overloads space.
        /// This basically forces the full use of the abstraction layer.
        /// TODO: Decide how this should be configured :P
        /// </summary>
        private const bool AllowStrayPointers = true; // TODO: Needs to be enabled to be able to set null on userdatum nicely.

        /// <summary>
        /// List of Objects we want to consider as "Classes"
        /// </summary>
        public static readonly string[] Objects =
        {
            "Adapter", "BindGroup", "BindGroupLayout", "Buffer", "CommandBuffer", "CommandEncoder",
            "ComputePassEncoder", "ComputePipeline", "Device", "Instance", "PipelineLayout", "QuerySet", "Queue",
            "RenderBundleEncoder", "RenderPassEncoder", "RenderPipeline", "Sampler", "ShaderModule", "Surface",
            "SwapChain", "Texture", "TextureView"
        };

        /// <summary>
        /// Chainable structs we are going to allow safe use of in the API.
        /// </summary>
        private static readonly string[] ChainableRoots =
        {
            "BufferDescriptor", "CommandBufferDescriptor", "CommandEncoderDescriptor", "InstanceDescriptor",
            "PipelineLayoutDescriptor", "QuerySetDescriptor", "RenderBundleDescriptor", "RenderBundleEncoderDescriptor",
            "RequestAdapterOptions", "SamplerDescriptor", "SurfaceDescriptor", "SwapChainDescriptor",
            "TextureViewDescriptor", "BindGroupDescriptor", "ComputePassDescriptor", "ProgrammableStageDescriptor",
            "ShaderModuleDescriptor", "TextureDescriptor", "BindGroupLayoutDescriptor", "ComputePipelineDescriptor",
            "DeviceDescriptor", "RenderPassDescriptor", "RenderPipelineDescriptor"
        };

        public void Execute(GeneratorExecutionContext context)
        {
            var webGpu = context.Compilation.GetTypeByMetadataName($"{Constants.WebGpuNS}.WebGPU");

            var outputWriter = new StringBuilder($@"// <auto-generated/>
// Temporary while in development
using {Constants.ExtensionNS};
using {Constants.ExtensionNS}.Chain;

using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace {Constants.ExtensionNS};

public static partial class {ClassName}
{{
");

            foreach (var member in webGpu.GetMembers())
            {
                if (member is IMethodSymbol method)
                {
                    ProcessMethod(outputWriter, method);
                }
            }

            outputWriter.AppendLine("}");

            context.AddSource($"{ClassName}.generated.cs", outputWriter.ToString());
        }

        private void ProcessMethod(StringBuilder outputWriter, IMethodSymbol method)
        {
            // Parameterless methods are ignored.
            if (method.Parameters.Length <= 0)
                return;

            // Only expand methods which are potentially one of the Objects we're looking for
            if (method.Parameters[0].Type.Kind != SymbolKind.PointerType)
                return;

            // Get the object type
            var objectType = (method.Parameters[0].Type as IPointerTypeSymbol).PointedAtType;

            // TODO: Right now we only generate object methods.
            if (!Objects.Contains(objectType.Name))
                return;

            // Determine new method name
            var methodName = method.Name.Replace(objectType.Name, "");

            // Process parameters, set flags for special outputs
            var chainablePresent = false;
            var generatePrimitive = true;
            foreach (var parameter in method.Parameters)
            {
                if (IsChainableParameter(parameter))
                    chainablePresent = true;

                if (!AllowStrayPointers)
                {
                    if (parameter.Type is IPointerTypeSymbol && GetParameterType(parameter).Equals(parameter.Type.ToString()))
                    {
                        generatePrimitive = false;
                    }
                }
            }

            // Write 1:1 translation
            if (generatePrimitive)
            {
                if (method.ReturnType is IPointerTypeSymbol pointerReturnType)
                {
                    if (pointerReturnType.PointedAtType.ToString() != "void")
                    {
                        WriteObjectMethod(outputWriter, method, objectType, methodName);
                    }
                    else
                    {
                        // Love for those few void pointers out there
                        WritePointerMethod(outputWriter, method, objectType, methodName);
                    }
                }
                else if (method.ReturnsVoid)
                {
                    WriteVoidMethod(outputWriter, method, objectType, methodName);
                }
                else
                {
                    WritePlainMethod(outputWriter, method, objectType, methodName);
                }
            }

            // Chainable extras
            if (chainablePresent)
            {
                if (method.ReturnType is IPointerTypeSymbol pointerReturnType)
                {
                    if (pointerReturnType.PointedAtType.ToString() != "void")
                    {
                        WriteObjectMethod(outputWriter, method, objectType, methodName, true);
                    }
                    else
                    {
                        // Love for those few void pointers out there
                        WritePointerMethod(outputWriter, method, objectType, methodName, true);
                    }
                }
                else if (method.ReturnsVoid)
                {
                    WriteVoidMethod(outputWriter, method, objectType, methodName, true);
                }
                else
                {
                    WritePlainMethod(outputWriter, method, objectType, methodName, true);
                }
            }
        }

        #region Writers

        // TODO: Modularise Writers. Then we can start to work on automatically unpacking the out parameters into return types

        /// <summary>
        /// </summary>
        /// <param name="outputWriter"></param>
        /// <param name="method"></param>
        /// <param name="objectType"></param>
        /// <param name="methodName"></param>
        /// <param name="resolveChainable"></param>
        private void WriteObjectMethod(StringBuilder outputWriter, IMethodSymbol method, ITypeSymbol objectType,
            string methodName, bool resolveChainable = false)
        {
            var parameterList = GetParameterList(method, 1, resolveChainable);
            var argumentList = GetArgumentList(method, 1, resolveChainable);
            var typeParameterList = GetTypeParameterList(method);
            var typeParameterConstraints = GetTypeParameterConstraints(method);

            var chainableHeader = GetChainableHeader(method, 1, resolveChainable);

            var returnType = (method.ReturnType as IPointerTypeSymbol).PointedAtType;
            outputWriter.Append($@"
    /// <summary>
    /// {method.Name}
    /// Generated from <see cref=""{Constants.WebGpuNS}.WebGPU""/>.
    /// </summary>
    public static unsafe {Constants.NativePtrType}<{returnType}> {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}
    {{{chainableHeader}
        return new {Constants.NativePtrType}<{returnType}>(WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList}));
    }}
");
        }

        private void WritePointerMethod(StringBuilder outputWriter, IMethodSymbol method, ITypeSymbol objectType,
            string methodName, bool resolveChainable = false)
        {
            var parameterList = GetParameterList(method, 1, resolveChainable);
            var argumentList = GetArgumentList(method, 1, resolveChainable);
            var typeParameterList = GetTypeParameterList(method);
            var typeParameterConstraints = GetTypeParameterConstraints(method);

            var chainableHeader = GetChainableHeader(method, 1, resolveChainable);

            outputWriter.Append($@"
    /// <summary>
    /// {method.Name}
    /// Generated from <see cref=""{Constants.WebGpuNS}.WebGPU""/>
    /// </summary>
    public static unsafe {method.ReturnType} {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}
    {{{chainableHeader}
        return WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList});
    }}
");
        }

        private void WriteVoidMethod(StringBuilder outputWriter, IMethodSymbol method, ITypeSymbol objectType,
            string methodName, bool resolveChainable = false)
        {
            var parameterList = GetParameterList(method, 1, resolveChainable);
            var argumentList = GetArgumentList(method, 1, resolveChainable);
            var typeParameterList = GetTypeParameterList(method);
            var typeParameterConstraints = GetTypeParameterConstraints(method);

            var chainableHeader = GetChainableHeader(method, 1, resolveChainable);

            outputWriter.Append($@"
    /// <summary>
    /// {method.Name}
    /// Generated from <see cref=""{Constants.WebGpuNS}.WebGPU""/>
    /// </summary>
    public static unsafe void {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}
    {{{chainableHeader}
        WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList});
    }}
");
        }

        private void WritePlainMethod(StringBuilder outputWriter, IMethodSymbol method, ITypeSymbol objectType,
            string methodName, bool resolveChainable = false)
        {
            var parameterList = GetParameterList(method, 1, resolveChainable);
            var argumentList = GetArgumentList(method, 1, resolveChainable);
            var typeParameterList = GetTypeParameterList(method);
            var typeParameterConstraints = GetTypeParameterConstraints(method);

            var chainableHeader = GetChainableHeader(method, 1, resolveChainable);

            outputWriter.Append($@"
    /// <summary>
    /// {method.Name}
    /// Generated from <see cref=""{Constants.WebGpuNS}.WebGPU""/>
    /// </summary>
    public static unsafe {method.ReturnType} {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}
    {{{chainableHeader}
        return WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList});
    }}
");
        }

        private StringBuilder GetChainableHeader(IMethodSymbol method, int startIndex = 0, bool generate = false)
        {
            var stringBuilder = new StringBuilder();
            if (!generate)
                return stringBuilder;
            for (int i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                if (IsChainableParameter(parameter))
                {
                    stringBuilder.Append($"\n        using var {parameter.Name}{ChainableUnmanagedSuffix} = {parameter.Name}.Get();");
                }
            }

            return stringBuilder;
        }

        #endregion

        #region Parameters

        private bool IsChainableParameter(IParameterSymbol parameter)
        {
            if (parameter.Type is IPointerTypeSymbol pointerType)
            {
                if (ChainableRoots.Contains(pointerType.PointedAtType.Name))
                {
                    return true;
                }
            }
            else if (ChainableRoots.Contains(parameter.Type.Name))
            {
                return true;
            }

            return false;
        }

        private string GetParameterType(IParameterSymbol parameter, bool resolveChainable = false)
        {
            if (parameter.Type is IPointerTypeSymbol pointerType)
            {
                if (Objects.Contains(pointerType.PointedAtType.Name))
                {
                    return $"{Constants.NativePtrType}<{pointerType.PointedAtType}>";
                }

                if (resolveChainable && ChainableRoots.Contains(pointerType.PointedAtType.Name))
                {
                    return $"{ChainableSafetyInterface}<{pointerType.PointedAtType}>";
                }
            }
            else if (resolveChainable && ChainableRoots.Contains(parameter.Type.Name))
            {
                return $"{ChainableSafetyInterface}<{parameter.Type}>";
            }

            return parameter.Type.ToString();
        }

        private string GetArgumentPrefix(IParameterSymbol parameterSymbol)
        {
            switch (parameterSymbol.RefKind)
            {
                case RefKind.Ref:
                    return "ref ";
                case RefKind.Out:
                    return "out ";
                case RefKind.In:
                case RefKind.None:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetArgumentSuffix(IParameterSymbol parameterSymbol)
        {
            // For references to pointers.
            if (parameterSymbol.RefKind == RefKind.Ref)
            {
                if (parameterSymbol.Type is IPointerTypeSymbol pointerType)
                {
                    if (Objects.Contains(pointerType.PointedAtType.Name))
                    {
                        return ".AsRef()";
                    }
                }
            }

            // TODO: Are there any others we may need?

            return "";
        }

        private string GetParameterPrefix(IParameterSymbol parameterSymbol)
        {
            switch (parameterSymbol.RefKind)
            {
                case RefKind.Ref:
                    return "ref ";
                case RefKind.Out:
                    return "out ";
                case RefKind.In:
                    return "in ";
                case RefKind.None:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /**
         * Get parameter list for new method, includes initial ", "
         */
        private StringBuilder GetParameterList(IMethodSymbol method, int startIndex = 0, bool resolveChainable = false)
        {
            var parameterList = new StringBuilder();
            for (int i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                parameterList.Append(
                    $", {GetParameterPrefix(parameter)}{GetParameterType(parameter, resolveChainable)} {parameter.Name}");
            }

            return parameterList;
        }

        private StringBuilder GetArgumentList(IMethodSymbol method, int startIndex = 0, bool resolveChainable = false)
        {
            // TODO: Is this where we will add the replacement for the descriptors?
            var argumentList = new StringBuilder();
            for (var i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                var parameterName = parameter.Name;
                if (resolveChainable && IsChainableParameter(parameter))
                    parameterName += ChainableUnmanagedSuffix;
                argumentList.Append($", {GetArgumentPrefix(parameter)}{parameterName}{GetArgumentSuffix(parameter)}");
            }

            return argumentList;
        }

        // endregion

        // region Type Parameters

        private List<string> GetTypeConstraints(ITypeParameterSymbol typeParameter)
        {
            var constraints = new List<string>();
            if (typeParameter.HasUnmanagedTypeConstraint)
            {
                constraints.Add("unmanaged");
            }

            if (typeParameter.HasConstructorConstraint)
            {
                constraints.Add("new()");
            }

            if (typeParameter.HasNotNullConstraint)
            {
                constraints.Add("notnull");
            }

            if (typeParameter.HasReferenceTypeConstraint)
            {
                constraints.Add("class");
            }

            if (typeParameter.HasValueTypeConstraint && !typeParameter.HasUnmanagedTypeConstraint)
            {
                constraints.Add("struct");
            }

            foreach (var constraintType in typeParameter.ConstraintTypes)
            {
                constraints.Add(constraintType.ToString());
            }

            return constraints;
        }

        private StringBuilder GetTypeParameterList(IMethodSymbol method)
        {
            var stringBuilder = new StringBuilder();

            if (!method.IsGenericMethod)
                return stringBuilder;

            stringBuilder.Append('<');
            foreach (var typeParameter in method.TypeParameters)
            {
                stringBuilder.Append($"{typeParameter.Name}, ");
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append('>');

            return stringBuilder;
        }

        private StringBuilder GetTypeParameterConstraints(IMethodSymbol method)
        {
            var stringBuilder = new StringBuilder();

            if (!method.IsGenericMethod)
                return stringBuilder;

            foreach (var typeParameter in method.TypeParameters)
            {
                var constraints = GetTypeConstraints(typeParameter);

                if (constraints.Count > 0)
                {
                    stringBuilder.Append($@"
        where {typeParameter.Name} : ");
                    for (int i = 0; i < constraints.Count; i++)
                    {
                        stringBuilder.Append(constraints[i]);
                        if (i + 1 < constraints.Count)
                            stringBuilder.Append(", ");
                    }
                }
            }

            return stringBuilder;
        }

        #endregion

        public void Initialize(GeneratorInitializationContext context)
        {
            // Do nothing.
        }
    }
}