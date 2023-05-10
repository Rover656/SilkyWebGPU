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
        /// Suffix applied to the unmanaged versions of chainable objects.
        /// </summary>
        private const string ChainableUnmanagedSuffix = "Unmanaged";

        /// <summary>
        /// Setting this to false obliterates any raw pointers. They will all be replaced by the native pointer type, or hidden to allow other overloads space.
        /// This basically forces the full use of the abstraction layer.
        /// </summary>
        private const bool AllowStrayPointers = true; // TODO: Needs to be enabled to be able to set null on userdatum nicely.

        public void Execute(GeneratorExecutionContext context)
        {
            var webGpu = context.Compilation.GetTypeByMetadataName($"{Constants.WebGpuNS}.WebGPU");

            var outputWriter = new StringBuilder($@"// <auto-generated/>
// Temporary while in development
using {Constants.ExtensionNS};
using {Constants.ExtensionNS}.Native;
using {Constants.ExtensionNS}.Native.Chain;

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
            if (!Constants.ClassObjects.Contains(objectType.Name))
                return;

            // Determine new method name
            var methodName = method.Name.Replace(objectType.Name, "");

            // Process parameters, set flags for special outputs
            var chainablePresent = false;
            var generatePrimitive = true;
            foreach (var parameter in method.Parameters)
            {
                if (IsChainableParameter(parameter))
                {
                    chainablePresent = true;
                    generatePrimitive = false;
                }

                if (!AllowStrayPointers)
                {
                    if (parameter.Type is IPointerTypeSymbol && GetParameterType(parameter).Equals(parameter.Type.ToString()))
                    {
                        generatePrimitive = false;
                    }
                }
                
                // Skip anything that takes a chained struct by pointer, as we change to reference types and its incompatible
                // TODO: This would not handle the prefixed ones. Will need to add a Constants.IsWrappedStruct which takes in the entire type.
                // TODO: Can we remove this check again by actually fixing the issue rather than removing the methods?
                if (parameter.Type is IPointerTypeSymbol parameterPointer && Constants.ChainedStructs.Concat(Constants.WrappedStructs).Contains(parameterPointer.PointedAtType.Name))
                {
                    return;
                }
            }
            
            // TODO: For any methods that have ref parameters, we should generate additional methods that omit them and pass null to the original method.

            // Write 1:1 translation
            if (generatePrimitive)
            {
                var typeParameterList = GetTypeParameterList(method);
                var typeParameterConstraints = GetTypeParameterConstraints(method);
                var parameterList = GetParameterList(method, 1);
                var argumentList = GetArgumentList(method, 1);
                
                if (method.ReturnType is IPointerTypeSymbol pointerReturnType && pointerReturnType.PointedAtType.ToString() != "void")
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetObjectMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetObjectMethodBody(method, argumentList)}
    }}
");

                }
                else if (method.ReturnsVoid)
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetVoidMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetVoidMethodBody(method, argumentList)}
    }}
");
                }
                else
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetPlainMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetPlainMethodBody(method, argumentList)}
    }}
");
                }
            }

            // Chainable extras
            if (chainablePresent)
            {
                var typeParameterList = GetTypeParameterList(method);
                var typeParameterConstraints = GetTypeParameterConstraints(method);
                var parameterList = GetParameterList(method, 1, true);
                var argumentList = GetArgumentList(method, 1, true);

                // First pass
                if (method.ReturnType is IPointerTypeSymbol pointerReturnType && pointerReturnType.PointedAtType.ToString() != "void")
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetObjectMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetChainableBody(method, GetObjectMethodRetType(method), GetObjectMethodBody(method, argumentList), 1)}
    }}
");

                }
                else if (method.ReturnsVoid)
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetVoidMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetChainableBody(method, GetVoidMethodRetType(method), GetVoidMethodBody(method, argumentList), 1)}
    }}
");
                }
                else
                {
                    outputWriter.Append($@"
    {GetMethodDocString(method)}
    {GetPlainMethodSignature(method, objectType, methodName, typeParameterList, typeParameterConstraints, parameterList)}
    {{
{GetChainableBody(method, GetPlainMethodRetType(method), GetPlainMethodBody(method, argumentList), 1)}
    }}
");
                }
            }
        }

        #region Writers
        
        private string GetMethodDocString(IMethodSymbol method)
        {
            // TODO /// <inheritdoc cref=""{Constants.WebGpuNS}.WebGPU.{method.Name}""/>
            return $@"/// NEW STYLE
    /// <summary>
    /// {method.Name}
    /// Generated from <see cref=""{Constants.WebGpuNS}.WebGPU""/>.
    /// </summary>";
        }

        private string GetObjectMethodRetType(IMethodSymbol method)
        {
            return $"{Constants.NativePtrType}<{(method.ReturnType as IPointerTypeSymbol).PointedAtType}>";
        }
        
        private string GetPlainMethodRetType(IMethodSymbol method)
        {
            return method.ReturnType.ToString();
        }

        private string GetVoidMethodRetType(IMethodSymbol method)
        {
            return "void";
        }

        private string GetObjectMethodSignature(IMethodSymbol method, ITypeSymbol objectType, string methodName, StringBuilder typeParameterList, StringBuilder typeParameterConstraints, StringBuilder parameterList)
        {
            // var returnType = (method.ReturnType as IPointerTypeSymbol).PointedAtType;
            // return $"public static unsafe {Constants.NativePtrType}<{returnType}> {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}";
            return GetMethodSignature(method, GetObjectMethodRetType(method), objectType, methodName, typeParameterList,
                typeParameterConstraints, parameterList);
        }
        
        private string GetPlainMethodSignature(IMethodSymbol method, ITypeSymbol objectType, string methodName, StringBuilder typeParameterList, StringBuilder typeParameterConstraints, StringBuilder parameterList)
        {
            // return $"public static unsafe {method.ReturnType} {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}";
            return GetMethodSignature(method, GetPlainMethodRetType(method), objectType, methodName, typeParameterList,
                typeParameterConstraints, parameterList);
        }
        
        private string GetVoidMethodSignature(IMethodSymbol method, ITypeSymbol objectType, string methodName, StringBuilder typeParameterList, StringBuilder typeParameterConstraints, StringBuilder parameterList)
        {
            // return $"public static unsafe void {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}";
            return GetMethodSignature(method, GetVoidMethodRetType(method), objectType, methodName, typeParameterList,
                typeParameterConstraints, parameterList);
        }
        
        private string GetMethodSignature(IMethodSymbol method, string returnType, ITypeSymbol objectType, string methodName, StringBuilder typeParameterList, StringBuilder typeParameterConstraints, StringBuilder parameterList)
        {
            return $"public static unsafe {returnType} {methodName}{typeParameterList}(this {Constants.NativePtrType}<{objectType}> {method.Parameters[0].Name}{parameterList}) {typeParameterConstraints}";
        }
        
        private string GetObjectMethodBody(IMethodSymbol method, StringBuilder argumentList)
        {
            var returnType = (method.ReturnType as IPointerTypeSymbol).PointedAtType;
            return $"        return new {Constants.NativePtrType}<{returnType}>(WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList}));";
        }

        private string GetPlainMethodBody(IMethodSymbol method, StringBuilder argumentList)
        {
            return $"        return WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList});";
        }
        
        private string GetVoidMethodBody(IMethodSymbol method, StringBuilder argumentList)
        {
            return $"        WGPU.API.{method.Name}({method.Parameters[0].Name}{argumentList});";
        }
        
        // One way could be to do the null check and pass along to an alternative body
        private StringBuilder GetChainableBody(IMethodSymbol method, string returnType, string body, int startIndex = 0)
        {
            // Count the number of chainable parameters.
            // Basically this generator *can* support multiple without optimisations, but is able to apply additional optimisations if there is only one.
            // Currently there is no known method that would have multiple chainables, but this is a safety case for future
            var chainableCount = method.Parameters.Count(IsChainableParameter);
            
            var stringBuilder = new StringBuilder();
            
            // Forward declare return variable
            if (!method.ReturnsVoid)
                stringBuilder.AppendLine($"        {returnType} ret;");
            
            for (int i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                if (IsChainableParameter(parameter))
                {
                    stringBuilder.Append("        ");
                    if (parameter.RefKind == RefKind.Ref)
                    {
                        // ref types cannot be null so we can write straight to them, then call mutate.
                        stringBuilder.AppendLine($@"var {parameter.Name}Chained = {parameter.Name}.GetWithChain();
        var {parameter.Name}{ChainableUnmanagedSuffix} = &{parameter.Name}Chained;");
                    }
                    else
                    {
                        if (chainableCount == 1)
                        {
                            stringBuilder.AppendLine($@"if ({parameter.Name} == null) {{
{body.Replace("return", "ret =").Replace($"{parameter.Name}{ChainableUnmanagedSuffix}", "null")}
        }}
        else
        {{
        var {parameter.Name}{ChainableUnmanagedSuffix} = {parameter.Name}.GetWithChain();");
                        }
                        else
                        {
                            stringBuilder.AppendLine($@"// ISSUE: This wouldn't have generated when the generator was designed. This is an ugly and slow way of not slowing down the source generator.
        var {parameter.Name}{ChainableUnmanagedSuffix} = {parameter.Name} != null ? {parameter.Name}.Alloc() : null;");
                        }
                    }
                }
            }

            stringBuilder.AppendLine($"{body.Replace("return", "ret =")}");

            for (int i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                if (IsChainableParameter(parameter))
                {
                    if (parameter.RefKind == RefKind.Ref)
                    {
                        // Now propagate mutations through the chain
                        stringBuilder.AppendLine($@"        {parameter.Name}.Mutate(((ChainedStruct*){parameter.Name}{ChainableUnmanagedSuffix}));
        ChainHelper.FreeChain(ref {parameter.Name}Chained);");
                    }
                    else
                    {
                        if (chainableCount == 1)
                        {
                            stringBuilder.AppendLine($@"        ChainHelper.FreeChain(ref {parameter.Name}{ChainableUnmanagedSuffix});
        }}");
                        }
                        else
                        {
                            stringBuilder.AppendLine($"        {parameter.Name}?.Free({parameter.Name}{ChainableUnmanagedSuffix});");
                        }
                    }
                }
            }

            if (!method.ReturnsVoid)
                stringBuilder.AppendLine("        return ret;");

            return stringBuilder;
        }

        #endregion

        #region Parameters

        private bool IsChainableParameter(IParameterSymbol parameter)
        {
            if (parameter.Type is IPointerTypeSymbol pointerType)
            {
                if (Constants.ChainedStructs.Contains(pointerType.PointedAtType.Name))
                {
                    return true;
                }
            }
            else if (Constants.ChainedStructs.Contains(parameter.Type.Name))
            {
                return true;
            }

            return false;
        }

        private string GetParameterType(IParameterSymbol parameter, bool resolveChainable = false)
        {
            if (parameter.Type is IPointerTypeSymbol pointerType)
            {
                if (Constants.ClassObjects.Contains(pointerType.PointedAtType.Name))
                {
                    return $"{Constants.NativePtrType}<{pointerType.PointedAtType}>";
                }

                if (resolveChainable && Constants.ManagedStructs.Contains(pointerType.PointedAtType.Name))
                {
                    // return $"{ChainableSafetyInterface}<{pointerType.PointedAtType}>";
                    return $"{Constants.ManagedStructPrefix}{pointerType.PointedAtType.Name}";
                }
            }
            else if (resolveChainable && Constants.ManagedStructs.Contains(parameter.Type.Name))
            {
                // return $"{ChainableSafetyInterface}<{parameter.Type}>";
                return $"{Constants.ManagedStructPrefix}{parameter.Type.Name}";
            }

            return parameter.Type.ToString();
        }

        private string GetArgumentPrefix(IParameterSymbol parameterSymbol, bool resolveChainable)
        {
            switch (parameterSymbol.RefKind)
            {
                case RefKind.Ref:
                    // We don't enable ref for chainable's as we use fixed.
                    if (resolveChainable && IsChainableParameter(parameterSymbol))
                        return "";
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
            // TODO: This needs fixed as it's creating false-positives for Queue.Submit accepting multiple command buffers. Need to detect arrays instead.
            if (parameterSymbol.RefKind == RefKind.Ref)
            {
                if (parameterSymbol.Type is IPointerTypeSymbol pointerType)
                {
                    if (Constants.ClassObjects.Contains(pointerType.PointedAtType.Name))
                    {
                        return ".AsRef()";
                    }
                }
            }

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
        private StringBuilder GetParameterList(IMethodSymbol method, int startIndex = 0, bool resolveChainable = false, bool nullChainables = false)
        {
            var parameterList = new StringBuilder();
            for (int i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];

                if (nullChainables && IsChainableParameter(parameter))
                    continue;
                
                parameterList.Append(
                    $", {GetParameterPrefix(parameter)}{GetParameterType(parameter, resolveChainable)} {parameter.Name}");
            }

            return parameterList;
        }

        private StringBuilder GetArgumentList(IMethodSymbol method, int startIndex = 0, bool resolveChainable = false, bool nullChainables = false)
        {
            var argumentList = new StringBuilder();
            for (var i = startIndex; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                
                string parameterName;
                if (resolveChainable && IsChainableParameter(parameter))
                    parameterName = nullChainables ? "null" : $"{parameter.Name}{ChainableUnmanagedSuffix}";
                else parameterName = parameter.Name;
                
                argumentList.Append($", {GetArgumentPrefix(parameter, resolveChainable)}{parameterName}{GetArgumentSuffix(parameter)}");
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