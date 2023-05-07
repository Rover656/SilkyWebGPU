using Rover656.SilkyWebGPU.Chain;
using Silk.NET.Core.Native;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Rover656.SilkyWebGPU.ManagedStructs;

// TODO: Deal with namings, overshadowing is evidently messy
public class ManagedInstanceDescriptor : IDisposable, IManagedChainable<InstanceDescriptor>
{
    private InstanceDescriptor _instanceDescriptor;

    public ManagedInstanceExtras? WGpuExtras = null!;
    
    public unsafe ChainHolder<InstanceDescriptor> Get()
    {
        if (WGpuExtras == null)
            return new ChainHolder<InstanceDescriptor>(_instanceDescriptor);
        
        // Add extras to the chain.
        var e = WGpuExtras._instanceExtras;

        fixed (InstanceDescriptor* instanceDescriptorPtr = &_instanceDescriptor)
        {
            ChainHelper.AddToChain((ChainedStruct*)instanceDescriptorPtr, e, (SType) NativeSType.STypeInstanceExtras);
        }
        
        return new ChainHolder<InstanceDescriptor>(_instanceDescriptor);
    }

    public void Dispose()
    {
        WGpuExtras?.Dispose();
    }

    public class ManagedInstanceExtras : IDisposable
    {
        internal InstanceExtras _instanceExtras;

        public ManagedInstanceExtras(InstanceBackend? backends = null, Dx12Compiler? dx12ShaderCompiler = null, string? dxilPath = null, string? dxcPath = null)
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

        ~ManagedInstanceExtras() => ReleaseUnmanagedResources();

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}