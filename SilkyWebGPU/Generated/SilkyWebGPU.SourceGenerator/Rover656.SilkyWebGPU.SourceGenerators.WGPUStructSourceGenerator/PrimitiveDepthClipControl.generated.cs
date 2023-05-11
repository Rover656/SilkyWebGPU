﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.PrimitiveDepthClipControl"/>
public class PrimitiveDepthClipControl : NewNewChainedStruct<Silk.NET.WebGPU.PrimitiveDepthClipControl>
{

    /// <seealso cref="Silk.NET.WebGPU.PrimitiveDepthClipControl.UnclippedDepth" />
    public Silk.NET.Core.Bool32 UnclippedDepth
    {
        get => Native.UnclippedDepth;
        set => Native.UnclippedDepth = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"PrimitiveDepthClipControl {{
    UnclippedDepth = ""{UnclippedDepth}""
}}";
    }
    internal override PrimitiveDepthClipControl Clone()
    {
        var clone = new PrimitiveDepthClipControl();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
