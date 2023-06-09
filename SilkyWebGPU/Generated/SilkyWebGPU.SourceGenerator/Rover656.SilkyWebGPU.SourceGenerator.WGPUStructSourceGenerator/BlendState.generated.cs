﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.BlendState"/>
public sealed unsafe class BlendState : WrappedStruct<Silk.NET.WebGPU.BlendState>
{
    public BlendState() : base() { }
    public BlendState(Silk.NET.WebGPU.BlendState native) : base(native) { }

    public static implicit operator BlendState(Silk.NET.WebGPU.BlendState native)
        => new BlendState(native);

    /// <seealso cref="Silk.NET.WebGPU.BlendState.Color" />
    public BlendComponent Color
    {
        get => Native.Color;
        set => Native.Color = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.BlendState.Alpha" />
    public BlendComponent Alpha
    {
        get => Native.Alpha;
        set => Native.Alpha = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"BlendState {{
    Color = ""{Color}""
    Alpha = ""{Alpha}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}
