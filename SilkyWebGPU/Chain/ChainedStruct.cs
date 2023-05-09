using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

public abstract class ChainedStruct<T> : ChainableStruct
    where T : unmanaged
{
    internal T Native;

    internal unsafe T GetWithChain()
    {
        // Copy the struct so we don't modify underlying
        var nativeCopy = Native;
        
        // Build the chain
        Next?.AddToChain((ChainedStruct*) &nativeCopy);

        // Return the copy.
        return nativeCopy;
    }

    // TODO: Maybe just make this a ChainHelper method.
    // It doesn't need to be an instance method at all.
    internal unsafe void FreeChain(ref T copy)
    {
        fixed (void* chained = &copy)
        {
            ChainHelper.FreeChain((ChainedStruct*) chained);
        }
    }

    /**
     * Allocate an unmanaged pointer containing the chained struct.
     * This needs to be freed manually.
     */
    internal unsafe T* Alloc()
    {
        // Allocate and copy
        var native = SilkMarshal.Allocate(sizeof(T));
        fixed (void* nativePtr = &Native)
        {
            Unsafe.CopyBlockUnaligned((void*)native, nativePtr, (uint) sizeof(T));
        }
        
        // Allocate the chain
        if (Next != null)
            Next.AddToChain((ChainedStruct*)native);
        else ((ChainedStruct*)native)->Next = null; // Ensure no old pointers are left lying around
        
        // Return pointer :D
        return (T*)native;
    }

    internal override unsafe void Mutate(ChainedStruct* native)
    {
        // Ignore nulls
        if (native == null)
            return;
        
        // We can just set Native to the value at the pointer.
        // You might think this is a bad idea considering it'll hold non-existant pointers, but we're wrapping the type.
        // Whenever a pointer to this is acquired, the old pointer is overwritten before its given to anything, so no risk.
        // To ensure this is the case, if Next somehow becomes null in the lifetime, we'll also set Next to null.
        Native = *(T*)native;
        
        // Now we do the same again on the chain
        if (Next != null && native->Next != null)
        {
            Next.Mutate(native->Next);
        }
    }

    internal unsafe void Free(T* native)
    {
        // Ignore nulls
        if (native == null)
            return;
        
        // Free any chained memory
        ChainHelper.FreeChain((ChainedStruct*)native);
        
        // Now free the main object
        SilkMarshal.Free((nint) native);
    }

    protected internal override unsafe void AddToChain(ChainedStruct* chainedStruct)
    {
        // Add self to chain
        chainedStruct = ChainHelper.AddToChain(chainedStruct, Native, ChainHelper.GetSType(this));
        
        // Add next to self
        Next?.AddToChain(chainedStruct);
    }
}