﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ShaderModuleCompilationHint"/>
public class ShaderModuleCompilationHint : NewNewChainedStruct<Silk.NET.WebGPU.ShaderModuleCompilationHint>
{

    /// <seealso cref="Silk.NET.WebGPU.ShaderModuleCompilationHint.EntryPoint" />
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
 
    /// <seealso cref="Silk.NET.WebGPU.ShaderModuleCompilationHint.Layout" />
    public unsafe WebGPUPtr<Silk.NET.WebGPU.PipelineLayout> Layout
    {
        get => WebGPUPtr<Silk.NET.WebGPU.PipelineLayout>.Weak(Native.Layout);
        set => Native.Layout = value;
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ShaderModuleCompilationHint {{
    EntryPoint = ""{EntryPoint}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.EntryPoint);
        Native.EntryPoint = null;
        base.ReleaseUnmanagedResources();
    }
    internal override ShaderModuleCompilationHint Clone()
    {
        var clone = new ShaderModuleCompilationHint();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
