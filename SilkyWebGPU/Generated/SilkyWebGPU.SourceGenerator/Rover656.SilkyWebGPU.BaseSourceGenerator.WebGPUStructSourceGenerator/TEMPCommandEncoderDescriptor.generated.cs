﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPCommandEncoderDescriptor : WrappedStruct<Silk.NET.WebGPU.CommandEncoderDescriptor>
{
    public unsafe byte* Label
    {
        get => Native.Label;
        set => Native.Label = value;
    }
}