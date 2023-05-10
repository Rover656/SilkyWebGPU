# SilkyWebGPU

> **Warning**
> This library is highly unstable and prone to API surface changes. Here be dragons!

SilkyWebGPU is a lightweight abstraction above Silk.NET's WebGPU bindings, mainly powered by source generators.
The abstractions aim to minimise the use of unsafe concepts throughout program code and attempts to handle marshalling of everything behind the scenes.
Chainable structs have been wrapped to ensure they can be constructed easily and sent to the underlying library safely.

## ToDo
- [ ] More type support for the struct generator
- [x] Ability to reverse chained structs back into managed
- [ ] Tidy up the WGPU singleton, it's not a great design.
- [ ] Implement Dawn extensions
- [ ] Separate Dawn and WGPU extensions into separate output projects
- [ ] Get this on Nuget

## Why?
I personally wasn't happy with how the raw API was laid out, so I decided to write this to wrap the Silk.NET APIs in a more C# friendly way. Here are some examples of before and after's.

### Get Adapter
*Before*
```c#
//Get adapter
var requestAdapterOptions = new RequestAdapterOptions
{
  CompatibleSurface = _Surface
};

wgpu.InstanceRequestAdapter
(
  _Instance,
  requestAdapterOptions,
  new PfnRequestAdapterCallback((_, adapter1, _, _) => _Adapter = adapter1),
  null
);
```

*After*
```c#
// Request adapter
using var requestAdapterOptions = new RequestAdapterOptions
{
  CompatibleSurface = _Surface
};
        
_Adapter = await _Instance.RequestAdapter(requestAdapterOptions);
```

## Load Shader
*Before*
```c#
var wgslDescriptor = new ShaderModuleWGSLDescriptor
{
  Code = (byte*) SilkMarshal.StringToPtr(SHADER),
  Chain = new ChainedStruct
  {
    SType = SType.ShaderModuleWgsldescriptor
  }
};

var shaderModuleDescriptor = new ShaderModuleDescriptor
{
  NextInChain = (ChainedStruct*) (&wgslDescriptor),
};

_Shader = wgpu.DeviceCreateShaderModule(_Device, shaderModuleDescriptor);
```

*After*
```c#
using var shaderModuleDescriptor = new ShaderModuleDescriptor
{
  Next = new ShaderModuleWGSLDescriptor
  {
    Code = SHADER
  }
};

_Shader = _Device.CreateShaderModule(shaderModuleDescriptor);
```

## Create Render Pipeline
*Before*
```c#
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

var renderPipelineDescriptor = new RenderPipelineDescriptor
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

_Pipeline = wgpu.DeviceCreateRenderPipeline(_Device, renderPipelineDescriptor);
```

*After*
```c#
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
```
