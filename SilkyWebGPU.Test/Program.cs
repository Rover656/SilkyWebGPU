using Silk.NET.Core.Native;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;
using Silk.NET.Windowing;

namespace Rover656.SilkyWebGPU.Test;

class Program
{
    private static IWindow _window = null!;

    private static WebGPUPtr<Instance> _Instance;
    private static WebGPUPtr<Surface> _Surface;
    private static WebGPUPtr<Adapter> _Adapter;
    private static WebGPUPtr<Device> _Device;
    private static WebGPUPtr<ShaderModule> _Shader;
    private static WebGPUPtr<RenderPipeline> _Pipeline;
    private static WebGPUPtr<SwapChain> _SwapChain;
    private static TextureFormat _SwapChainFormat;
    
    private const string SHADER = @"@vertex
fn vs_main(@builtin(vertex_index) in_vertex_index: u32) -> @builtin(position) vec4<f32> {
    let x = f32(i32(in_vertex_index) - 1);
    let y = f32(i32(in_vertex_index & 1u) * 2 - 1);
    return vec4<f32>(x, y, 0.0, 1.0);
}

@fragment
fn fs_main() -> @location(0) vec4<f32> {
    return vec4<f32>(1.0, 0.0, 0.0, 1.0);
}";

    public static void Main(string[] args)
    {
        var options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "SilkyWebGPU",
            API = GraphicsAPI.None,
            ShouldSwapAutomatically = false,
            IsContextControlDisabled = true
        };

        _window = Window.Create(options);
        _window.Load += WindowOnLoad;
        _window.Closing += WindowClosing;
        _window.Update += WindowOnUpdate;
        _window.Render += WindowOnRender;
        _window.FramebufferResize += FramebufferResize;
        _window.Run();
    }
    
    private static void FramebufferResize(Vector2D<int> obj)
    {
        CreateSwapChain();
    }

    private static async void WindowOnLoad()
    {
        // Create instance
        var descriptor = new ManagedInstanceDescriptor();
        var extras = new ManagedInstanceExtras
        {
            Backends = (uint)InstanceBackend.DX12 // TODO: Make enums type-aware.
        };
        descriptor.Next = extras;
        _Instance = WGPU.CreateInstance(descriptor);
        
        // Create surface from window
        unsafe
        {
            _Surface = new WebGPUPtr<Surface>(_window.CreateWebGPUSurface(WGPU.API, _Instance));
        }
        
        // Request adapter
        var requestAdapterOptions = new ManagedRequestAdapterOptions
        {
            CompatibleSurface = _Surface
        };
        var adapterExtras = new ManagedAdapterExtras
        {
            Backend = BackendType.D3D12
        };
        requestAdapterOptions.Next = adapterExtras;
        
        _Adapter = await _Instance.RequestAdapter(requestAdapterOptions);

        // Write properties to console
        PrintAdapterFeatures();
        
        // Get device
        _Device = await _Adapter.RequestDevice();

        // TODO: Device callbacks
        unsafe
        {
            _Device.SetUncapturedErrorCallback(new PfnErrorCallback(UncapturedError), null);
            _Device.SetLostCallback(new PfnDeviceLostCallback(DeviceLost), null);
        }

        // Load shader
        unsafe
        {
            var wgslDescriptor = new ManagedShaderModuleWGSLDescriptor
            {
                Code = (byte*) SilkMarshal.StringToPtr(SHADER)
            };
            var shaderModuleDescriptor = new ManagedShaderModuleDescriptor
            {
                Next = wgslDescriptor
            };

            _Shader = _Device.CreateShaderModule(shaderModuleDescriptor);
        }

        _SwapChainFormat = _Surface.GetPreferredFormat(_Adapter);

        unsafe
        {
            var blendState = new BlendState
            {
                Color = new BlendComponent
                {
                    SrcFactor = BlendFactor.One,
                    DstFactor = BlendFactor.Zero,
                    Operation = BlendOperation.Add
                },
                Alpha = new BlendComponent
                {
                    SrcFactor = BlendFactor.One,
                    DstFactor = BlendFactor.Zero,
                    Operation = BlendOperation.Add
                }
            };

            var colorTargetState = new ColorTargetState
            {
                Format    = _SwapChainFormat,
                Blend     = &blendState,
                WriteMask = ColorWriteMask.All
            };

            var fragmentState = new FragmentState
            {
                Module      = _Shader,
                TargetCount = 1,
                Targets     = &colorTargetState,
                EntryPoint  = (byte*) SilkMarshal.StringToPtr("fs_main")
            };

            var renderPipelineDescriptor = new ManagedRenderPipelineDescriptor
            {
                Vertex = new VertexState
                {
                    Module     = _Shader,
                    EntryPoint = (byte*) SilkMarshal.StringToPtr("vs_main"),
                },
                Primitive = new PrimitiveState
                {
                    Topology         = PrimitiveTopology.TriangleList,
                    StripIndexFormat = IndexFormat.Undefined,
                    FrontFace        = FrontFace.Ccw,
                    CullMode         = CullMode.None
                },
                Multisample = new MultisampleState
                {
                    Count                  = 1,
                    Mask                   = ~0u,
                    AlphaToCoverageEnabled = false
                },
                Fragment     = &fragmentState,
                DepthStencil = null
            };

            _Pipeline = _Device.CreateRenderPipeline(renderPipelineDescriptor);
        }

        CreateSwapChain();
    }

    private static void WindowClosing()
    {
        _Shader.Dispose();
        _Pipeline.Dispose();
        _Device.Dispose();
        _Adapter.Dispose();
        _Surface.Dispose();
    }

    private static void CreateSwapChain()
    {
        var swapChainDescriptor = new ManagedSwapChainDescriptor
        {
            Usage = TextureUsage.RenderAttachment,
            Format = _SwapChainFormat,
            Width = (uint)_window.FramebufferSize.X,
            Height = (uint)_window.FramebufferSize.Y,
            PresentMode = PresentMode.Fifo
        };
        _SwapChain = _Device.CreateSwapChain(_Surface, swapChainDescriptor);
    }

    private static void WindowOnUpdate(double delta)
    {
    }

    private static void WindowOnRender(double delta)
    {
        WebGPUPtr<TextureView> nextTexture = null;

        for (var attempt = 0; attempt < 2; attempt++)
        {
            nextTexture = _SwapChain.GetCurrentTextureView();

            if (attempt == 0 && (nextTexture == null || nextTexture.IsNull()))
            {
                Console.WriteLine("wgpu.SwapChainGetCurrentTextureView() failed; trying to create a new swap chain...\n");
                CreateSwapChain();
                continue;
            }

            break;
        }

        if (nextTexture == null || nextTexture.IsNull())
        {
            Console.WriteLine("wgpu.SwapChainGetCurrentTextureView() failed after multiple attempts; giving up.\n");
            return;
        }

        var commandEncoderDescriptor = new ManagedCommandEncoderDescriptor();

        var encoder = _Device.CreateCommandEncoder(commandEncoderDescriptor);

        var colorAttachment = new RenderPassColorAttachment
        {
            View          = nextTexture,
            ResolveTarget = null,
            LoadOp        = LoadOp.Clear,
            StoreOp       = StoreOp.Store,
            ClearValue = new Color
            {
                R = 0,
                G = 1,
                B = 0,
                A = 1
            }
        };

        var renderPassDescriptor = new ManagedRenderPassDescriptor
        {
            ColorAttachmentCount   = 1,
            DepthStencilAttachment = null
        };

        unsafe
        {
            renderPassDescriptor.ColorAttachments = &colorAttachment;
        }

        var renderPass = encoder.BeginRenderPass(renderPassDescriptor);

        renderPass.SetPipeline(_Pipeline);
        renderPass.Draw(3, 1, 0, 0);
        renderPass.End();
        nextTexture.Dispose();

        var queue = _Device.GetQueue();

        var commandBuffer = encoder.Finish(new CommandBufferDescriptor());

        queue.Submit(1, ref commandBuffer);
        _SwapChain.Present();
        // _Window.SwapBuffers();
    }
    
    private static void PrintAdapterFeatures()
    {
        var features = _Adapter.EnumerateFeatures();

        Console.WriteLine("Adapter features:");

        for (var i = 0; i < features.Length; i++)
        {
            Console.WriteLine($"\t{features[i]}");
        }
    }
    
    private static unsafe void DeviceLost(DeviceLostReason arg0, byte* arg1, void* arg2)
    {
        Console.WriteLine($"Device lost! Reason: {arg0} Message: {SilkMarshal.PtrToString((nint) arg1)}");
    }

    private static unsafe void UncapturedError(ErrorType arg0, byte* arg1, void* arg2)
    {
        Console.WriteLine($"{arg0}: {SilkMarshal.PtrToString((nint) arg1)}");
    }
}