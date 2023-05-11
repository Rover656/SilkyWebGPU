﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.QueueDescriptor"/>
public class QueueDescriptor : NewNewChainedStruct<Silk.NET.WebGPU.QueueDescriptor>
{

    /// <seealso cref="Silk.NET.WebGPU.QueueDescriptor.Label" />
    public unsafe string Label
    {
        get => SilkMarshal.PtrToString((nint) Native.Label);

        set
       {
           if (Native.Label != null)
               SilkMarshal.Free((nint) Native.Label);
           Native.Label = (byte *) SilkMarshal.StringToPtr(value);
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"QueueDescriptor {{
    Label = ""{Label}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.Label);
        Native.Label = null;
        base.ReleaseUnmanagedResources();
    }
    internal override QueueDescriptor Clone()
    {
        var clone = new QueueDescriptor();
        clone.Native = Native;
        clone.Next = Next;
        return clone;
    }
}
