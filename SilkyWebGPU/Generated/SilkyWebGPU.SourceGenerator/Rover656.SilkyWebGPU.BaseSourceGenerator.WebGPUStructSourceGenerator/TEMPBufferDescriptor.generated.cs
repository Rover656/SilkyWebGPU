﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPBufferDescriptor : WrappedStruct<Silk.NET.WebGPU.BufferDescriptor>
{
    public unsafe byte* Label
    {
        get => Native.Label;
        set => Native.Label = value;
    }

    public Silk.NET.WebGPU.BufferUsage Usage
    {
        get => Native.Usage;
        set => Native.Usage = value;
    }

    public ulong Size
    {
        get => Native.Size;
        set => Native.Size = value;
    }

    public Silk.NET.Core.Bool32 MappedAtCreation
    {
        get => Native.MappedAtCreation;
        set => Native.MappedAtCreation = value;
    }
}