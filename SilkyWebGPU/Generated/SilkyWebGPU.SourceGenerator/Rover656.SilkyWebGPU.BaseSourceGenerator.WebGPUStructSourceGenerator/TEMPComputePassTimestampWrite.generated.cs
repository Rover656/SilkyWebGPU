﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPComputePassTimestampWrite : WrappedStruct<Silk.NET.WebGPU.ComputePassTimestampWrite>
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

    public Silk.NET.WebGPU.ComputePassTimestampLocation Location
    {
        get => Native.Location;
        set => Native.Location = value;
    }
}