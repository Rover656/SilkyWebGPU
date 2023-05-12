namespace Rover656.SilkyWebGPU;

public unsafe struct WebGPUPtr<T> : IDisposable
    where T : unmanaged
{
    private T* _ptr;

    // TODO: Rename to Weak
    public bool IsWeak { get; }

    private WebGPUPtr(T* ptr, bool weak)
    {
        _ptr = ptr;
        IsWeak = weak;
    }

    [Obsolete(message: "Kept for old generator compat.")]
    internal WebGPUPtr(T* ptr)
    {
        _ptr = ptr;
        IsWeak = false;
    }

    internal static WebGPUPtr<T> MakeStrong(T* ptr)
    {
        return new WebGPUPtr<T>(ptr, false);
    }

    [Obsolete(message: "Kept for old generator compat.")]
    internal static WebGPUPtr<T> Weak(T* ptr)
    {
        return new WebGPUPtr<T>(ptr, true);
    }
    
    internal static WebGPUPtr<T> MakeWeak(T* ptr)
    {
        return new WebGPUPtr<T>(ptr, true);
    }

    // Implicitly convert to base pointer. Makes life a million times easier :D
    public static implicit operator T*(WebGPUPtr<T> ptr) => ptr._ptr;
    public static implicit operator WebGPUPtr<T>(T* ptr) => MakeWeak(ptr);

    public bool IsNull() => _ptr == null;

    [Obsolete(message: "This is being kept for compatibility with old method generator. It implements arrays wrong.")]
    internal ref T* AsRef() => ref _ptr;

    public void Dispose()
    {
        if (_ptr == null)
            return;
        
        if (!IsWeak)
            WebGPU.Dispose(this);
        _ptr = null;
    }
}