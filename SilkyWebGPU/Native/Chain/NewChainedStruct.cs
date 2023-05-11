using System.Runtime.InteropServices;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

public abstract unsafe class NewChainedStruct : IDisposable
{
    internal abstract ChainedStruct* Alloc();
    protected internal abstract void Update(ChainedStruct* value);
    
    /// <summary>
    /// Create an empty clone.
    /// This is simply for the purposes of copying the type info without the hassle of references.
    /// </summary>
    /// <returns></returns>
    internal abstract NewChainedStruct Clone();

    public abstract void Dispose();
}

public abstract unsafe class NewChainedStruct<T> : NewChainedStruct, IDisposable
    where T : unmanaged
{
    /// <summary>
    /// The original data structure.
    /// </summary>
    internal T Native;

    private NewChainedStruct? _next;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>When setting, the provided value will be Disposed as it is consumed.</remarks>
    public NewChainedStruct? Next
    {
        get
        {
            // There is no next
            if (_next == null) return null;
            
            // Copy data back from the unmanaged ptr
            fixed (T* native = &Native)
            {
                // Convert to chained.
                var nativeChain = (ChainedStruct*)native;
                
                // Update next along
                _next.Update(nativeChain->Next);
            }
            
            return _next;
        }
        set
        {
            fixed (T* native = &Native)
            {
                // Convert to chained.
                var nativeChain = (ChainedStruct*)native;

                // Free old memory if present
                if (nativeChain->Next != null)
                    ChainHelper.DestroyChained(nativeChain->Next);
                
                // Dispose next if we need to
                _next?.Dispose();
                
                // Allocate the new memory or set to null
                if (value != null)
                {
                    _next = value.Clone();
                    nativeChain->Next = _next.Alloc();
                    value.Dispose();
                }
                else
                {
                    _next = null;
                    nativeChain->Next = null;
                }
            }
        }
    }

    /// <summary>
    /// Get the internal value, with updated chain.
    /// </summary>
    internal T Get()
    {
        // Copy to local context
        var native = Native;
        
        // Update chain
        if (_next != null)
        {
            ChainRead();
        }
        return native;
    }
    
    /// <summary>
    /// Get a reference to the internal value.
    /// </summary>
    internal Span<T> GetRef()
    {
        if (_next != null)
            ChainRead();
        return MemoryMarshal.CreateSpan(ref Native, 1);
    }

    internal ReadOnlySpan<T> GetRefReadonly()
    {
        if (_next != null)
        {
            ChainRead();
        }
        return MemoryMarshal.CreateReadOnlySpan(ref Native, 1);
    }

    /// <summary>
    /// Read any updated information from the chain in memory into the managed wrapper type(s).
    /// </summary>
    private void ChainRead()
    {
        // Update chain
        fixed (T* native = &Native)
        {
            var nativeChained = (ChainedStruct*)native;
            if (nativeChained->Next != null)
                _next?.Update(nativeChained->Next);
        }
    }

    /// <summary>
    /// Allocate this chained struct into a completely new area of memory.
    /// </summary>
    /// <returns></returns>
    internal override ChainedStruct* Alloc()
    {
        // Allocate
        var ptr = ChainHelper.CreateChained(Native);
        
        // Allocate the next in line.
        if (_next != null)
        {
            // Update to ensure the info is recent, then allocate new.
            _next.Update(ptr);
            ptr->Next = _next.Alloc();
        }
        
        // TODO: Override and do further copying per-implementation.

        return ptr;
    }

    protected internal override void Update(ChainedStruct* value)
    {
        // Copy data back from the managed ptr
        if (value != null)
            Native = *(T*)value;
        else Native = default;
        
        // Deal with next in the line
        if (value->Next != null)
            _next?.Update(value->Next);
    }
    
    protected virtual void ReleaseUnmanagedResources()
    {
        // Free the chain!
        fixed (T* native = &Native)
        {
            ChainHelper.DestroyChained((ChainedStruct*)native);
        }
    }

    ~NewChainedStruct() => ReleaseUnmanagedResources();

    public override void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}