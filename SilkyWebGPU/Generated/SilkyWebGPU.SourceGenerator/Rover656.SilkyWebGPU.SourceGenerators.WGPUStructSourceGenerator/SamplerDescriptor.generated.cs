﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor"/>
public class ManagedSamplerDescriptor : ChainedStruct<Silk.NET.WebGPU.SamplerDescriptor>
{

    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.Label" />
    public unsafe string Label
    {
        get => SilkMarshal.PtrToString((nint) Native.Label);
        set
       {
           if (Native.Label != null)
               SilkMarshal.Free((nint) Native.Label);
           Native.Label = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.AddressModeU" />
    public Silk.NET.WebGPU.AddressMode AddressModeU
    {
        get => Native.AddressModeU;
        set => Native.AddressModeU = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.AddressModeV" />
    public Silk.NET.WebGPU.AddressMode AddressModeV
    {
        get => Native.AddressModeV;
        set => Native.AddressModeV = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.AddressModeW" />
    public Silk.NET.WebGPU.AddressMode AddressModeW
    {
        get => Native.AddressModeW;
        set => Native.AddressModeW = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.MagFilter" />
    public Silk.NET.WebGPU.FilterMode MagFilter
    {
        get => Native.MagFilter;
        set => Native.MagFilter = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.MinFilter" />
    public Silk.NET.WebGPU.FilterMode MinFilter
    {
        get => Native.MinFilter;
        set => Native.MinFilter = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.MipmapFilter" />
    public Silk.NET.WebGPU.MipmapFilterMode MipmapFilter
    {
        get => Native.MipmapFilter;
        set => Native.MipmapFilter = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.LodMinClamp" />
    public float LodMinClamp
    {
        get => Native.LodMinClamp;
        set => Native.LodMinClamp = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.LodMaxClamp" />
    public float LodMaxClamp
    {
        get => Native.LodMaxClamp;
        set => Native.LodMaxClamp = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.Compare" />
    public Silk.NET.WebGPU.CompareFunction Compare
    {
        get => Native.Compare;
        set => Native.Compare = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.SamplerDescriptor.MaxAnisotropy" />
    public ushort MaxAnisotropy
    {
        get => Native.MaxAnisotropy;
        set => Native.MaxAnisotropy = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"SamplerDescriptor {{
    Label = ""{Label}""
    AddressModeU = ""{AddressModeU}""
    AddressModeV = ""{AddressModeV}""
    AddressModeW = ""{AddressModeW}""
    MagFilter = ""{MagFilter}""
    MinFilter = ""{MinFilter}""
    MipmapFilter = ""{MipmapFilter}""
    LodMinClamp = ""{LodMinClamp}""
    LodMaxClamp = ""{LodMaxClamp}""
    Compare = ""{Compare}""
    MaxAnisotropy = ""{MaxAnisotropy}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.Label);
    }
}
