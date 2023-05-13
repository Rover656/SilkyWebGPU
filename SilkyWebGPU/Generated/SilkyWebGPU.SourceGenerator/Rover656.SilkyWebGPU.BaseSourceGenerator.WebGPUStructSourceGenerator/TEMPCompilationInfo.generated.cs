﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPCompilationInfo : WrappedStruct<Silk.NET.WebGPU.CompilationInfo>
{
    public uint MessageCount
    {
        get => Native.MessageCount;
        set => Native.MessageCount = value;
    }

    public unsafe Silk.NET.WebGPU.CompilationMessage* Messages
    {
        get => Native.Messages;
        set => Native.Messages = value;
    }
}