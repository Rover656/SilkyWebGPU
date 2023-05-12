// namespace Rover656.SilkyWebGPU;
//
// /// <summary>
// /// Web GPU Native Type.
// /// This encompasses a native pointer from the WebGPU API.
// /// This allows extensions methods to breathe life into these pointers without hand-writing entire classes.
// /// Disposal is handled by <see cref="WGPU"/>.
// /// </summary>
// /// <typeparam name="T">The type being wrapped</typeparam>
// public sealed unsafe class WebGPUPtr<T> : IDisposable
//     where T : unmanaged
// {
//     /// <summary>
//     /// The native pointer.
//     /// </summary>
//     private T* _ptr;
//
//     /// <summary>
//     /// If this is a weak pointer, we don't dispose, we're a reference.
//     /// </summary>
//     private bool _weak;
//
//     // TODO: RETURN TO INTERNAL
//     public WebGPUPtr(T* ptr)
//     {
//         _ptr = ptr;
//         _weak = false;
//     }
//
//     internal static WebGPUPtr<T> Weak(T* ptr)
//     {
//         var weak = new WebGPUPtr<T>(ptr);
//         weak._weak = true;
//         return weak;
//     }
//
//     public static implicit operator T*(WebGPUPtr<T> ptr)
//         => ptr == null! ? null : ptr._ptr;
//
//     public ref T* AsRef() => ref _ptr;
//
//     public bool IsNull() => _ptr == null;
//
//     /// <summary>
//     /// Whether or not the pointer is weak or not.
//     /// A weak pointer does not control the lifecycle of the object.
//     /// </summary>
//     public bool IsWeak() => _weak;
//
//     ~WebGPUPtr() => Dispose(false);
//
//     public void Dispose()
//     {
//         Dispose(true);
//         GC.SuppressFinalize(this);
//     }
//
//     private void Dispose(bool disposing)
//     {
//         if (_ptr == null) return;
//         if (!_weak)
//             WGPU.Dispose(this);
//         _ptr = null;
//     }
// }