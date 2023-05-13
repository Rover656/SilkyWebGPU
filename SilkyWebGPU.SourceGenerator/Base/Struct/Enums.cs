using System;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public enum NativeAccessor
{
    Pointer,
    Value,
}

public static class EnumMethods
{
    public static string GetAccessor(this NativeAccessor accessor)
    {
        return accessor switch
        {
            NativeAccessor.Pointer => "->",
            NativeAccessor.Value => ".",
            _ => throw new ArgumentOutOfRangeException(nameof(accessor), accessor, null)
        };
    }
}