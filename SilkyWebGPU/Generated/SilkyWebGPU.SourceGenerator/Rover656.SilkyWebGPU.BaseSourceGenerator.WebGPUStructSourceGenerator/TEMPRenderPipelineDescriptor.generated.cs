﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPRenderPipelineDescriptor : WrappedStruct<Silk.NET.WebGPU.RenderPipelineDescriptor>
{
    public unsafe byte* Label
    {
        get => Native.Label;
        set => Native.Label = value;
    }

    public unsafe Silk.NET.WebGPU.PipelineLayout* Layout
    {
        get => Native.Layout;
        set => Native.Layout = value;
    }

    public Silk.NET.WebGPU.VertexState Vertex
    {
        get => Native.Vertex;
        set => Native.Vertex = value;
    }

    public Silk.NET.WebGPU.PrimitiveState Primitive
    {
        get => Native.Primitive;
        set => Native.Primitive = value;
    }

    public unsafe Silk.NET.WebGPU.DepthStencilState* DepthStencil
    {
        get => Native.DepthStencil;
        set => Native.DepthStencil = value;
    }

    public Silk.NET.WebGPU.MultisampleState Multisample
    {
        get => Native.Multisample;
        set => Native.Multisample = value;
    }

    public unsafe Silk.NET.WebGPU.FragmentState* Fragment
    {
        get => Native.Fragment;
        set => Native.Fragment = value;
    }
}