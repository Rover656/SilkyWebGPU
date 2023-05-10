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
        using var descriptor = new InstanceDescriptor
        {
            Next = new InstanceExtras
            {
                Backends = (uint) InstanceBackend.DX12 // TODO: Make enums type-aware.
            }
        };
        
        _Instance = WGPU.CreateInstance(descriptor);
        
        // Create surface from window
        _Surface = _window.CreateWebGPUSurface(_Instance);

        // Request adapter
        using var requestAdapterOptions = new RequestAdapterOptions
        {
            CompatibleSurface = _Surface,
            Next = new AdapterExtras
            {
                Backend = BackendType.D3D12
            }
        };
        
        _Adapter = await _Instance.RequestAdapter(requestAdapterOptions);

        {
            // Create limits object to populate
            var limits = new SupportedLimits
            {
                Next = new SupportedLimitsExtras()
            };

            // Get limits
            _Adapter.GetLimits(ref limits);

            Console.WriteLine(limits);

            // Breakpoint here to take a look.
            limits.Dispose();
        }

        {
            var properties = new AdapterProperties();
            _Adapter.GetProperties(ref properties);
            Console.WriteLine(properties);
            Console.WriteLine(properties.Next);
            properties.Dispose(); // We can't use a "using" directive here as that makes it immutable.
        }

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
        using var shaderModuleDescriptor = new ShaderModuleDescriptor
        {
            Next = new ShaderModuleWGSLDescriptor
            {
                Code = SHADER
            }
        };

        _Shader = _Device.CreateShaderModule(shaderModuleDescriptor);

        // Get swap chain format.
        _SwapChainFormat = _Surface.GetPreferredFormat(_Adapter);

        // Create render pipeline
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
        
        using var colorTargetState = new ColorTargetState
        {
            Format    = _SwapChainFormat,
            Blend     = blendState,
            WriteMask = ColorWriteMask.All
        };

        using var fragmentState = new FragmentState
        {
            Module      = _Shader,
            Targets     = new[] {colorTargetState},
            EntryPoint  = "fs_main"
        };

        using var renderPipelineDescriptor = new RenderPipelineDescriptor
        {
            Vertex = new VertexState
            {
                Module     = _Shader,
                EntryPoint = "vs_main",
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
            Fragment     = fragmentState,
            DepthStencil = null
        };

        _Pipeline = _Device.CreateRenderPipeline(renderPipelineDescriptor);

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
        using var swapChainDescriptor = new SwapChainDescriptor
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

        using var commandEncoderDescriptor = new CommandEncoderDescriptor();

        var encoder = _Device.CreateCommandEncoder(commandEncoderDescriptor);

        using var colorAttachment = new RenderPassColorAttachment
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

        using var renderPassDescriptor = new RenderPassDescriptor
        {
            ColorAttachments = new[] {colorAttachment},
            DepthStencilAttachment = null
        };

        var renderPass = encoder.BeginRenderPass(renderPassDescriptor);

        renderPass.SetPipeline(_Pipeline);
        renderPass.Draw(3, 1, 0, 0);
        renderPass.End();
        nextTexture.Dispose();

        var queue = _Device.GetQueue();

        var commandBuffer = encoder.Finish(new CommandBufferDescriptor()); // This new object is fine, as an empty class won't allocate anything :)

        queue.Submit(1, ref commandBuffer);
        _SwapChain.Present();
        _window.SwapBuffers();
    }
    
    private static void PrintAdapterFeatures()
    {
        var features = _Adapter.EnumerateFeatures();

        Console.WriteLine("Adapter features:");

        foreach (var feature in features)
        {
            Console.WriteLine($"\t{feature}");
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