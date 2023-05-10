using Silk.NET.Core.Native;

namespace Rover656.SilkyWebGPU.Native;

public unsafe class NativePtrArray<T> : IDisposable
    where T : unmanaged
{
    public uint Count;
    internal T** Ptr;
    private readonly bool _owner;

    public static implicit operator NativePtrArray<T>(WebGPUPtr<T>[] arr) => new(arr);

    internal NativePtrArray(uint count, T** ptr)
    {
        _owner = false;
        Count = count;
        Ptr = ptr;
    }

    private NativePtrArray(uint count)
    {
        _owner = true;
        Count = count;
        Ptr = (T**)SilkMarshal.Allocate((int)(sizeof(T*) * count));
    }

    private NativePtrArray(params WebGPUPtr<T>[] values) : this((uint) values.Length)
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