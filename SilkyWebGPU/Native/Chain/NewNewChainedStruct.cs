using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

public abstract unsafe class NewNewChainedStruct : IDisposable
{
    protected internal abstract void* GetPtr();

    protected internal abstract SType GetSType();

    internal abstract NewNewChainedStruct Clone();

    public abstract void Dispose();
}

public abstract unsafe class NewNewChainedStruct<T> : NewNewChainedStruct
    where T : unmanaged
{
    /// <summary>
    /// The native handle for this struct.
    /// </summary>
    internal T* Native;

    private NewNewChainedStruct? _next;

    public NewNewChainedStruct? Next
    {
        get => _next;
        set
        {
            if (value == null)
            {
                ((ChainedStruct*)Native)->Next = null;
                _next?.Dispose();
                _next = null;
                return;
            }
            
            // Set the type
            var nextChained = (ChainedStruct*)value.GetPtr();
            nextChained->SType = value.GetSType();
            
            // Add to the chain.
            ((ChainedStruct*)Native)->Next = nextChained;
            _next = value;
        }
    }

    protected internal NewNewChainedStruct()
    {
        Native = (T*) SilkMarshal.Allocate(Marshal.SizeOf<T>());
    }

    protected internal override void* GetPtr() => Native;
    protected internal override SType GetSType() => ChainHelper.GetSType<T>(default);

    internal T Get()
    {
        return *Native;
    }

    internal Span<T> GetRef()
    {
        return MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>(Native), 1);
    }

    internal ReadOnlySpan<T> GetRefReadonly()
    {
        return MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>(Native), 1);
    }

    protected virtual void ReleaseUnmanagedResources()
    {
        SilkMarshal.Free((nint)Native);
    }

    ~NewNewChainedStruct() => ReleaseUnmanagedResources();

    public override void Dispose()
    {
        _next?.Dispose();
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}