using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

/**
 * Helper methods for dealing with chains.
 */
public static class ChainHelper
{
    /**
     * Incredibly unsafe if used incorrectly.
     * <typeparam name="T">Must be a chainable struct.</typeparam>
     */
    public static unsafe ChainedStruct* AddToChain<T>(ChainedStruct* self, T toAdd, SType sType) where T : unmanaged
    {
        // Allocate memory for this piece to go.
        var allocatedMem = SilkMarshal.Allocate(sizeof(T));
        Unsafe.CopyBlockUnaligned((void*)allocatedMem, &toAdd, (uint) sizeof(T));

        // Set SType.
        var chain = (ChainedStruct*)allocatedMem;
        chain->SType = sType;

        // Add as the next in the chain.
        self->Next = (ChainedStruct*)allocatedMem;

        return chain;
    }

    /**
     * Free the allocated memory of a chained struct.
     * This will not free the main chain, as that is normally managed by C#
     */
    public static unsafe void FreeChain(ChainedStruct* chain)
    {
        if (chain == null)
            return;
        
        // Wipe out the chain.
        var nextChain = chain->Next;
        while (nextChain != null)
        {
            var next = nextChain->Next;
            SilkMarshal.Free((nint) nextChain);
            nextChain = next;
        }
        
        // Finally null it to ensure we don't try freeing randomly
        chain->Next = null;
    }
}