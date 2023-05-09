﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.RenderBundleDescriptor"/>
public class ManagedRenderBundleDescriptor : ChainedStruct<Silk.NET.WebGPU.RenderBundleDescriptor>
{

    /// <seealso cref="Silk.NET.WebGPU.RenderBundleDescriptor.Label" />
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
        return $@"RenderBundleDescriptor {{
    Label = ""{Label}""
}}";
    }

    public override unsafe void Dispose()
    {
        base.Dispose();
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.Label);
    }
}
