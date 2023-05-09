﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.VertexState"/>
public class ManagedVertexState : ChainedStruct<Silk.NET.WebGPU.VertexState>
{

    /// <seealso cref="Silk.NET.WebGPU.VertexState.Module" />
    public unsafe WebGPUPtr<Silk.NET.WebGPU.ShaderModule> Module
    {
        get => WebGPUPtr<Silk.NET.WebGPU.ShaderModule>.Weak(Native.Module);
        set => Native.Module = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.VertexState.EntryPoint" />
    public unsafe string EntryPoint
    {
        get => SilkMarshal.PtrToString((nint) Native.EntryPoint);
        set
       {
           if (Native.EntryPoint != null)
               SilkMarshal.Free((nint) Native.EntryPoint);
           Native.EntryPoint = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.VertexState.ConstantCount" />
    public uint ConstantCount
    {
        get => Native.ConstantCount;
        set => Native.ConstantCount = value;
    }
 
    /// <summary>
    /// This is a currently unsupported type.
    /// Native type: Silk.NET.WebGPU.ConstantEntry*.
    /// Original name: Constants.
    /// Is array type?: True.
    /// </summary>
    /// <seealso cref="Silk.NET.WebGPU.VertexState.Constants" />
    public unsafe Silk.NET.WebGPU.ConstantEntry* Constants
    {
        get => Native.Constants;
        set => Native.Constants = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.VertexState.BufferCount" />
    public uint BufferCount
    {
        get => Native.BufferCount;
        set => Native.BufferCount = value;
    }
 
    /// <summary>
    /// This is a currently unsupported type.
    /// Native type: Silk.NET.WebGPU.VertexBufferLayout*.
    /// Original name: Buffers.
    /// Is array type?: True.
    /// </summary>
    /// <seealso cref="Silk.NET.WebGPU.VertexState.Buffers" />
    public unsafe Silk.NET.WebGPU.VertexBufferLayout* Buffers
    {
        get => Native.Buffers;
        set => Native.Buffers = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"VertexState {{
    EntryPoint = ""{EntryPoint}""
    ConstantCount = ""{ConstantCount}""
    BufferCount = ""{BufferCount}""
}}";
    }

    public override unsafe void Dispose()
    {
        base.Dispose();
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.EntryPoint);
    }
}