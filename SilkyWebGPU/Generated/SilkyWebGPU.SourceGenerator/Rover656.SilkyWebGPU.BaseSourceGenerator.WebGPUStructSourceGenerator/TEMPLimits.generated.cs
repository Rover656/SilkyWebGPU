﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPLimits : WrappedStruct<Silk.NET.WebGPU.Limits>
{
    public uint MaxTextureDimension1D
    {
        get => Native.MaxTextureDimension1D;
        set => Native.MaxTextureDimension1D = value;
    }

    public uint MaxTextureDimension2D
    {
        get => Native.MaxTextureDimension2D;
        set => Native.MaxTextureDimension2D = value;
    }

    public uint MaxTextureDimension3D
    {
        get => Native.MaxTextureDimension3D;
        set => Native.MaxTextureDimension3D = value;
    }

    public uint MaxTextureArrayLayers
    {
        get => Native.MaxTextureArrayLayers;
        set => Native.MaxTextureArrayLayers = value;
    }

    public uint MaxBindGroups
    {
        get => Native.MaxBindGroups;
        set => Native.MaxBindGroups = value;
    }

    public uint MaxBindingsPerBindGroup
    {
        get => Native.MaxBindingsPerBindGroup;
        set => Native.MaxBindingsPerBindGroup = value;
    }

    public uint MaxDynamicUniformBuffersPerPipelineLayout
    {
        get => Native.MaxDynamicUniformBuffersPerPipelineLayout;
        set => Native.MaxDynamicUniformBuffersPerPipelineLayout = value;
    }

    public uint MaxDynamicStorageBuffersPerPipelineLayout
    {
        get => Native.MaxDynamicStorageBuffersPerPipelineLayout;
        set => Native.MaxDynamicStorageBuffersPerPipelineLayout = value;
    }

    public uint MaxSampledTexturesPerShaderStage
    {
        get => Native.MaxSampledTexturesPerShaderStage;
        set => Native.MaxSampledTexturesPerShaderStage = value;
    }

    public uint MaxSamplersPerShaderStage
    {
        get => Native.MaxSamplersPerShaderStage;
        set => Native.MaxSamplersPerShaderStage = value;
    }

    public uint MaxStorageBuffersPerShaderStage
    {
        get => Native.MaxStorageBuffersPerShaderStage;
        set => Native.MaxStorageBuffersPerShaderStage = value;
    }

    public uint MaxStorageTexturesPerShaderStage
    {
        get => Native.MaxStorageTexturesPerShaderStage;
        set => Native.MaxStorageTexturesPerShaderStage = value;
    }

    public uint MaxUniformBuffersPerShaderStage
    {
        get => Native.MaxUniformBuffersPerShaderStage;
        set => Native.MaxUniformBuffersPerShaderStage = value;
    }

    public ulong MaxUniformBufferBindingSize
    {
        get => Native.MaxUniformBufferBindingSize;
        set => Native.MaxUniformBufferBindingSize = value;
    }

    public ulong MaxStorageBufferBindingSize
    {
        get => Native.MaxStorageBufferBindingSize;
        set => Native.MaxStorageBufferBindingSize = value;
    }

    public uint MinUniformBufferOffsetAlignment
    {
        get => Native.MinUniformBufferOffsetAlignment;
        set => Native.MinUniformBufferOffsetAlignment = value;
    }

    public uint MinStorageBufferOffsetAlignment
    {
        get => Native.MinStorageBufferOffsetAlignment;
        set => Native.MinStorageBufferOffsetAlignment = value;
    }

    public uint MaxVertexBuffers
    {
        get => Native.MaxVertexBuffers;
        set => Native.MaxVertexBuffers = value;
    }

    public ulong MaxBufferSize
    {
        get => Native.MaxBufferSize;
        set => Native.MaxBufferSize = value;
    }

    public uint MaxVertexAttributes
    {
        get => Native.MaxVertexAttributes;
        set => Native.MaxVertexAttributes = value;
    }

    public uint MaxVertexBufferArrayStride
    {
        get => Native.MaxVertexBufferArrayStride;
        set => Native.MaxVertexBufferArrayStride = value;
    }

    public uint MaxInterStageShaderComponents
    {
        get => Native.MaxInterStageShaderComponents;
        set => Native.MaxInterStageShaderComponents = value;
    }

    public uint MaxInterStageShaderVariables
    {
        get => Native.MaxInterStageShaderVariables;
        set => Native.MaxInterStageShaderVariables = value;
    }

    public uint MaxColorAttachments
    {
        get => Native.MaxColorAttachments;
        set => Native.MaxColorAttachments = value;
    }

    public uint MaxColorAttachmentBytesPerSample
    {
        get => Native.MaxColorAttachmentBytesPerSample;
        set => Native.MaxColorAttachmentBytesPerSample = value;
    }

    public uint MaxComputeWorkgroupStorageSize
    {
        get => Native.MaxComputeWorkgroupStorageSize;
        set => Native.MaxComputeWorkgroupStorageSize = value;
    }

    public uint MaxComputeInvocationsPerWorkgroup
    {
        get => Native.MaxComputeInvocationsPerWorkgroup;
        set => Native.MaxComputeInvocationsPerWorkgroup = value;
    }

    public uint MaxComputeWorkgroupSizeX
    {
        get => Native.MaxComputeWorkgroupSizeX;
        set => Native.MaxComputeWorkgroupSizeX = value;
    }

    public uint MaxComputeWorkgroupSizeY
    {
        get => Native.MaxComputeWorkgroupSizeY;
        set => Native.MaxComputeWorkgroupSizeY = value;
    }

    public uint MaxComputeWorkgroupSizeZ
    {
        get => Native.MaxComputeWorkgroupSizeZ;
        set => Native.MaxComputeWorkgroupSizeZ = value;
    }

    public uint MaxComputeWorkgroupsPerDimension
    {
        get => Native.MaxComputeWorkgroupsPerDimension;
        set => Native.MaxComputeWorkgroupsPerDimension = value;
    }
}