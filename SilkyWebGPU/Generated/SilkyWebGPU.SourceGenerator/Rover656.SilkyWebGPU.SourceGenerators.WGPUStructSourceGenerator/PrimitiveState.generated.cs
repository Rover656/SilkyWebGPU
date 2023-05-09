﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.PrimitiveState"/>
public class ManagedPrimitiveState : ChainedStruct<Silk.NET.WebGPU.PrimitiveState>
{

    /// <seealso cref="Silk.NET.WebGPU.PrimitiveState.Topology" />
    public Silk.NET.WebGPU.PrimitiveTopology Topology
    {
        get => Native.Topology;
        set => Native.Topology = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.PrimitiveState.StripIndexFormat" />
    public Silk.NET.WebGPU.IndexFormat StripIndexFormat
    {
        get => Native.StripIndexFormat;
        set => Native.StripIndexFormat = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.PrimitiveState.FrontFace" />
    public Silk.NET.WebGPU.FrontFace FrontFace
    {
        get => Native.FrontFace;
        set => Native.FrontFace = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.PrimitiveState.CullMode" />
    public Silk.NET.WebGPU.CullMode CullMode
    {
        get => Native.CullMode;
        set => Native.CullMode = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"PrimitiveState {{
    Topology = ""{Topology}""
    StripIndexFormat = ""{StripIndexFormat}""
    FrontFace = ""{FrontFace}""
    CullMode = ""{CullMode}""
}}";
    }
}
