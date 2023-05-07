using Rover656.SilkyWebGPU.Chain;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Structs;

public class RequestAdapterOptions : IManagedChainable<Silk.NET.WebGPU.RequestAdapterOptions>
{
    private Silk.NET.WebGPU.RequestAdapterOptions _requestAdapterOptions;

    public AdapterExtras? WGpuExtras = null!;

    public RequestAdapterOptions(
        WebGPUPtr<Surface>? compatibleSurface = null,
        PowerPreference? powerPreference = null,
        bool? forceFallbackAdapter = null)
    {
        if (compatibleSurface != null)
            CompatibleSurface = compatibleSurface;
        if (powerPreference.HasValue)
            PowerPreference = powerPreference.Value;
        if (forceFallbackAdapter.HasValue)
            ForceFallbackAdapter = forceFallbackAdapter.Value;
    }

    public unsafe WebGPUPtr<Surface> CompatibleSurface
    {
        // TODO: Further testing before we introduce this concept..
        // get => WebGPUPtr<Surface>.Weak(_requestAdapterOptions.CompatibleSurface);
        set => _requestAdapterOptions.CompatibleSurface = value;
    }

    public PowerPreference PowerPreference
    {
        get => _requestAdapterOptions.PowerPreference;
        set => _requestAdapterOptions.PowerPreference = value;
    }

    public bool ForceFallbackAdapter
    {
        get => _requestAdapterOptions.ForceFallbackAdapter;
        set => _requestAdapterOptions.ForceFallbackAdapter = value;
    }

    public unsafe ChainHolder<Silk.NET.WebGPU.RequestAdapterOptions> Get()
    {
        if (WGpuExtras == null)
            return new ChainHolder<Silk.NET.WebGPU.RequestAdapterOptions>(_requestAdapterOptions);
        
        // Add extras to the chain
        var e = WGpuExtras._adapterExtras;

        // TODO: Instead of this pattern, is it safer to copy to local and do it that way. Then theres no floating memory in the management class.
        fixed (Silk.NET.WebGPU.RequestAdapterOptions* requestAdapterOptionsPtr = &_requestAdapterOptions)
        {
            ChainHelper.AddToChain((ChainedStruct*)requestAdapterOptionsPtr, e, (SType)NativeSType.STypeAdapterExtras);
        }

        return new ChainHolder<Silk.NET.WebGPU.RequestAdapterOptions>(_requestAdapterOptions);
    }

    public class AdapterExtras
    {
        internal Silk.NET.WebGPU.Extensions.WGPU.AdapterExtras _adapterExtras;

        public BackendType Backend
        {
            get => _adapterExtras.Backend;
            set => _adapterExtras.Backend = value;
        }
    }
}