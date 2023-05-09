﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras"/>
public class ManagedInstanceExtras : ChainedStruct<Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras>
{

    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras.Backends" />
    public uint Backends
    {
        get => Native.Backends;
        set => Native.Backends = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras.Dx12ShaderCompiler" />
    public Silk.NET.WebGPU.Extensions.WGPU.Dx12Compiler Dx12ShaderCompiler
    {
        get => Native.Dx12ShaderCompiler;
        set => Native.Dx12ShaderCompiler = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras.DxilPath" />
    public unsafe string DxilPath
    {
        get => SilkMarshal.PtrToString((nint) Native.DxilPath);
        set
       {
           if (Native.DxilPath != null)
               SilkMarshal.Free((nint) Native.DxilPath);
           Native.DxilPath = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras.DxcPath" />
    public unsafe string DxcPath
    {
        get => SilkMarshal.PtrToString((nint) Native.DxcPath);
        set
       {
           if (Native.DxcPath != null)
               SilkMarshal.Free((nint) Native.DxcPath);
           Native.DxcPath = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"InstanceExtras {{
    Backends = ""{Backends}""
    Dx12ShaderCompiler = ""{Dx12ShaderCompiler}""
    DxilPath = ""{DxilPath}""
    DxcPath = ""{DxcPath}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.DxilPath);
        SilkMarshal.Free((nint) Native.DxcPath);
    }
}
