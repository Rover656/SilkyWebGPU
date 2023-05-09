using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

/**
 * A helper class for a struct that is chainable.
 * We needed a base type without a generic so we could have the Next field.
 * Unsure if there's a solution that removes this need.
 */
public abstract class ChainableStruct
{
    /**
     * The next struct in the chain. 
     */
    public ChainableStruct? Next = null;
    
    protected internal abstract unsafe void AddToChain(ChainedStruct *chainedStruct);

    /// <summary>
    /// Any use of this method MUST ensure that the native pointer is of the correct type.
    /// If it is not, really bad things will happen!
    /// </summary>
    /// <param name="native"></param>
    internal abstract unsafe void Mutate(ChainedStruct* native);
}
