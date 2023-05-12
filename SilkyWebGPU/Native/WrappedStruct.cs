namespace Rover656.SilkyWebGPU.Native;

public abstract class WrappedStruct<T> : INativeWrapper<T>
    where T : unmanaged
{
    /// <summary>
    /// The wrapped native structure.
    /// </summary>
    internal T Native;

    protected bool Disposed;
    
    internal WrappedStruct()
    {
    }
    
    internal WrappedStruct(T native)
    {
        Native = native;
    }

    public static implicit operator T(WrappedStruct<T> wrapped) => wrapped.Native;

    public T Get()
    {
        return Native;
    }
    
    ~WrappedStruct() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Disposed = true;
    }
}