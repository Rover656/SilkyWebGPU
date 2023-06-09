﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.VertexBufferLayout"/>
public sealed unsafe class VertexBufferLayout : WrappedStruct<Silk.NET.WebGPU.VertexBufferLayout>
{
    public VertexBufferLayout() : base() { }
    public VertexBufferLayout(Silk.NET.WebGPU.VertexBufferLayout native) : base(native) { }

    public static implicit operator VertexBufferLayout(Silk.NET.WebGPU.VertexBufferLayout native)
        => new VertexBufferLayout(native);

    /// <seealso cref="Silk.NET.WebGPU.VertexBufferLayout.ArrayStride" />
    public ulong ArrayStride
    {
        get => Native.ArrayStride;
        set => Native.ArrayStride = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.VertexBufferLayout.StepMode" />
    public Silk.NET.WebGPU.VertexStepMode StepMode
    {
        get => Native.StepMode;
        set => Native.StepMode = value;
    }
 
    // Keep a copy around for disposal.
    private NativeArray<Silk.NET.WebGPU.VertexAttribute> _Attributes;

    /// <seealso cref="Silk.NET.WebGPU.VertexBufferLayout.Attributes" />
    /// <remarks>
    /// TODO: Write this remark.
    /// Summary: Will update if you modify the existing pointer, but if you replace it, it won't.
    /// </remarks>
    public NativeArray<Silk.NET.WebGPU.VertexAttribute> Attributes
    {
        // Return a readonly weak copy.
        get => new NativeArray<Silk.NET.WebGPU.VertexAttribute>((int) Native.AttributeCount, Native.Attributes);

        set
        {
            // Dispose any existing object.
            _Attributes?.Dispose();

            // Set array
            if (value != null)
            {
                Native.Attributes = value.Ptr;
                Native.AttributeCount = (UInt32) value.Count;
            }
            else
            {
                Native.Attributes = null;
                Native.AttributeCount = 0;
            }

            // Save for later disposal
            _Attributes = value;
        }
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"VertexBufferLayout {{
    ArrayStride = ""{ArrayStride}""
    StepMode = ""{StepMode}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        if (disposing)
        {
            _Attributes?.Dispose();
            _Attributes = null;
        }

        base.Dispose(disposing);
    }
}
