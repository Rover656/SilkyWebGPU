﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.FragmentState"/>
public class ManagedFragmentState : ChainedStruct<Silk.NET.WebGPU.FragmentState>
{

    /// <seealso cref="Silk.NET.WebGPU.FragmentState.Module" />
    public unsafe WebGPUPtr<Silk.NET.WebGPU.ShaderModule> Module
    {
        get => WebGPUPtr<Silk.NET.WebGPU.ShaderModule>.Weak(Native.Module);
        set => Native.Module = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.FragmentState.EntryPoint" />
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
 
    /// <seealso cref="Silk.NET.WebGPU.FragmentState.ConstantCount" />
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
    /// <seealso cref="Silk.NET.WebGPU.FragmentState.Constants" />
    public unsafe Silk.NET.WebGPU.ConstantEntry* Constants
    {
        get => Native.Constants;
        set => Native.Constants = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.FragmentState.TargetCount" />
    public uint TargetCount
    {
        get => Native.TargetCount;
        set => Native.TargetCount = value;
    }
 
    /// <summary>
    /// This is a currently unsupported type.
    /// Native type: Silk.NET.WebGPU.ColorTargetState*.
    /// Original name: Targets.
    /// Is array type?: True.
    /// </summary>
    /// <seealso cref="Silk.NET.WebGPU.FragmentState.Targets" />
    public unsafe Silk.NET.WebGPU.ColorTargetState* Targets
    {
        get => Native.Targets;
        set => Native.Targets = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"FragmentState {{
    EntryPoint = ""{EntryPoint}""
    ConstantCount = ""{ConstantCount}""
    TargetCount = ""{TargetCount}""
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
