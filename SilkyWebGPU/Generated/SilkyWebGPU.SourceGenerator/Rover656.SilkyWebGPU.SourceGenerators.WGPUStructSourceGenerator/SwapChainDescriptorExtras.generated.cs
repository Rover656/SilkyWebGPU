﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using System.Runtime.CompilerServices;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SwapChainDescriptorExtras"/>
public class SwapChainDescriptorExtras : ChainedStruct<Silk.NET.WebGPU.Extensions.WGPU.SwapChainDescriptorExtras>
{

    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SwapChainDescriptorExtras.AlphaMode" />
    public Silk.NET.WebGPU.Extensions.WGPU.CompositeAlphaMode AlphaMode
    {
        get => Native.AlphaMode;
        set => Native.AlphaMode = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.SwapChainDescriptorExtras.ViewFormats" />
    public unsafe Silk.NET.WebGPU.TextureFormat? ViewFormats
    {
        get
        {
            if (Native.ViewFormats == null)
                return null;
            return *Native.ViewFormats;
        }

        set
        {
            // If we're setting this to null, wipe the memory.
            if (!value.HasValue)
            {
                SilkMarshal.Free((nint) Native.ViewFormats);
                Native.ViewFormats = null;
                return;
            }

            // Because we will always own this handle, we allocate if its null, or we overwrite data.
            if (Native.ViewFormats == null)
                Native.ViewFormats = (Silk.NET.WebGPU.TextureFormat*) SilkMarshal.Allocate(sizeof(Silk.NET.WebGPU.TextureFormat));

            // Write new data
            *Native.ViewFormats = value.Value;
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"SwapChainDescriptorExtras {{
    AlphaMode = ""{AlphaMode}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.ViewFormats);
    }
}
