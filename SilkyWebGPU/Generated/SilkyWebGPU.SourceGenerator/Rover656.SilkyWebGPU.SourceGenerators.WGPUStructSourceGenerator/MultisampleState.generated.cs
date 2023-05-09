﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.MultisampleState"/>
public class ManagedMultisampleState : ChainedStruct<Silk.NET.WebGPU.MultisampleState>
{

    /// <seealso cref="Silk.NET.WebGPU.MultisampleState.Count" />
    public uint Count
    {
        get => Native.Count;
        set => Native.Count = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.MultisampleState.Mask" />
    public uint Mask
    {
        get => Native.Mask;
        set => Native.Mask = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.MultisampleState.AlphaToCoverageEnabled" />
    public Silk.NET.Core.Bool32 AlphaToCoverageEnabled
    {
        get => Native.AlphaToCoverageEnabled;
        set => Native.AlphaToCoverageEnabled = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"MultisampleState {{
    Count = ""{Count}""
    Mask = ""{Mask}""
    AlphaToCoverageEnabled = ""{AlphaToCoverageEnabled}""
}}";
    }
}
