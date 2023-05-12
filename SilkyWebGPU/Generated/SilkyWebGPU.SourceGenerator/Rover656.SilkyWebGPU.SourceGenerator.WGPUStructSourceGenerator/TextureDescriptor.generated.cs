﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.TextureDescriptor"/>
public sealed unsafe class TextureDescriptor : ChainedStruct<Silk.NET.WebGPU.TextureDescriptor>
{
    public static implicit operator Silk.NET.WebGPU.TextureDescriptor(TextureDescriptor managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.TextureDescriptor>(TextureDescriptor managed)
        => managed != null ? managed.GetRef() : default;

    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.Label" />
    public string Label
    {
        get => SilkMarshal.PtrToString((nint) Native->Label);
        set
        {
           if (Native->Label != null)
               SilkMarshal.Free((nint) Native->Label);
           Native->Label = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.Usage" />
    public Silk.NET.WebGPU.TextureUsage Usage
    {
        get => Native->Usage;
        set => Native->Usage = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.Dimension" />
    public Silk.NET.WebGPU.TextureDimension Dimension
    {
        get => Native->Dimension;
        set => Native->Dimension = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.Size" />
    public Extent3D Size
    {
        get => Native->Size;
        set => Native->Size = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.Format" />
    public Silk.NET.WebGPU.TextureFormat Format
    {
        get => Native->Format;
        set => Native->Format = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.MipLevelCount" />
    public uint MipLevelCount
    {
        get => Native->MipLevelCount;
        set => Native->MipLevelCount = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.SampleCount" />
    public uint SampleCount
    {
        get => Native->SampleCount;
        set => Native->SampleCount = value;
    }
 
    // Keep a copy around for disposal.
    private NativeArray<Silk.NET.WebGPU.TextureFormat> _ViewFormats;

    /// <seealso cref="Silk.NET.WebGPU.TextureDescriptor.ViewFormats" />
    /// <remarks>
    /// TODO: Write this remark.
    /// Summary: Will update if you modify the existing pointer, but if you replace it, it won't.
    /// </remarks>
    public NativeArray<Silk.NET.WebGPU.TextureFormat> ViewFormats
    {
        // Return a readonly weak copy.
        get => new NativeArray<Silk.NET.WebGPU.TextureFormat>((int) Native->ViewFormatCount, Native->ViewFormats);

        set
        {
            // Dispose any existing object.
            _ViewFormats?.Dispose();

            // Set array
            if (value != null)
            {
                Native->ViewFormats = value.Ptr;
                Native->ViewFormatCount = (UInt32) value.Count;
            }
            else
            {
                Native->ViewFormats = null;
                Native->ViewFormatCount = 0;
            }

            // Save for later disposal
            _ViewFormats = value;
        }
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"TextureDescriptor {{
    Label = ""{Label}""
    Usage = ""{Usage}""
    Dimension = ""{Dimension}""
    Size = ""{Size}""
    Format = ""{Format}""
    MipLevelCount = ""{MipLevelCount}""
    SampleCount = ""{SampleCount}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        if (disposing)
        {
            _ViewFormats?.Dispose();
            _ViewFormats = null;
        }

        SilkMarshal.Free((nint) Native->Label);
        Native->Label = null;
        base.Dispose(disposing);
    }
}