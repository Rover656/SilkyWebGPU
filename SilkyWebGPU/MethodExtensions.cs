﻿using System.Runtime.CompilerServices;
using Silk.NET.Core.Contexts;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;

namespace Rover656.SilkyWebGPU;

// Some hand-written extension methods.
public static partial class MethodExtensions
{
    public static unsafe WebGPUPtr<Surface> CreateWebGPUSurface(this INativeWindowSource view, WebGPUPtr<Instance> instance)
    {
        return new WebGPUPtr<Surface>(view.CreateWebGPUSurface(WebGPU.WebGPUAPI, instance));
    }
    
    public static WebGPUPtr<Adapter> RequestAdapter(this WebGPUPtr<Instance> instance,
        RequestAdapterOptions adapterOptions)
    {
        WebGPUPtr<Adapter> adapter = default;

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

                    // Wrap adapter
                    adapter = WebGPUPtr<Adapter>.MakeStrong(adapter1);
                }), ref Unsafe.NullRef<int>()); // TODO: Don't do this once we fix userdatum optionality
        }

        return adapter;
    }
    
    public static WebGPUPtr<Device> RequestDevice(this WebGPUPtr<Adapter> adapter,
        DeviceDescriptor? deviceDescriptor = null)
    {
        WebGPUPtr<Device> device = default;
        
        unsafe
        {
            var callback = new PfnRequestDeviceCallback((_, device1, _, _) =>
            {
                device = WebGPUPtr<Device>.MakeStrong(device1);
            });

            adapter.RequestDevice(deviceDescriptor, callback, ref Unsafe.NullRef<int>()); // TODO: Don't do this once we fix userdatum optionality
        }

        return device;
    }

    public static unsafe FeatureName[] EnumerateFeatures(this WebGPUPtr<Adapter> adapter)
    {
        // Get feature count
        var count = WebGPU.WebGPUAPI.AdapterEnumerateFeatures(adapter, null);

        // Allocate memory for writing
        Span<FeatureName> span = stackalloc FeatureName[(int)count];
        WebGPU.WebGPUAPI.AdapterEnumerateFeatures(adapter, span);

        // Copy to array
        var features = new FeatureName[count];
        span.CopyTo(features);
        return features;
    }
}