﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras"/>
public class SupportedLimitsExtras : ChainedStruct<Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras>
{

    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras.MaxPushConstantSize" />
    public uint MaxPushConstantSize
    {
        get => Native.MaxPushConstantSize;
        set => Native.MaxPushConstantSize = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"SupportedLimitsExtras {{
    MaxPushConstantSize = ""{MaxPushConstantSize}""
}}";
    }
}
