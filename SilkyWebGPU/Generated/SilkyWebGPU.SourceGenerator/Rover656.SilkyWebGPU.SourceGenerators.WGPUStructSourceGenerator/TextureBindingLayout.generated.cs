﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.TextureBindingLayout"/>
public class TextureBindingLayout : NewNewChainedStruct<Silk.NET.WebGPU.TextureBindingLayout>
{

    /// <seealso cref="Silk.NET.WebGPU.TextureBindingLayout.SampleType" />
    public Silk.NET.WebGPU.TextureSampleType SampleType
    {
        get => Native.SampleType;
        set => Native.SampleType = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureBindingLayout.ViewDimension" />
    public Silk.NET.WebGPU.TextureViewDimension ViewDimension
    {
        get => Native.ViewDimension;
        set => Native.ViewDimension = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureBindingLayout.Multisampled" />
    public Silk.NET.Core.Bool32 Multisampled
    {
        get => Native.Multisampled;
        set => Native.Multisampled = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"TextureBindingLayout {{
    SampleType = ""{SampleType}""
    ViewDimension = ""{ViewDimension}""
    Multisampled = ""{Multisampled}""
}}";
    }
    internal override TextureBindingLayout Clone()
    {
        var clone = new TextureBindingLayout();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
