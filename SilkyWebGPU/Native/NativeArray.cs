using Rover656.SilkyWebGPU.Native.Chain;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Native;

/// <summary>
/// An immutable unmanaged array.
/// </summary>
/// <typeparam name="T"></typeparam>
public unsafe class NativeArray<T> : IDisposable
    where T : unmanaged
{
    public uint Count { get; }
    internal T* Ptr { get; private set; }
    private readonly bool _chainable;
    private readonly NewChainedStruct<T>[]? _chainedStructs;
    private readonly WrappedStruct<T>[]? _wrappedStructs;
    private readonly bool _owner;

    // public static implicit operator NativeArray<T>(ChainedStruct<T>[] arr) => new(arr);
    public static implicit operator NativeArray<T>(NewChainedStruct<T>[] arr) => new(arr);
    public static implicit operator NativeArray<T>(WrappedStruct<T>[] arr) => new(arr);
    public static implicit operator NativeArray<T>(T[] arr) => new(arr);

    internal NativeArray(uint count, T* ptr)
    {
        _owner = false;
        Count = count;
        Ptr = ptr;
    }

    private NativeArray(uint count)
    {
        _owner = true;
        Count = count;
        Ptr = (T*)SilkMarshal.Allocate((int)(sizeof(T) * count));
    }

    public NativeArray(params NewChainedStruct<T>[] values) : this((uint)values.Length)
    {
        _chainable = true;
        _chainedStructs = values;
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val.Get();
            ptr++;
        }
    }

    public NativeArray(params WrappedStruct<T>[] values) : this((uint)values.Length)
    {
        _chainable = false;
        _wrappedStructs = values;
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val;
            ptr++;
        }
    }

    public NativeArray(params T[] values) : this((uint)values.Length)
    {
        _chainable = false;
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val;
            ptr++;
        }
    }

    public T this[int index]
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
        var ptr = Ptr;
        if (_chainable)
        {
            for (var i = 0; i < Count; i++)
            {
                ChainHelper.FreeChain((ChainedStruct*)ptr);
                ptr++;
            }
        }

        SilkMarshal.Free((nint)Ptr);
        Ptr = null;
    }

    ~NativeArray() => ReleaseUnmanagedResources();

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        if (_chainedStructs != null)
        {
            foreach (var chainedStruct in _chainedStructs)
            {
                chainedStruct.Dispose();
            }
        }
        else if (_wrappedStructs != null)
        {
            foreach (var wrappedStruct in _wrappedStructs)
            {
                wrappedStruct.Dispose();
            }
        }

        GC.SuppressFinalize(this);
    }
}