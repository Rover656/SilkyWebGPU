using Rover656.SilkyWebGPU.Chain;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU;

public unsafe class NativeChainableArray<T> : IDisposable
    where T : unmanaged
{
    public uint Count { get; }

    internal T* Ptr { get; }

    public static NativeChainableArray<T> Of(params ChainedStruct<T>[] values) => new(values);

    public static implicit operator NativeChainableArray<T>(ChainedStruct<T>[] arr) => new(arr);

    public NativeChainableArray(uint count)
    {
        Count = count;
        Ptr = (T*)SilkMarshal.Allocate((int) (sizeof(T) * count));
    }

    public NativeChainableArray(params ChainedStruct<T>[] values) : this((uint) values.Length)
    {
        // Copy into memory
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val.GetWithChain();
            ptr++;
        }
    }

    public ChainedStruct<T> this[int index]
    {
        // TODO: Do we need to do getter?
        set
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();

            // Get the array element
            var val = Ptr + index;
            
            // Free it's allocated chain
            ChainHelper.FreeChain((ChainedStruct*)val);
            
            // Update value with new chain
            *val = value.GetWithChain();
        }
    }

    private void ReleaseUnmanagedResources()
    {
        var ptr = Ptr;
        for (var i = 0; i < Count; i++)
        {
            ChainHelper.FreeChain((ChainedStruct*)ptr);
            ptr++;
        }
        SilkMarshal.Free((nint) Ptr);
    }

    ~NativeChainableArray() => ReleaseUnmanagedResources();

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}