﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor"/>
public sealed unsafe class ProgrammableStageDescriptor : ChainedStruct<Silk.NET.WebGPU.ProgrammableStageDescriptor>
{
    public static implicit operator Silk.NET.WebGPU.ProgrammableStageDescriptor(ProgrammableStageDescriptor managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.ProgrammableStageDescriptor>(ProgrammableStageDescriptor managed)
        => managed != null ? managed.GetRef() : default;

    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.Module" />
    public WebGPUPtr<Silk.NET.WebGPU.ShaderModule> Module
    {
        get => WebGPUPtr<Silk.NET.WebGPU.ShaderModule>.Weak(Native->Module);
        set => Native->Module = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.EntryPoint" />
    public string EntryPoint
    {
        get => SilkMarshal.PtrToString((nint) Native->EntryPoint);
        set
        {
           if (Native->EntryPoint != null)
               SilkMarshal.Free((nint) Native->EntryPoint);
           Native->EntryPoint = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    // Keep a copy around for disposal.
    private NativeArray<Silk.NET.WebGPU.ConstantEntry> _Constants;

    /// <seealso cref="Silk.NET.WebGPU.ProgrammableStageDescriptor.Constants" />
    /// <remarks>
    /// TODO: Write this remark.
    /// Summary: Will update if you modify the existing pointer, but if you replace it, it won't.
    /// </remarks>
    public NativeArray<Silk.NET.WebGPU.ConstantEntry> Constants
    {
        // Return a readonly weak copy.
        get => new NativeArray<Silk.NET.WebGPU.ConstantEntry>((int) Native->ConstantCount, Native->Constants);

        set
        {
            // Dispose any existing object.
            _Constants?.Dispose();

            // Set array
            if (value != null)
            {
                Native->Constants = value.Ptr;
                Native->ConstantCount = (UInt32) value.Count;
            }
            else
            {
                Native->Constants = null;
                Native->ConstantCount = 0;
            }

            // Save for later disposal
            _Constants = value;
        }
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ProgrammableStageDescriptor {{
    EntryPoint = ""{EntryPoint}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        if (disposing)
        {
            _Constants?.Dispose();
            _Constants = null;
        }

        SilkMarshal.Free((nint) Native->EntryPoint);
        Native->EntryPoint = null;
        base.Dispose(disposing);
    }
}
