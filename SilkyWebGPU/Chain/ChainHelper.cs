using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Chain;

/**
 * Helper methods for dealing with chains.
 */
public static class ChainHelper
{
    /**
     * Incredibly unsafe if used incorrectly.
     * <typeparam name="T">Must be a chainable struct.</typeparam>
     */
    public static unsafe ChainedStruct* AddToChain<T>(ChainedStruct* self, T toAdd, SType sType) where T : unmanaged
    {
        // Allocate memory for this piece to go.
        var allocatedMem = SilkMarshal.Allocate(sizeof(T));
        Unsafe.CopyBlockUnaligned((void*)allocatedMem, &toAdd, (uint) sizeof(T));

        // Set SType.
        var chain = (ChainedStruct*)allocatedMem;
        chain->SType = sType;

        // Add as the next in the chain.
        self->Next = (ChainedStruct*)allocatedMem;
        return chain;
    }

    /**
     * Free the allocated memory of a chained struct.
     * This will not free the main chain, as that is normally managed by C#
     */
    public static unsafe void FreeChain(ChainedStruct* chain)
    {
        // Ignore an empty chain
        if (chain == null)
            return;
        
        // Wipe out the chain.
        var nextChain = chain->Next;
        while (nextChain != null)
        {
            var next = nextChain->Next;
            SilkMarshal.Free((nint) nextChain);
            nextChain = next;
        }
        
        // Finally null it to ensure we don't try freeing randomly
        chain->Next = null;
    }

    public static SType GetSType<T>(ChainedStruct<T> chainedStruct) where T : unmanaged
    {
        return chainedStruct switch
        {
            ChainedStruct<SurfaceDescriptorFromMetalLayer> => SType.SurfaceDescriptorFromMetalLayer,
            ChainedStruct<SurfaceDescriptorFromWindowsHWND> => SType.SurfaceDescriptorFromWindowsHwnd,
            ChainedStruct<SurfaceDescriptorFromXlibWindow> => SType.SurfaceDescriptorFromXlibWindow,
            ChainedStruct<SurfaceDescriptorFromCanvasHTMLSelector> => SType.SurfaceDescriptorFromCanvasHtmlselector,
            ChainedStruct<ShaderModuleSPIRVDescriptor> => SType.ShaderModuleSpirvdescriptor,
            ChainedStruct<ShaderModuleWGSLDescriptor> => SType.ShaderModuleWgsldescriptor,
            ChainedStruct<PrimitiveDepthClipControl> => SType.PrimitiveDepthClipControl,
            ChainedStruct<SurfaceDescriptorFromWaylandSurface> => SType.SurfaceDescriptorFromWaylandSurface,
            ChainedStruct<SurfaceDescriptorFromAndroidNativeWindow> => SType.SurfaceDescriptorFromAndroidNativeWindow,
            ChainedStruct<SurfaceDescriptorFromXcbWindow> => SType.SurfaceDescriptorFromXcbWindow,
            ChainedStruct<RenderPassDescriptorMaxDrawCount> => SType.RenderPassDescriptorMaxDrawCount,
            
            // WGPU Extensions
            ChainedStruct<DeviceExtras> => (SType)NativeSType.STypeDeviceExtras,
            ChainedStruct<AdapterExtras> => (SType)NativeSType.STypeAdapterExtras,
            ChainedStruct<RequiredLimitsExtras> => (SType)NativeSType.STypeRequiredLimitsExtras,
            ChainedStruct<SupportedLimitsExtras> => (SType)NativeSType.STypeSupportedLimitsExtras,
            ChainedStruct<PipelineLayoutExtras> => (SType)NativeSType.STypePipelineLayoutExtras,
            ChainedStruct<ShaderModuleGLSLDescriptor> => (SType)NativeSType.STypeShaderModuleGlsldescriptor,
            ChainedStruct<InstanceExtras> => (SType)NativeSType.STypeInstanceExtras,
            ChainedStruct<SwapChainDescriptorExtras> => (SType)NativeSType.STypeSwapChainDescriptorExtras,
            _ => SType.Invalid
        };
    }
}