﻿using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Rover656.SilkyWebGPU.SourceGenerator;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public class BaseStructGenerator : ISourceGenerator
{
    private GeneratorConfig _config;

    public BaseStructGenerator(GeneratorConfig config)
    {
        _config = config;
    }
    
    public void Execute(GeneratorExecutionContext context)
    {
        // Get list of structs
        var structDefinitions = new List<BaseStructDefinition>();
        foreach (var structName in _config.ManagedStructs)
        {
            // Attempt to get type info
            var structSymbol = context.Compilation.GetTypeByMetadataName($"{_config.WebGPUNamespace}.{structName}");
            if (structName == null)
                continue;
            
            // Process it
            ProcessStruct(structSymbol, ref structDefinitions);
        }

        foreach (var structDefinition in structDefinitions)
        {
            var outputWriter = new StringBuilder($@"// <auto-generated/>

using {GlobalConstants.ProjectNS};
using {GlobalConstants.ProjectNS}.Native;
using {GlobalConstants.ProjectNS}.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace {GlobalConstants.ProjectNS};
");

            structDefinition.WriteSource(outputWriter);
            
            context.AddSource($"{structDefinition.Name}.generated.cs", outputWriter.ToString());
        }
    }

    private void ProcessStruct(INamedTypeSymbol type, ref List<BaseStructDefinition> structDefinitions)
    {
        // TODO: Determine correct type based on name
        structDefinitions.Add(new WrappedStructDefinition(type));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}