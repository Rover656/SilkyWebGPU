﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPSurfaceDescriptorFromXlibWindow : WrappedStruct<Silk.NET.WebGPU.SurfaceDescriptorFromXlibWindow>
{
    public unsafe void* Display
    {
        get => Native.Display;
        set => Native.Display = value;
    }

    public uint Window
    {
        get => Native.Window;
        set => Native.Window = value;
    }
}