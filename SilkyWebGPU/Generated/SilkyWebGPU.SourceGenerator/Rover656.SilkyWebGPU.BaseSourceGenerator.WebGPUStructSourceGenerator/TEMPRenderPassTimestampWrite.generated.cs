﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPRenderPassTimestampWrite : WrappedStruct<Silk.NET.WebGPU.RenderPassTimestampWrite>
{
    public unsafe Silk.NET.WebGPU.QuerySet* QuerySet
    {
        get => Native.QuerySet;
        set => Native.QuerySet = value;
    }

    public uint QueryIndex
    {
        get => Native.QueryIndex;
        set => Native.QueryIndex = value;
    }

    public Silk.NET.WebGPU.RenderPassTimestampLocation Location
    {
        get => Native.Location;
        set => Native.Location = value;
    }
}