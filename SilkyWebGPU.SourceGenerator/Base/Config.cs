using System.Linq;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public static class Config
{
    public static GeneratorConfig GeneratorConfig = new GeneratorConfig
    {
        ChainableStructs = GlobalConstants.ChainableStructs.ToList(),
        ChainedStructs = GlobalConstants.ChainedStructs.ToList(),
        Objects = GlobalConstants.Objects.ToList(),
        WebGPUNamespace = GlobalConstants.WebGpuNS,
        WrappedStructs = GlobalConstants.WrappedStructs.ToList(),
    };
}