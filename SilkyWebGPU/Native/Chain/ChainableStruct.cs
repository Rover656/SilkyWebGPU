using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

public unsafe interface IChainableStruct : IDisposable
{
    internal ChainedStruct* GetPtr();
}

public unsafe class ChainableStruct<T> : IChainableStruct, INativeWrapper<T>
    where T : unmanaged
{
    /// <summary>
    /// The native handle for this struct.
    /// </summary>
    internal T* Native;

    /// <summary>
    /// The next struct in the chain.
    /// </summary>
    private IChainedStruct? _next;

    protected bool Disposed;

    /// <summary>
    /// The next struct in the chain.
    /// </summary>
    public IChainedStruct? Next
    {
        get => _next;
        set
        {
            // Dispose the old next in chain
            _next?.Dispose();
            
            // Add to the chain.
            ((Silk.NET.WebGPU.ChainedStruct*)Native)->Next = value != null ? value.GetPtr() : null;
            _next = value;
            
            // TEMP
            if (value != null)
                value.GetPtr()->SType = value.GetSType();
        }
    }

    protected ChainableStruct()
    {
        // Allocate and default memory for the struct to be stored in.
        Native = (T*) SilkMarshal.Allocate(Marshal.SizeOf<T>());
        *Native = default;
    }

    public T Get()
    {
        return *Native;
    }
    
    public Span<T> GetRef()
    {
        return MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>(Native), 1);
    }

    public ReadOnlySpan<T> GetRefReadonly()
    {
        return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef<T>(Native), 1);
    }

    public ChainedStruct* GetPtr()
    {
        return (ChainedStruct*)Native;
    }

    ~ChainableStruct() => Dispose(false);
    
    /*
     * Could add this to destructor:
     * if (System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debug.Fail("Undisposed chained struct.");            
        }
     */

    public void Dispose()
    {
        // TODO: Solve this...
        if (Disposed || Native == null) // Shouldn't be necessary, but weird bug :/
            return;
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed) return;
        if (disposing)
        {
            _next?.Dispose();
            _next = null;
        }

        SilkMarshal.Free((nint)Native);
        Native = null;
        Disposed = true;
    }
}