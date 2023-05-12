using Microsoft.CodeAnalysis;
using Rover656.SilkyWebGPU.BaseSourceGenerator;

namespace Rover656.SilkyWebGPU.SourceGenerator;

[Generator]
public class WebGPUExtensionSourceGenerator : ISourceGenerator
{
    private BaseExtensionMethodGenerator _methodGenerator;

    public WebGPUExtensionSourceGenerator()
    {
        _methodGenerator = new BaseExtensionMethodGenerator($"{Constants.WebGpuNS}.WebGPU", Constants.ExtensionNS,
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