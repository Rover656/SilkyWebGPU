using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

// TODO: We can now go Managed -> Native. We also want to be able to go back the way.
public abstract class ChainedStruct<T> : ChainingStruct, IManagedChainable<T>
    where T : unmanaged
{
    protected T Native;

    protected internal override unsafe void AddToChain(ChainedStruct* chainedStruct)
    {
        chainedStruct = ChainHelper.AddToChain(chainedStruct, Native, ChainHelper.GetSType(this));
        Next?.AddToChain(chainedStruct);
    }

    public unsafe ChainHolder<T> Get()
    {
        // Copy the struct
        var native = Native;
        
        // Perform any chaining
        Next?.AddToChain((ChainedStruct*) &native);
        
        // Return the wrapped chained struct
        return new ChainHolder<T>(native);
    }
}