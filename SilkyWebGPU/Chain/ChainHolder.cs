using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU.Chain;

/**
 * This class wraps ChainedStructs to ensure that they are freed.
 * This should only be used with structs that have been chained by ChainHelper.
 */
public class ChainHolder<T> : IDisposable 
    where T : unmanaged
{
    private T _chainable;

    internal ChainHolder(T chainable)
    {
        _chainable = chainable;
    }
    
    public static implicit operator T(ChainHolder<T> ptr) => ptr._chainable;

    ~ChainHolder()
    {
        Console.WriteLine("It happened.");
        ReleaseUnmanagedResources();
    }

    private unsafe void ReleaseUnmanagedResources()
    {
        fixed (T* descriptorPtr = &_chainable)
        {
            ChainHelper.FreeChain((ChainedStruct*)descriptorPtr);
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}