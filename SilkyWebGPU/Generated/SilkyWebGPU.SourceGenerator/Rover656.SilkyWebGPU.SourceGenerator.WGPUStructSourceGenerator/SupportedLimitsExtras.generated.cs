﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras"/>
public sealed unsafe class SupportedLimitsExtras : ChainedStruct<Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras>
{
    public static implicit operator Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras(SupportedLimitsExtras managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras>(SupportedLimitsExtras managed)
        => managed != null ? managed.GetRef() : default;

    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras.MaxPushConstantSize" />
    public uint MaxPushConstantSize
    {
        get => Native->MaxPushConstantSize;
        set => Native->MaxPushConstantSize = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"SupportedLimitsExtras {{
    MaxPushConstantSize = ""{MaxPushConstantSize}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}