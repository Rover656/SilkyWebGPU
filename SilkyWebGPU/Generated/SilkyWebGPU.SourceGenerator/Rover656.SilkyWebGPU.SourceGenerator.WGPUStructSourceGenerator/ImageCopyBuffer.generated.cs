﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ImageCopyBuffer"/>
public sealed unsafe class ImageCopyBuffer : ChainedStruct<Silk.NET.WebGPU.ImageCopyBuffer>
{
    public static implicit operator Silk.NET.WebGPU.ImageCopyBuffer(ImageCopyBuffer managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.ImageCopyBuffer>(ImageCopyBuffer managed)
        => managed != null ? managed.GetRef() : default;

    // Keep the pointer alive :D
    private TextureDataLayout _Layout;

    /// <seealso cref="Silk.NET.WebGPU.ImageCopyBuffer.Layout" />
    public TextureDataLayout Layout
    {
        get => _Layout;

        set
        {
            // Dispose existing
            _Layout?.Dispose();
            
            // Store managed version
            _Layout = value;

            // Set within chain.
            if (value != null)
                Native->Layout = value.Get();
            else Native->Layout = default;
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ImageCopyBuffer.Buffer" />
    public WebGPUPtr<Silk.NET.WebGPU.Buffer> Buffer
    {
        get => WebGPUPtr<Silk.NET.WebGPU.Buffer>.Weak(Native->Buffer);
        set => Native->Buffer = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ImageCopyBuffer {{
    Layout = ""{Layout}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        if (disposing)
        {
            _Layout?.Dispose();
            _Layout = null;
        }

        base.Dispose(disposing);
    }
}