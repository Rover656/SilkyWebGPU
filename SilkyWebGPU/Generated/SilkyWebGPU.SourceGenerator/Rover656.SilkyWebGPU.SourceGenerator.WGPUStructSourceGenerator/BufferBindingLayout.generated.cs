﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.BufferBindingLayout"/>
public sealed unsafe class BufferBindingLayout : ChainedStruct<Silk.NET.WebGPU.BufferBindingLayout>
{
    public static implicit operator Silk.NET.WebGPU.BufferBindingLayout(BufferBindingLayout managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.BufferBindingLayout>(BufferBindingLayout managed)
        => managed != null ? managed.GetRef() : default;

    /// <seealso cref="Silk.NET.WebGPU.BufferBindingLayout.Type" />
    public Silk.NET.WebGPU.BufferBindingType Type
    {
        get => Native->Type;
        set => Native->Type = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.BufferBindingLayout.HasDynamicOffset" />
    public Silk.NET.Core.Bool32 HasDynamicOffset
    {
        get => Native->HasDynamicOffset;
        set => Native->HasDynamicOffset = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.BufferBindingLayout.MinBindingSize" />
    public ulong MinBindingSize
    {
        get => Native->MinBindingSize;
        set => Native->MinBindingSize = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"BufferBindingLayout {{
    Type = ""{Type}""
    HasDynamicOffset = ""{HasDynamicOffset}""
    MinBindingSize = ""{MinBindingSize}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        base.Dispose(disposing);
    }
}
