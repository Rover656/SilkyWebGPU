using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

/**
 * Helper methods for dealing with chains.
 */
public static class ChainHelper
{
    /**
     * Incredibly unsafe if used incorrectly.
     * <typeparam name="T">Must be a chainable struct.</typeparam>
     */
    internal static unsafe ChainedStruct* AddToChain<T>(ChainedStruct* self, T toAdd, SType sType) where T : unmanaged
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
     * Incredibly unsafe if used incorrectly.
     * <typeparam name="T">Must be a chainable struct.</typeparam>
     */
    internal static unsafe ChainedStruct* CreateChained<T>(T toAdd) where T : unmanaged
    {
        // Allocate memory for this piece to go.
        var allocatedMem = SilkMarshal.Allocate(Marshal.SizeOf<T>());
        *(T*)allocatedMem = toAdd;

        // Set SType.
        var chain = (ChainedStruct*)allocatedMem;
        chain->SType = GetSType(toAdd);
        
        return (ChainedStruct*)allocatedMem;
    }

    internal static unsafe void DestroyChained(ChainedStruct* toDestroy)
    {
        // Destroy the chain
        FreeChain(toDestroy);
        
        // Delete the chain
        SilkMarshal.Free((nint) toDestroy);
    }

    /// <inheritdoc cref="FreeChain(ChainedStruct*)"/>
    internal static unsafe void FreeChain<T>(ref T chain) where T : unmanaged
    {
        // Fix the ref and run with it.
        fixed (void* chained = &chain)
        {
            FreeChain((ChainedStruct*)chained);
        }
    }

    /**
     * Free the allocated memory of a chained struct.
     * This will not free the main chain, as that is normally managed.
     */
    internal static unsafe void FreeChain(ChainedStruct* chain)
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

    internal static SType GetSType<T>(T native) where T : unmanaged
    {
        return native switch
        {
            Silk.NET.WebGPU.SurfaceDescriptorFromMetalLayer => SType.SurfaceDescriptorFromMetalLayer,
            Silk.NET.WebGPU.SurfaceDescriptorFromWindowsHWND => SType.SurfaceDescriptorFromWindowsHwnd,
            Silk.NET.WebGPU.SurfaceDescriptorFromXlibWindow => SType.SurfaceDescriptorFromXlibWindow,
            Silk.NET.WebGPU.SurfaceDescriptorFromCanvasHTMLSelector => SType.SurfaceDescriptorFromCanvasHtmlselector,
            Silk.NET.WebGPU.ShaderModuleSPIRVDescriptor => SType.ShaderModuleSpirvdescriptor,
            Silk.NET.WebGPU.ShaderModuleWGSLDescriptor => SType.ShaderModuleWgsldescriptor,
            Silk.NET.WebGPU.PrimitiveDepthClipControl => SType.PrimitiveDepthClipControl,
            Silk.NET.WebGPU.SurfaceDescriptorFromWaylandSurface => SType.SurfaceDescriptorFromWaylandSurface,
            Silk.NET.WebGPU.SurfaceDescriptorFromAndroidNativeWindow => SType.SurfaceDescriptorFromAndroidNativeWindow,
            Silk.NET.WebGPU.SurfaceDescriptorFromXcbWindow => SType.SurfaceDescriptorFromXcbWindow,
            Silk.NET.WebGPU.RenderPassDescriptorMaxDrawCount => SType.RenderPassDescriptorMaxDrawCount,
            
            // WGPU Extensions
            Silk.NET.WebGPU.Extensions.WGPU.DeviceExtras => (SType)NativeSType.STypeDeviceExtras,
            Silk.NET.WebGPU.Extensions.WGPU.AdapterExtras => (SType)NativeSType.STypeAdapterExtras,
            Silk.NET.WebGPU.Extensions.WGPU.RequiredLimitsExtras => (SType)NativeSType.STypeRequiredLimitsExtras,
            Silk.NET.WebGPU.Extensions.WGPU.SupportedLimitsExtras => (SType)NativeSType.STypeSupportedLimitsExtras,
            Silk.NET.WebGPU.Extensions.WGPU.PipelineLayoutExtras => (SType)NativeSType.STypePipelineLayoutExtras,
            Silk.NET.WebGPU.Extensions.WGPU.ShaderModuleGLSLDescriptor => (SType)NativeSType.STypeShaderModuleGlsldescriptor,
            Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras => (SType)NativeSType.STypeInstanceExtras,
            Silk.NET.WebGPU.Extensions.WGPU.SwapChainDescriptorExtras => (SType)NativeSType.STypeSwapChainDescriptorExtras,
            _ => SType.Invalid
        };
    }
}