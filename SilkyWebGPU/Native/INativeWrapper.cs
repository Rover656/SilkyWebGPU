namespace Rover656.SilkyWebGPU.Native;

public interface INativeWrapper<out T> : IDisposable
    where T : unmanaged
{
    public T Get();
}