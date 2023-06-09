﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPShaderModuleSPIRVDescriptor : WrappedStruct<Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor>
{
    public uint CodeSize
    {
        get => Native.CodeSize;
        set => Native.CodeSize = value;
    }

    public unsafe uint* Code
    {
        get => Native.Code;
        set => Native.Code = value;
    }
}