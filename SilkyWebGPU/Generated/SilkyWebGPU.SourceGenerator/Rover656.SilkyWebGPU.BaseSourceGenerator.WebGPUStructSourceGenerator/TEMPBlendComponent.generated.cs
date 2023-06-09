﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPBlendComponent : WrappedStruct<Silk.NET.WebGPU.BlendComponent>
{
    public Silk.NET.WebGPU.BlendOperation Operation
    {
        get => Native.Operation;
        set => Native.Operation = value;
    }

    public Silk.NET.WebGPU.BlendFactor SrcFactor
    {
        get => Native.SrcFactor;
        set => Native.SrcFactor = value;
    }

    public Silk.NET.WebGPU.BlendFactor DstFactor
    {
        get => Native.DstFactor;
        set => Native.DstFactor = value;
    }
}