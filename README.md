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
