﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPTextureDataLayout : WrappedStruct<Silk.NET.WebGPU.TextureDataLayout>
{
    public ulong Offset
    {
        get => Native.Offset;
        set => Native.Offset = value;
    }

    public uint BytesPerRow
    {
        get => Native.BytesPerRow;
        set => Native.BytesPerRow = value;
    }

    public uint RowsPerImage
    {
        get => Native.RowsPerImage;
        set => Native.RowsPerImage = value;
    }
}