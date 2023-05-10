﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

/// <seealso cref="Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor"/>
public class ShaderModuleSPIRVDescriptor : ChainedStruct<Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor>
{

    /// <seealso cref="Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor.CodeSize" />
    public uint CodeSize
    {
        get => Native.CodeSize;
        set => Native.CodeSize = value;
    }
 
    /// <seealso cref="Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor.Code" />
    public unsafe uint? Code
    {
        get
        {
            if (Native.Code == null)
                return null;
            return *Native.Code;
        }

        set
        {
            // If we're setting this to null, wipe the memory.
            if (!value.HasValue)
            {
                SilkMarshal.Free((nint) Native.Code);
                Native.Code = null;
                return;
            }

            // Because we will always own this handle, we allocate if its null, or we overwrite data.
            if (Native.Code == null)
                Native.Code = (uint*) SilkMarshal.Allocate(sizeof(uint));

            // Write new data
            *Native.Code = value.Value;
        }
    }
 
    public override unsafe string ToString()
    {
        // Write anything to the console we deem writable. This might not be accurate but its good enough for debug purposes :)
        return $@"ShaderModuleSPIRVDescriptor {{
    CodeSize = ""{CodeSize}""
}}";
    }

    protected override unsafe void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint) Native.Code);
        Native.Code = null;
    }
}
