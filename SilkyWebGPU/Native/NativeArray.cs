using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;

namespace Rover656.SilkyWebGPU.Native;

/// <summary>
/// An immutable unmanaged array.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed unsafe class NativeArray<T> : IDisposable
    where T : unmanaged
{
    /// <summary>
    /// The unmanaged array pointer.
    /// </summary>
    internal T* Ptr { get; private set; }
    
    /// <summary>
    /// The number of elements in the array.
    /// </summary>
    public int Count { get; }
    
    /// <summary>
    /// Disposable handles for this memory.
    /// </summary>
    private readonly List<IDisposable>? _disposables;
    private readonly bool _owner;

    public static implicit operator NativeArray<T>(INativeWrapper<T>[] arr) => new(arr);
    public static implicit operator NativeArray<T>(T[] arr) => new(arr);

    // Just in case its needed :)
    public static implicit operator Span<T>(NativeArray<T> arr) =>
        MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>(arr.Ptr), arr.Count);

    internal NativeArray(int count, T* ptr)
    {
        _owner = false;
        Count = count;
        Ptr = ptr;
    }

    private NativeArray(int count)
    {
        _owner = true;
        Count = count;
        Ptr = (T*)SilkMarshal.Allocate(Marshal.SizeOf<T>() * count);
    }

    public NativeArray(params INativeWrapper<T>[] values) : this(values.Length)
    {
        _disposables = new List<IDisposable>(values);
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val.Get();
            ptr++;
        }
    }

    public NativeArray(params T[] values) : this(values.Length)
    {
        var ptr = Ptr;
        foreach (var val in values)
        {
            // Copy in :)
            *ptr = val;
            ptr++;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            return *(Ptr + index);
        }
        set
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            *(Ptr + index) = value;
        }
    }
    
    ~NativeArray() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing && _disposables != null)
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
        
        if (!_owner || Ptr == null) return;
        SilkMarshal.Free((nint)Ptr);
        Ptr = null;
    }
}