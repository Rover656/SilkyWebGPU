using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

public abstract class ChainingStruct
{
    public ChainingStruct? Next = null;
    protected internal abstract unsafe void AddToChain(ChainedStruct *chainedStruct);
}