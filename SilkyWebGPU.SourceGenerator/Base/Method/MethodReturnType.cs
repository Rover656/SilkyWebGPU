using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator.Method;

public struct MethodReturnType
{
    public string Type;
    public bool ClassObject;

    public MethodReturnType(IMethodSymbol method)
    {
        if (method.ReturnType is IPointerTypeSymbol pointer &&
            GlobalConstants.Objects.Contains(pointer.PointedAtType.Name))
        {
            ClassObject = true;
            Type = pointer.PointedAtType.ToString();
        }
        else
        {
            ClassObject = false;
            Type = method.ReturnType.ToString();
        }
    }

    public override string ToString() => ClassObject ? $"{GlobalConstants.NativePtrType}<{Type}>" : Type;
}