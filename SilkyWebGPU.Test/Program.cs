using Rover656.SilkyWebGPU.Structs;
using Silk.NET.Core.Native;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;
using Silk.NET.Windowing;
using InstanceDescriptor = Rover656.SilkyWebGPU.Structs.InstanceDescriptor;
using RequestAdapterOptions = Rover656.SilkyWebGPU.Structs.RequestAdapterOptions;

namespace Rover656.SilkyWebGPU.Test;

class Program
{
    private static IWindow _window = null!;

    private static WebGPUPtr<Instance> _Instance;
    private static WebGPUPtr<Surface> _Surface;
    private static WebGPUPtr<Adapter> _Adapter;
    
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
        _window.Load += OnLoad;
        _window.Run();
    }

    private static async void OnLoad()
    {
        // Create instance
        using var descriptor = new InstanceDescriptor
        {
            WGpuExtras = new InstanceDescriptor.InstanceExtras
            {
                Backends = InstanceBackend.Primary
            }
        };
        _Instance = WGPU.CreateInstance(descriptor);
        
        // Create surface from window
        unsafe
        {
            _Surface = new WebGPUPtr<Surface>(_window.CreateWebGPUSurface(WGPU.API, _Instance));
        }
        
        // Request adapter
        var requestAdapterOptions = new RequestAdapterOptions(compatibleSurface: _Surface)
        {
            WGpuExtras = new RequestAdapterOptions.AdapterExtras
            {
                Backend = BackendType.D3D12,
            }
        };
        _Adapter = await _Instance.RequestAdapter(requestAdapterOptions);
        
        // TODO: Device callbacks
        
        // Load shader
        // TODO Finish the API surface for this bit.
    }
}