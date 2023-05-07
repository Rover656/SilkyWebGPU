namespace Rover656.SilkyWebGPU.Chain;

/**
 * A descriptor which is safe to use.
 * This can be passed to WebGPU APIs and it will be created and disposed of correctly.
 */
public interface IManagedChainable<T> where T : unmanaged
{
    /**
     * You must dispose of the descriptor immediately after use.
     */
    internal ChainHolder<T> Get();
}