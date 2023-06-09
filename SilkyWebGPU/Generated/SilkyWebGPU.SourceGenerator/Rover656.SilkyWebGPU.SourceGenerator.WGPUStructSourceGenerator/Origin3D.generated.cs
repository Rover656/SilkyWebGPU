﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Origin3D"/>
public sealed unsafe class Origin3D : WrappedStruct<Silk.NET.WebGPU.Origin3D>
{
    public Origin3D() : base() { }
    public Origin3D(Silk.NET.WebGPU.Origin3D native) : base(native) { }

    public static implicit operator Origin3D(Silk.NET.WebGPU.Origin3D native)
        => new Origin3D(native);

    /// <seealso cref="Silk.NET.WebGPU.Origin3D.X" />
    public uint X
    {
        get => Native.X;
        set => Native.X = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Origin3D.Y" />
    public uint Y
    {
        get => Native.Y;
        set => Native.Y = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Origin3D.Z" />
    public uint Z
    {
        get => Native.Z;
        set => Native.Z = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"Origin3D {{
    X = ""{X}""
    Y = ""{Y}""
    Z = ""{Z}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}
