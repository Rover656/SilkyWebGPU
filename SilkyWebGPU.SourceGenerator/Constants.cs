using System.Linq;

namespace Rover656.SilkyWebGPU.SourceGenerators
{
    public static class Constants
    {
        /// <summary>
        /// The namespace of the generated class
        /// </summary>
        public const string ExtensionNS = "Rover656.SilkyWebGPU";
        
        /// <summary>
        /// The WebGPU binding namespace.
        /// </summary>
        public const string WebGpuNS = "Silk.NET.WebGPU";

        /// <summary>
        /// The native pointer type used for wrapping Web GPU native objects.
        /// </summary>
        public const string NativePtrType = "WebGPUPtr";
        
        public const string NativeChainableArrayType = "NativeArray";
        public const string NativePointerArrayType = "NativePtrArray";
        public const string ChainedStructBaseClass = "NewNewChainedStruct";
        
        /// <summary>
        /// Native objects that are being treated like "classes"
        /// </summary>
        public static readonly string[] ClassObjects =
        {
            "Adapter", "BindGroup", "BindGroupLayout", "Buffer", "CommandBuffer", "CommandEncoder",
            "ComputePassEncoder", "ComputePipeline", "Device", "Instance", "PipelineLayout", "QuerySet", "Queue",
            "RenderBundleEncoder", "RenderPassEncoder", "RenderPipeline", "Sampler", "ShaderModule", "Surface",
            "SwapChain", "Texture", "TextureView"
        };

        public static readonly string ManagedStructPrefix = "";//""Managed";
        
        // TODO: Wrapping of non-chainable structs with pointers n such in them...

        /// <summary>
        /// All structs that are wrapped by a managed type for handling chaining.
        /// </summary>
        public static readonly string[] ChainedStructs =
        {
            "AdapterProperties", "BindGroupDescriptor", "BindGroupEntry", "BindGroupLayoutDescriptor", "BindGroupLayoutEntry", "BufferBindingLayout",
            "BufferDescriptor", "ColorTargetState", "CommandBufferDescriptor", "CommandEncoderDescriptor", "CompilationInfo", "CompilationMessage",
            "ComputePassDescriptor", "ComputePipelineDescriptor", "ConstantEntry", "DepthStencilState", "DeviceDescriptor", "FragmentState",
            "ImageCopyBuffer", "ImageCopyTexture", "InstanceDescriptor", "MultisampleState", "PipelineLayoutDescriptor",
            "PrimitiveDepthClipControl", "PrimitiveState", "ProgrammableStageDescriptor", "QuerySetDescriptor",
            "QueueDescriptor", "RenderBundleDescriptor", "RenderBundleEncoderDescriptor", "RenderPassDescriptor", "RenderPassDescriptorMaxDrawCount",
            "RenderPipelineDescriptor", "RequestAdapterOptions", "RequiredLimits", "SamplerBindingLayout", "SamplerDescriptor",
            "ShaderModuleCompilationHint", "ShaderModuleDescriptor", "ShaderModuleSPIRVDescriptor", "ShaderModuleWGSLDescriptor",
            "StorageTextureBindingLayout", "SupportedLimits", "SurfaceDescriptor", "SurfaceDescriptorFromAndroidNativeWindow",
            "SurfaceDescriptorFromCanvasHTMLSelector", "SurfaceDescriptorFromMetalLayer", "SurfaceDescriptorFromWaylandSurface",
            "SurfaceDescriptorFromWindowsHWND", "SurfaceDescriptorFromXcbWindow", "SurfaceDescriptorFromXlibWindow",
            "SwapChainDescriptor", "TextureBindingLayout", "TextureDataLayout", "TextureDescriptor", "TextureViewDescriptor",
            "VertexState",

            // Extensions
            "Extensions.WGPU.AdapterExtras",
            "Extensions.WGPU.DeviceExtras", "Extensions.WGPU.InstanceExtras",
            "Extensions.WGPU.PipelineLayoutExtras", "Extensions.WGPU.ShaderModuleGLSLDescriptor",
            "Extensions.WGPU.SupportedLimitsExtras", "Extensions.WGPU.SwapChainDescriptorExtras"
        };

        /// <summary>
        /// All structs that are not chainable that can be wrapped.
        /// TODO: Remove (but comment) those which do not need to be wrapped.
        /// </summary>
        public static readonly string[] WrappedStructs =
        {
            "BlendComponent", "BlendState", "Color", "ComputePassTimestampWrite", "Extent3D", "Limits", "Origin3D", "RenderPassColorAttachment",
            "RenderPassDepthStencilAttachment", "RenderPassTimestampWrite", "StencilFaceState", "VertexAttribute", "VertexBufferLayout",
        };

        public static readonly string[] ManagedStructs = ChainedStructs.Concat(WrappedStructs).ToArray();
    }
}