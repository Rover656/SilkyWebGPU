using Microsoft.CodeAnalysis;
using Rover656.SilkyWebGPU.BaseSourceGenerator;

namespace Rover656.SilkyWebGPU.Extensions.WGPU.SourceGenerator;

[Generator]
public class WGPUExtensionSourceGenerator : ISourceGenerator
{
    private BaseExtensionMethodGenerator _methodGenerator;

    public WGPUExtensionSourceGenerator()
    {
        _methodGenerator = new BaseExtensionMethodGenerator($"{Constants.WGPUNS}.Wgpu", Constants.ExtensionNS,
            Constants.APIHandle);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        _methodGenerator.Execute(context);
    }
}