using Silk.NET.Core.Native;

namespace Rover656.SilkyWebGPU.Native;

public unsafe class NativePtrArray<T> : IDisposable
    where T : unmanaged
{
    public int Count;
    internal T** Ptr;
    private readonly bool _owner;

    public static implicit operator NativePtrArray<T>(WebGPUPtr<T>[] arr) => new(arr);

    internal NativePtrArray(int count, T** ptr)
    {
        _owner = false;
        Count = count;
        Ptr = ptr;
    }

    private NativePtrArray(int count)
    {
        _owner = true;
        Count = count;
        Ptr = (T**)SilkMarshal.Allocate(sizeof(T*) * count);
    }
    
    public static implicit operator T**(NativePtrArray<T> arr) => arr != null! ? arr.Ptr : null;

    private NativePtrArray(params WebGPUPtr<T>[] values) : this(values.Length)
    {
        var ptr = Ptr;
        foreach (var val in values)
        {
            *ptr = val;
        }
    }

    public T* this[int index]
    {
        get
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            return *(Ptr + index);
        }
    }
    
    private void ReleaseUnmanagedResources()
    {
        if (!_owner || Ptr == null) return;
        SilkMarshal.Free((nint) Ptr);
        Ptr = null;
    }

    ~NativePtrArray() => ReleaseUnmanagedResources();

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}