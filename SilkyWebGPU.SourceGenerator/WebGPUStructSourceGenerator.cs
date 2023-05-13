using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

[Generator]
public class WebGPUStructSourceGenerator : BaseStructGenerator
{
    public WebGPUStructSourceGenerator() : base(Config.GeneratorConfig)
    {
    }
}