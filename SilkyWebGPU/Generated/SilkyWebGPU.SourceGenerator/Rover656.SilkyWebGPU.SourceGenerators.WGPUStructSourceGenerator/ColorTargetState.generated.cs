﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ColorTargetState"/>
public class ColorTargetState : ChainedStruct<Silk.NET.WebGPU.ColorTargetState>
{

    /// <seealso cref="Silk.NET.WebGPU.ColorTargetState.Format" />
    public Silk.NET.WebGPU.TextureFormat Format
    {
        get => Native.Format;
        set => Native.Format = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ColorTargetState.Blend" />
    public unsafe Silk.NET.WebGPU.BlendState? Blend
    {
        get
        {
            if (Native.Blend == null)
                return null;
            return *Native.Blend;
        }

        set
        {
            // If we're setting this to null, wipe the memory.
            if (!value.HasValue)
            {
                SilkMarshal.Free((nint) Native.Blend);
                Native.Blend = null;
                return;
            }

            // Because we will always own this handle, we allocate if its null, or we overwrite data.
            if (Native.Blend == null)
                Native.Blend = (Silk.NET.WebGPU.BlendState*) SilkMarshal.Allocate(sizeof(Silk.NET.WebGPU.BlendState));

            // Write new data
            *Native.Blend = value.Value;
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ColorTargetState.WriteMask" />
    public Silk.NET.WebGPU.ColorWriteMask WriteMask
    {
        get => Native.WriteMask;
        set => Native.WriteMask = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ColorTargetState {{
    Format = ""{Format}""
    WriteMask = ""{WriteMask}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.Blend);
    }
}
