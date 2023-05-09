using Rover656.SilkyWebGPU.Chain;
using Silk.NET.Core.Contexts;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU;

// Some hand-written extension methods.
public static partial class MethodExtensions
{
    public static unsafe WebGPUPtr<Surface> CreateWebGPUSurface(this INativeWindowSource view, WebGPUPtr<Instance> instance)
    {
        return new WebGPUPtr<Surface>(view.CreateWebGPUSurface(WGPU.API, instance));
    }
    
    public static async Task<WebGPUPtr<Adapter>> RequestAdapter(this WebGPUPtr<Instance> instance,
        RequestAdapterOptions adapterOptions)
    {
        WebGPUPtr<Adapter> adapter = null!;

        using var sem = new SemaphoreSlim(0, 1);
        unsafe
        {
            instance.RequestAdapter(adapterOptions, new PfnRequestAdapterCallback(
                (status, adapter1, message, _) =>
                {
                    // Throw on error so we know what's going on.
                    if (status != RequestAdapterStatus.Success)
                    {
                        throw new NotImplementedException(
                            $"Unable to request adapter with the given options: {SilkMarshal.PtrToString((nint)message)}. Exception type not implemented.");
                    }

                    // Wrap adapter and release semaphore
                    adapter = new WebGPUPtr<Adapter>(adapter1);
                    sem.Release();
                }), null);
        }

        await sem.WaitAsync();
        return adapter;
    }
    
    public static async Task<WebGPUPtr<Device>> RequestDevice(this WebGPUPtr<Adapter> adapter,
        DeviceDescriptor? deviceDescriptor = null)
    {
        WebGPUPtr<Device> device = null!;
        
        using var sem = new SemaphoreSlim(0, 1);
        unsafe
        {
            var callback = new PfnRequestDeviceCallback((_, device1, _, _) =>
            {
                device = new WebGPUPtr<Device>(device1);
                sem.Release();
            });

            adapter.RequestDevice(deviceDescriptor, callback, null);
        }

        await sem.WaitAsync();
        return device;
    }

    public static unsafe FeatureName[] EnumerateFeatures(this WebGPUPtr<Adapter> adapter)
    {
        // Get feature count
        var count = WGPU.API.AdapterEnumerateFeatures(adapter, null);

        // Allocate memory for writing
        Span<FeatureName> span = stackalloc FeatureName[(int)count];
        WGPU.API.AdapterEnumerateFeatures(adapter, span);

        // Copy to array
        var features = new FeatureName[count];
        span.CopyTo(features);
        return features;
    }
}