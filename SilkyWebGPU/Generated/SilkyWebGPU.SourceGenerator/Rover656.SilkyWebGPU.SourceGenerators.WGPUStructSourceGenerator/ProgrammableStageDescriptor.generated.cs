﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor"/>
public class ProgrammableStageDescriptor : NewNewChainedStruct<Silk.NET.WebGPU.ProgrammableStageDescriptor>
{

    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.Module" />
    public unsafe WebGPUPtr<Silk.NET.WebGPU.ShaderModule> Module
    {
        get => WebGPUPtr<Silk.NET.WebGPU.ShaderModule>.Weak(Native.Module);
        set => Native.Module = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.EntryPoint" />
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
 
    // Keep a copy around for disposal.
    private NativeArray<Silk.NET.WebGPU.ConstantEntry> _Constants;

    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.Constants" />
    /// <remarks>
    /// TODO: Write this remark.
    /// Summary: Will update if you modify the existing pointer, but if you replace it, it won't.
    /// </remarks>
    public unsafe NativeArray<Silk.NET.WebGPU.ConstantEntry> Constants
    {
        // Return a readonly weak copy.
        get => new NativeArray<Silk.NET.WebGPU.ConstantEntry>((uint) Native.ConstantCount, Native.Constants);

        set
        {
            // Dispose any existing object.
            _Constants?.Dispose();

            // Set array
            if (value != null)
            {
                Native.Constants = value.Ptr;
                Native.ConstantCount = value.Count;
            }
            else
            {
                Native.Constants = null;
                Native.ConstantCount = 0;
            }

            // Save for later disposal
            _Constants = value;
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ProgrammableStageDescriptor {{
    EntryPoint = ""{EntryPoint}""
}}";
    }

    public override unsafe void Dispose()
    {
        _Constants?.Dispose();
        _Constants = null;
        base.Dispose();
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.EntryPoint);
        Native.EntryPoint = null;
        base.ReleaseUnmanagedResources();
    }
    internal override ProgrammableStageDescriptor Clone()
    {
        var clone = new ProgrammableStageDescriptor();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
