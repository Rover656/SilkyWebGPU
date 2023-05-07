using Rover656.SilkyWebGPU.Chain;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.Structs;

// TODO: Deal with namings, overshadowing is evidently messy
public class InstanceDescriptor : IDisposable, IManagedChainable<Silk.NET.WebGPU.InstanceDescriptor>
{
    private Silk.NET.WebGPU.InstanceDescriptor _instanceDescriptor;

    public InstanceExtras? WGpuExtras = null!;
    
    public unsafe ChainHolder<Silk.NET.WebGPU.InstanceDescriptor> Get()
    {
        if (WGpuExtras == null)
            return new ChainHolder<Silk.NET.WebGPU.InstanceDescriptor>(_instanceDescriptor);
        
        // Add extras to the chain.
        var e = WGpuExtras._instanceExtras;

        fixed (Silk.NET.WebGPU.InstanceDescriptor* instanceDescriptorPtr = &_instanceDescriptor)
        {
            ChainHelper.AddToChain((ChainedStruct*)instanceDescriptorPtr, e, (SType) NativeSType.STypeInstanceExtras);
        }
        
        return new ChainHolder<Silk.NET.WebGPU.InstanceDescriptor>(_instanceDescriptor);
    }

    public void Dispose()
    {
        WGpuExtras?.Dispose();
    }

    public class InstanceExtras : IDisposable
    {
        internal Silk.NET.WebGPU.Extensions.WGPU.InstanceExtras _instanceExtras;

        public InstanceExtras(InstanceBackend? backends = null, Dx12Compiler? dx12ShaderCompiler = null, string? dxilPath = null, string? dxcPath = null)
        {
            if (backends.HasValue)
                Backends = backends.Value;
            if (dx12ShaderCompiler.HasValue)
                Dx12ShaderCompiler = dx12ShaderCompiler.Value;
            if (dxilPath != null)
                DxilPath = dxilPath;
            if (dxcPath != null)
                DxcPath = dxcPath;
        }

        public InstanceBackend Backends
        {
            get => (InstanceBackend) _instanceExtras.Backends;
            set => _instanceExtras.Backends = (uint) value;
        }

        public Dx12Compiler Dx12ShaderCompiler
        {
            get => _instanceExtras.Dx12ShaderCompiler;
            set => _instanceExtras.Dx12ShaderCompiler = value;
        }

        public unsafe string? DxilPath
        {
            get => SilkMarshal.PtrToString((nint) _instanceExtras.DxilPath);
            set
            {
                if (_instanceExtras.DxilPath != null)
                    SilkMarshal.FreeString((nint) _instanceExtras.DxilPath);
                if (value != null)
                    SilkMarshal.StringToPtr(value);
            }
        }
        
        public unsafe string? DxcPath
        {
            get => SilkMarshal.PtrToString((nint) _instanceExtras.DxcPath);
            set
            {
                if (_instanceExtras.DxcPath != null)
                    SilkMarshal.FreeString((nint) _instanceExtras.DxcPath);
                if (value != null)
                    SilkMarshal.StringToPtr(value);
            }
        }

        private unsafe void ReleaseUnmanagedResources()
        {
            if (_instanceExtras.DxilPath != null)
            {
                SilkMarshal.FreeString((nint)_instanceExtras.DxilPath);
                _instanceExtras.DxilPath = null;
            }

            if (_instanceExtras.DxcPath != null)
            {
                SilkMarshal.FreeString((nint) _instanceExtras.DxcPath);
                _instanceExtras.DxcPath = null;
            }
        }

        ~InstanceExtras() => ReleaseUnmanagedResources();

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}