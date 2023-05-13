﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPTextureBindingLayout : WrappedStruct<Silk.NET.WebGPU.TextureBindingLayout>
{
    public Silk.NET.WebGPU.TextureSampleType SampleType
    {
        get => Native.SampleType;
        set => Native.SampleType = value;
    }

    public Silk.NET.WebGPU.TextureViewDimension ViewDimension
    {
        get => Native.ViewDimension;
        set => Native.ViewDimension = value;
    }

    public Silk.NET.Core.Bool32 Multisampled
    {
        get => Native.Multisampled;
        set => Native.Multisampled = value;
    }
}