﻿namespace Rover656.SilkyWebGPU;

/// <summary>
/// Web GPU Native Type.
/// This encompasses a native pointer from the WebGPU API.
/// This allows extensions methods to breathe life into these pointers without hand-writing entire classes.
/// Disposal is handled by <see cref="WGPU"/>.
/// </summary>
/// <typeparam name="T">The type being wrapped</typeparam>
public unsafe class WebGPUPtr<T> : IDisposable
    where T : unmanaged
{
    private T* _ptr;
    private bool _weak;

    // TODO: Make internal again?
    public WebGPUPtr(T* ptr)
    {
        _ptr = ptr;
        _weak = false;
    }

    internal static WebGPUPtr<T> Weak(T* ptr)
    {
        var weak = new WebGPUPtr<T>(ptr);
        weak._weak = true;
        return weak;
    }

    public static implicit operator T*(WebGPUPtr<T> ptr) => ptr._ptr;
    
    public ref T* AsRef()
    {
        return ref _ptr;
    }

    public bool IsNull()
    {
        return _ptr == null;
    }

    public bool IsWeak()
    {
        return _weak;
    }

    // TODO: Investigate if this is a bad idea
    //       It feels like a good idea to ensure its always disposed of, but it might cause unintended side effects.
    // ~WebGpuPtr() => ReleaseUnmanagedResources();
    
    private void ReleaseUnmanagedResources()
    {
        if (!_weak)
            WGPU.Dispose(this);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        // GC.SuppressFinalize(this);
    }
}