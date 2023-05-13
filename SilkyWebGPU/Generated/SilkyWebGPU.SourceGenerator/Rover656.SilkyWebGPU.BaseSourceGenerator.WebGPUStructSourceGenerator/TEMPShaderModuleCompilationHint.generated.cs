﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPShaderModuleCompilationHint : WrappedStruct<Silk.NET.WebGPU.ShaderModuleCompilationHint>
{
    public unsafe byte* EntryPoint
    {
        get => Native.EntryPoint;
        set => Native.EntryPoint = value;
    }

    public unsafe Silk.NET.WebGPU.PipelineLayout* Layout
    {
        get => Native.Layout;
        set => Native.Layout = value;
    }
}