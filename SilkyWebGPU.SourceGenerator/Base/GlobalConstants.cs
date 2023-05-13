using System.Linq;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public static class GlobalConstants
{
    /// <summary>
    /// The root namespace of the project
    /// </summary>
    public const string ProjectNS = "Rover656.SilkyWebGPU";

    public const string WebGpuNS = "Silk.NET.WebGPU";
    public const string WgpuNS = $"{WebGpuNS}.Extensions.WGPU";
    public const string DawnNS = $"{WebGpuNS}.Extensions.WGPU";
    
    /// <summary>
    /// Native pointer type for wrapping objects.
    /// </summary>
    public const string NativePtrType = "WebGPUPtr";
    
    /// <summary>
    /// Native array type.
    /// </summary>
    public const string NativeStructArrayType = "NativeArray";
    
    /// <summary>
    /// Native pointer array type.
    /// </summary>
    public const string NativePointerArrayType = "NativePtrArray";
    
    /// <summary>
    /// The base class for a chainable struct.
    /// </summary>
    public const string ChainableBaseClass = "ChainableStruct";
    
    /// <summary>
    /// The base class for a chained struct.
    /// </summary>
    public const string ChainedBaseClass = "ChainedStruct";
    
    /// <summary>
    /// The base class for a wrapped struct.
    /// </summary>
    public const string WrappedBaseClass = "WrappedStruct";

    /// <summary>
    /// The standard set of objects that will be wrapped by WebGPUPtr's and given methods.
    /// </summary>
    public static readonly string[] Objects =
    {
        "Adapter", "BindGroup", "BindGroupLayout", "Buffer", "CommandBuffer", "CommandEncoder", "ComputePassEncoder",
        "ComputePipeline", "Device", "Instance", "PipelineLayout", "QuerySet", "Queue", "RenderBundle",
        "RenderBundleEncoder", "RenderPassEncoder", "RenderPipeline", "Sampler", "ShaderModule", "Surface", "SwapChain",
        "Texture", "TextureView"
    };

    public static readonly string[] ChainableStructs =
    {
        "AdapterProperties", "BindGroupDescriptor", "BindGroupEntry", "BindGroupLayoutDescriptor",
        "BindGroupLayoutEntry", "BufferBindingLayout", "BufferDescriptor", "ColorTargetState",
        "CommandBufferDescriptor", "CommandEncoderDescriptor", "CompilationInfo", "CompilationMessage",
        "ComputePassDescriptor", "ComputePipelineDescriptor", "ConstantEntry", "DepthStencilState", "DeviceDescriptor",
        "FragmentState", "ImageCopyBuffer", "ImageCopyTexture", "InstanceDescriptor", "MultisampleState",
        "PipelineLayoutDescriptor", "PrimitiveState", "ProgrammableStageDescriptor", "QuerySetDescriptor",
        "QueueDescriptor", "RenderBundleDescriptor", "RenderBundleEncoderDescriptor", "RenderPassDescriptor",
        "RenderPipelineDescriptor", "RequestAdapterOptions", "RequiredLimits", "SamplerBindingLayout",
        "SamplerDescriptor", "ShaderModuleCompilationHint", "ShaderModuleDescriptor", "StorageTextureBindingLayout",
        "SupportedLimits", "SurfaceDescriptor", "SwapChainDescriptor", "TextureBindingLayout", "TextureDataLayout",
        "TextureDescriptor", "TextureViewDescriptor", "VertexState",
    };

    /// <summary>
    /// All chained structs and their SType.
    /// </summary>
    public static readonly (string, string)[] ChainedStructs =
    {
        ("SurfaceDescriptorFromMetalLayer", "SType.SurfaceDescriptorFromMetalLayer"),
        ("SurfaceDescriptorFromWindowsHWND", "SType.SurfaceDescriptorFromWindowsHwnd"),
        ("SurfaceDescriptorFromXlibWindow", "SType.SurfaceDescriptorFromXlibWindow"),
        ("SurfaceDescriptorFromCanvasHTMLSelector", "SType.SurfaceDescriptorFromCanvasHtmlselector"),
        ("ShaderModuleSPIRVDescriptor", "SType.ShaderModuleSpirvdescriptor"),
        ("ShaderModuleWGSLDescriptor", "SType.ShaderModuleWgsldescriptor"),
        ("PrimitiveDepthClipControl", "SType.PrimitiveDepthClipControl"),
        ("SurfaceDescriptorFromWaylandSurface", "SType.SurfaceDescriptorFromWaylandSurface"),
        ("SurfaceDescriptorFromAndroidNativeWindow", "SType.SurfaceDescriptorFromAndroidNativeWindow"),
        ("SurfaceDescriptorFromXcbWindow", "SType.SurfaceDescriptorFromXcbWindow"),
        ("RenderPassDescriptorMaxDrawCount", "SType.RenderPassDescriptorMaxDrawCount"),
    };

    /// <summary>
    /// Non-Chainable structs that require wrapping.
    /// </summary>
    public static readonly string[] WrappedStructs =
    {
        "BlendComponent", "BlendState", "Color", "ComputePassTimestampWrite", "Extent3D", "Limits", "Origin3D",
        "RenderPassColorAttachment", "RenderPassDepthStencilAttachment", "RenderPassTimestampWrite", "StencilFaceState",
        "VertexAttribute", "VertexBufferLayout",
    };

    /// <summary>
    /// All structs that have managed versions.
    /// </summary>
    public static readonly string[] ManagedStructs = ChainableStructs.Concat(ChainedStructs.Select(e => e.Item1)).Concat(WrappedStructs).ToArray();
}