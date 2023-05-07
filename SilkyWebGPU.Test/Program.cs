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
        var descriptor = new ManagedInstanceDescriptor();
        var extras = new ManagedInstanceExtras()
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
        var properties = _Adapter.GetProperties();
        var features = _Adapter.EnumerateFeatures();

        unsafe
        {
            Console.WriteLine($"Device: {SilkMarshal.PtrToString((nint)properties.Name)}. API: {properties.BackendType}");
        }

        Console.WriteLine("========");
        Console.WriteLine("Adapter Features:");
        Console.WriteLine("========");
        foreach (var feature in features)
        {
            if ((int)feature >= (int)NativeFeature.PushConstants)
                Console.WriteLine((NativeFeature)feature);
            else Console.WriteLine(feature);
        }
        Console.WriteLine("========");

        // TODO: Device callbacks

        //_Adapter.GetProperties();

        // Load shader
        // TODO Finish the API surface for this bit.
    }
}