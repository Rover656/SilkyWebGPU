using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Extensions.WGPU;

internal static class WGPU
{
    private static Wgpu _wgpuApi = null!;

    public static Wgpu WGPUAPI
    {
        get
        {
            if (_wgpuApi != null!) return _wgpuApi;
            
            unsafe
            {
                WebGPU.WebGPUAPI.TryGetDeviceExtension(null, out _wgpuApi);
            }

            if (_wgpuApi == null!)
            {
                throw new NotImplementedException("WGPU Extension is not available.");
            }

            return _wgpuApi;
        }
    }
}