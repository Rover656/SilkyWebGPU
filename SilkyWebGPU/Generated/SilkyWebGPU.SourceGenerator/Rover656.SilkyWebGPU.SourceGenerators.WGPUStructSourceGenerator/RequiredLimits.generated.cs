﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.RequiredLimits"/>
public class RequiredLimits : NewNewChainedStruct<Silk.NET.WebGPU.RequiredLimits>
{

    /// <seealso cref="Silk.NET.WebGPU.RequiredLimits.Limits" />
    public Limits Limits
    {
        get => Native.Limits;
        set => Native.Limits = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"RequiredLimits {{
    Limits = ""{Limits}""
}}";
    }
    internal override RequiredLimits Clone()
    {
        var clone = new RequiredLimits();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
