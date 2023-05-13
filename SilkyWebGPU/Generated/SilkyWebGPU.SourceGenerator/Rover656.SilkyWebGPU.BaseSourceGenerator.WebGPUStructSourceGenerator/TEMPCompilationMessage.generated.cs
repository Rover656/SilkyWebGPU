﻿// <auto-generated/>

using Rover656.SilkyWebGPU;
using Rover656.SilkyWebGPU.Native;
using Rover656.SilkyWebGPU.Native.Chain;

using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU;

public sealed class TEMPCompilationMessage : WrappedStruct<Silk.NET.WebGPU.CompilationMessage>
{
    public unsafe byte* Message
    {
        get => Native.Message;
        set => Native.Message = value;
    }

    public Silk.NET.WebGPU.CompilationMessageType Type
    {
        get => Native.Type;
        set => Native.Type = value;
    }

    public ulong LineNum
    {
        get => Native.LineNum;
        set => Native.LineNum = value;
    }

    public ulong LinePos
    {
        get => Native.LinePos;
        set => Native.LinePos = value;
    }

    public ulong Offset
    {
        get => Native.Offset;
        set => Native.Offset = value;
    }

    public ulong Length
    {
        get => Native.Length;
        set => Native.Length = value;
    }

    public ulong Utf16LinePos
    {
        get => Native.Utf16LinePos;
        set => Native.Utf16LinePos = value;
    }

    public ulong Utf16Offset
    {
        get => Native.Utf16Offset;
        set => Native.Utf16Offset = value;
    }

    public ulong Utf16Length
    {
        get => Native.Utf16Length;
        set => Native.Utf16Length = value;
    }
}