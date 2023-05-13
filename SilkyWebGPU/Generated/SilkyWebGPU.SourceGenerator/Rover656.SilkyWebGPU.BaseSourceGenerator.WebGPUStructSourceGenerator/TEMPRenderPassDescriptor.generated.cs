﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPRenderPassDescriptor : WrappedStruct<Silk.NET.WebGPU.RenderPassDescriptor>
{
    public unsafe byte* Label
    {
        get => Native.Label;
        set => Native.Label = value;
    }

    public uint ColorAttachmentCount
    {
        get => Native.ColorAttachmentCount;
        set => Native.ColorAttachmentCount = value;
    }

    public unsafe Silk.NET.WebGPU.RenderPassColorAttachment* ColorAttachments
    {
        get => Native.ColorAttachments;
        set => Native.ColorAttachments = value;
    }

    public unsafe Silk.NET.WebGPU.RenderPassDepthStencilAttachment* DepthStencilAttachment
    {
        get => Native.DepthStencilAttachment;
        set => Native.DepthStencilAttachment = value;
    }

    public unsafe Silk.NET.WebGPU.QuerySet* OcclusionQuerySet
    {
        get => Native.OcclusionQuerySet;
        set => Native.OcclusionQuerySet = value;
    }

    public uint TimestampWriteCount
    {
        get => Native.TimestampWriteCount;
        set => Native.TimestampWriteCount = value;
    }

    public unsafe Silk.NET.WebGPU.RenderPassTimestampWrite* TimestampWrites
    {
        get => Native.TimestampWrites;
        set => Native.TimestampWrites = value;
    }
}