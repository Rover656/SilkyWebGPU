﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using System.Runtime.CompilerServices;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.PipelineLayoutExtras"/>
public class PipelineLayoutExtras : ChainedStruct<Silk.NET.WebGPU.Extensions.WGPU.PipelineLayoutExtras>
{

    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.PipelineLayoutExtras.PushConstantRanges" />
    public unsafe Silk.NET.WebGPU.Extensions.WGPU.PushConstantRange? PushConstantRanges
    {
        get
        {
            if (Native.PushConstantRanges == null)
                return null;
            return *Native.PushConstantRanges;
        }

        set
        {
            // If we're setting this to null, wipe the memory.
            if (!value.HasValue)
            {
                SilkMarshal.Free((nint) Native.PushConstantRanges);
                Native.PushConstantRanges = null;
                return;
            }

            // Because we will always own this handle, we allocate if its null, or we overwrite data.
            if (Native.PushConstantRanges == null)
                Native.PushConstantRanges = (Silk.NET.WebGPU.Extensions.WGPU.PushConstantRange*) SilkMarshal.Allocate(sizeof(Silk.NET.WebGPU.Extensions.WGPU.PushConstantRange));

            // Write new data
            *Native.PushConstantRanges = value.Value;
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"PipelineLayoutExtras {{
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.PushConstantRanges);
    }
}
