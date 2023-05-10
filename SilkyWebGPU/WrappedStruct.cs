namespace Rover656.SilkyWebGPU;

public abstract class WrappedStruct<T> : IDisposable
    where T : unmanaged
{
    /// <summary>
    /// The wrapped native structure.
    /// </summary>
    internal T Native;

    public static implicit operator T(WrappedStruct<T> wrapped) => wrapped.Native;

    /// <summary>
    /// Release native resources required by the structure.
    /// </summary>
    protected virtual void ReleaseUnmanagedResources()
    {
    }

    ~WrappedStruct() => ReleaseUnmanagedResources();

    public virtual void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}