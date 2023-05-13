using Microsoft.CodeAnalysis;
using Rover656.SilkyWebGPU.BaseSourceGenerator.Base;

namespace Rover656.SilkyWebGPU.SourceGenerator;

[Generator]
public class WebGPUExtensionSourceGenerator : BaseExtensionMethodGenerator
{
    public WebGPUExtensionSourceGenerator() : base($"{Constants.WebGpuNS}.WebGPU", Constants.ExtensionNS,
        Constants.APIHandle)
    {
    }
}