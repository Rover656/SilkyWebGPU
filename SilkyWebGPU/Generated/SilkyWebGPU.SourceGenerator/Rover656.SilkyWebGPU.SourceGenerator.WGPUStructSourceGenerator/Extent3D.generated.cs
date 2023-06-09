﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extent3D"/>
public sealed unsafe class Extent3D : WrappedStruct<Silk.NET.WebGPU.Extent3D>
{
    public Extent3D() : base() { }
    public Extent3D(Silk.NET.WebGPU.Extent3D native) : base(native) { }

    public static implicit operator Extent3D(Silk.NET.WebGPU.Extent3D native)
        => new Extent3D(native);

    /// <seealso cref="Silk.NET.WebGPU.Extent3D.Width" />
    public uint Width
    {
        get => Native.Width;
        set => Native.Width = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extent3D.Height" />
    public uint Height
    {
        get => Native.Height;
        set => Native.Height = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extent3D.DepthOrArrayLayers" />
    public uint DepthOrArrayLayers
    {
        get => Native.DepthOrArrayLayers;
        set => Native.DepthOrArrayLayers = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"Extent3D {{
    Width = ""{Width}""
    Height = ""{Height}""
    DepthOrArrayLayers = ""{DepthOrArrayLayers}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}
