using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Chain;

// TODO: We can now go Managed -> Native. We also want to be able to go back the way.
public abstract class ChainedStruct<T> : ChainingStruct, IManagedChainable<T>
    where T : unmanaged
{
    protected T _native;

    protected internal override unsafe void AddToChain(ChainedStruct* chainedStruct)
    {
        chainedStruct = ChainHelper.AddToChain(chainedStruct, _native, GetSType());
        Next?.AddToChain(chainedStruct);
    }

    private SType GetSType()
    {
        if (typeof(T) == typeof(AdapterExtras))
        {
            return (SType)NativeSType.STypeAdapterExtras;
        }

        return SType.Invalid;
    }

    public unsafe ChainHolder<T> Get()
    {
        // Copy the struct
        var native = _native;
        
        // Perform any chaining
        Next?.AddToChain((ChainedStruct*) &native);
        
        // Return the wrapped chained struct
        return new ChainHolder<T>(native);
    }
}