using System.Linq;

namespace Rover656.SilkyWebGPU.Extensions.WGPU.SourceGenerator
{
    public class Constants
    {
        /// <summary>
        /// The namespace of the generated class
        /// </summary>
        public const string ExtensionNS = "Rover656.SilkyWebGPU.Extensions.WGPU";
        
        public const string APIHandle = $"{ExtensionNS}.WGPU.WGPUAPI";

        /// <summary>
        /// The WebGPU binding namespace.
        /// </summary>
        public const string WGPUNS = "Silk.NET.WebGPU.Extensions.WGPU";

        /// <summary>
        /// All structs that are wrapped by a managed type for handling chaining.
        /// </summary>
        public static readonly string[] ChainedStructs =
        {
            "AdapterExtras", "DeviceExtras", "InstanceExtras",
            "PipelineLayoutExtras", "ShaderModuleGLSLDescriptor",
            "SupportedLimitsExtras", "SwapChainDescriptorExtras",
        };

        /// <summary>
        /// All structs that are not chainable that can be wrapped.
        /// TODO: Remove (but comment) those which do not need to be wrapped.
        /// </summary>
        public static readonly string[] WrappedStructs =
        {
            "GlobalReport", "HubReport", "PushConstantRange", "ShaderDefine",
            "StorageReport", "SurfaceCapabilities", "WrappedSubmissionIndex",
        };
    }
}
