using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.Disposal;

namespace Rover656.SilkyWebGPU;

// TODO: This is the heart of the operation, and its a bloody mess :P
public static partial class WebGPU
{
    private static Silk.NET.WebGPU.WebGPU _webGpuApi;
    private static WebGPUDisposal _disposal;

    public static Silk.NET.WebGPU.WebGPU WebGPUAPI
    {
        get
        {
            if (_webGpuApi == null)
                _webGpuApi = Silk.NET.WebGPU.WebGPU.GetApi();
            return _webGpuApi;
        }
    }
    
    public static WebGPUDisposal Disposal
    {
        get
        {
            if (_disposal == null)
                _disposal = new WebGPUDisposal(WebGPUAPI);
            return _disposal;
        }
    }
    

    public static unsafe WebGPUPtr<Instance> CreateInstance(InstanceDescriptor descriptor)
    {
        var ret = new WebGPUPtr<Instance>(WebGPUAPI.CreateInstance(descriptor.Get()));
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webGpuPtr"></param>
    /// <typeparam name="T"></typeparam>
    public static unsafe void Dispose<T>(WebGPUPtr<T> webGpuPtr) where T : unmanaged
    {
        // TODO: It appears these are missing from the Disposal extension..
        //       Will consider a PR after querying with devs.
        switch (webGpuPtr)
        {
            // case WebGpuPtr<Instance> instance:
                // Disposal.Dispose(instance);
                // break;
            case WebGPUPtr<Adapter> adapter:
                Disposal.Dispose(adapter);
                break;
            case WebGPUPtr<BindGroup> bindGroup:
                Disposal.Dispose(bindGroup);
                break;
            case WebGPUPtr<BindGroupLayout> bindGroupLayout:
                Disposal.Dispose(bindGroupLayout);
                break;
            case WebGPUPtr<Silk.NET.WebGPU.Buffer> buffer:
                Disposal.Dispose(buffer);
                break;
            case WebGPUPtr<CommandBuffer> commandBuffer:
                // Disposal.Dispose(commandBuffer); // TODO: Also crashing.
                break;
            case WebGPUPtr<CommandEncoder> commandEncoder:
                // Disposal.Dispose(commandEncoder); // TODO: Causes the entire program to fail pog.
                break;
            // case WebGpuPtr<RenderPassEncoder> renderPassEncoder:
            //     Disposal.Dispose(renderPassEncoder);
            //     break;
            // case WebGpuPtr<ComputePassEncoder> computePassEncoder:
            //     Disposal.Dispose(computePassEncoder);
            //     break;
            // case WebGpuPtr<RenderBundleEncoder> renderBundleEncoder:
            //     Disposal.Dispose(renderBundleEncoder);
            //     break;
            case WebGPUPtr<ComputePipeline> computePipeline:
                Disposal.Dispose(computePipeline);
                break;
            case WebGPUPtr<Device> device:
                Disposal.Dispose(device);
                break;
            case WebGPUPtr<PipelineLayout> pipelineLayout:
                Disposal.Dispose(pipelineLayout);
                break;
            case WebGPUPtr<QuerySet> querySet:
                Disposal.Dispose(querySet);
                break;
            case WebGPUPtr<RenderBundle> renderBundle:
                Disposal.Dispose(renderBundle);
                break;
            case WebGPUPtr<RenderPipeline> renderPipeline:
                Disposal.Dispose(renderPipeline);
                break;
            case WebGPUPtr<Sampler> sampler:
                Disposal.Dispose(sampler);
                break;
            case WebGPUPtr<ShaderModule> shaderModule:
                Disposal.Dispose(shaderModule);
                break;
            case WebGPUPtr<Surface> surface:
                Disposal.Dispose(surface);
                break;
            // case WebGpuPtr<SwapChain> swapChain:
            //     Disposal.Dispose(swapChain);
            //     break;
            case WebGPUPtr<Texture> texture:
                Disposal.Dispose(texture);
                break;
            case WebGPUPtr<TextureView> textureView:
                Disposal.Dispose(textureView);
                break;
        }
    }
}