using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Extensions.WGPU;

internal static class WGPU
{
    private static Wgpu _wgpuApi;

    public static Wgpu WGPUAPI
    {
        get
        {
            if (_wgpuApi == null!)
            {
                unsafe
                {
                   WebGPU.WebGPUAPI.TryGetDeviceExtension(null, out _wgpuApi);
                }
            }

            return _wgpuApi;
        }
    }
}