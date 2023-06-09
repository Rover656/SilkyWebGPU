﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPColorTargetState : WrappedStruct<Silk.NET.WebGPU.ColorTargetState>
{
    public Silk.NET.WebGPU.TextureFormat Format
    {
        get => Native.Format;
        set => Native.Format = value;
    }

    public unsafe Silk.NET.WebGPU.BlendState* Blend
    {
        get => Native.Blend;
        set => Native.Blend = value;
    }

    public Silk.NET.WebGPU.ColorWriteMask WriteMask
    {
        get => Native.WriteMask;
        set => Native.WriteMask = value;
    }
}