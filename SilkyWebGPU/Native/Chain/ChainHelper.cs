using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Native.Chain;

/**
 * Helper methods for dealing with chains.
 */
public static class ChainHelper
{
    // TODO: Put these definitions into the generator.
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