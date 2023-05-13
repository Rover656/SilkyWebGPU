using System.Collections.Generic;
using System.Linq;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public class GeneratorConfig
{
    public string WebGPUNamespace;
    public List<string> Objects;
    public List<string> ChainableStructs;
    public List<(string, string)> ChainedStructs;
    public List<string> WrappedStructs;
    
    public List<string> ManagedStructs => ChainableStructs.Concat(ChainedStructs.Select(e => e.Item1)).Concat(WrappedStructs).ToList();
}