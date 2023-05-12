﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ConstantEntry"/>
public sealed unsafe class ConstantEntry : ChainedStruct<Silk.NET.WebGPU.ConstantEntry>
{
    public static implicit operator Silk.NET.WebGPU.ConstantEntry(ConstantEntry managed)
        => managed != null ? managed.Get() : default;

    public static implicit operator Span<Silk.NET.WebGPU.ConstantEntry>(ConstantEntry managed)
        => managed != null ? managed.GetRef() : default;

    /// <seealso cref="Silk.NET.WebGPU.ConstantEntry.Key" />
    public string Key
    {
        get => SilkMarshal.PtrToString((nint) Native->Key);
        set
        {
           if (Native->Key != null)
               SilkMarshal.Free((nint) Native->Key);
           Native->Key = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ConstantEntry.Value" />
    public double Value
    {
        get => Native->Value;
        set => Native->Value = value;
    }
 
    public override string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ConstantEntry {{
    Key = ""{Key}""
    Value = ""{Value}""
}}";
    }

    protected override void Dispose(bool disposing)
    {
        if (Disposed) return;
        SilkMarshal.Free((nint) Native->Key);
        Native->Key = null;
        base.Dispose(disposing);
    }
}