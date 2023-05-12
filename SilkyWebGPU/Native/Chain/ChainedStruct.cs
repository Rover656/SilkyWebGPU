using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

public interface IChainedStruct : IChainableStruct
{
    protected internal SType GetSType();
}

public abstract unsafe class ChainedStruct<T> : ChainableStruct<T>, IChainedStruct
    where T : unmanaged
{
    protected internal ChainedStruct()
    {
        // TODO: Once we generate only the chained structs to inherit this, we can re-enable this line.
        // ((ChainedStruct*)Native)->SType = ((IChainedStruct)this).GetSType();
    }

    SType IChainedStruct.GetSType()
    {
        return ChainHelper.GetSType<T>(default);
    }
}