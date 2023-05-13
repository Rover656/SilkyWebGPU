﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPRenderPassColorAttachment : WrappedStruct<Silk.NET.WebGPU.RenderPassColorAttachment>
{
    public unsafe Silk.NET.WebGPU.TextureView* View
    {
        get => Native.View;
        set => Native.View = value;
    }

    public unsafe Silk.NET.WebGPU.TextureView* ResolveTarget
    {
        get => Native.ResolveTarget;
        set => Native.ResolveTarget = value;
    }

    public Silk.NET.WebGPU.LoadOp LoadOp
    {
        get => Native.LoadOp;
        set => Native.LoadOp = value;
    }

    public Silk.NET.WebGPU.StoreOp StoreOp
    {
        get => Native.StoreOp;
        set => Native.StoreOp = value;
    }

    public Silk.NET.WebGPU.Color ClearValue
    {
        get => Native.ClearValue;
        set => Native.ClearValue = value;
    }
}