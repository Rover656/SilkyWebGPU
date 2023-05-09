namespace Rover656.SilkyWebGPU.SourceGenerators
{
    public class Constants
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

        public static readonly string ManagedStructPrefix = "Managed";
        
        /// <summary>
        /// All structs that are wrapped by a managed type for handling chaining.
        /// </summary>
        public static readonly string[] ManagedStructs =
        {
            "AdapterProperties", "BindGroupLayoutDescriptor", "BufferBindingLayout",
            "BufferDescriptor", "CommandBufferDescriptor", "CommandEncoderDescriptor", "InstanceDescriptor",
            "PipelineLayoutDescriptor", "QuerySetDescriptor", "RenderBundleDescriptor", "RenderBundleEncoderDescriptor",
            "RequestAdapterOptions", "SamplerDescriptor", "SurfaceDescriptor", "SwapChainDescriptor",
            "TextureViewDescriptor", "BindGroupDescriptor", "ComputePassDescriptor", "ProgrammableStageDescriptor",
            "ShaderModuleDescriptor", "TextureDescriptor", "ComputePipelineDescriptor", "SupportedLimits",
            "DeviceDescriptor", "RenderPassDescriptor", "RenderPipelineDescriptor", "ShaderModuleWGSLDescriptor",

            // Extensions
            "Extensions.WGPU.AdapterExtras",
            "Extensions.WGPU.DeviceExtras", "Extensions.WGPU.InstanceExtras",
            "Extensions.WGPU.PipelineLayoutExtras", "Extensions.WGPU.ShaderModuleGLSLDescriptor",
            "Extensions.WGPU.SupportedLimitsExtras", "Extensions.WGPU.SwapChainDescriptorExtras"
        };
    }
}