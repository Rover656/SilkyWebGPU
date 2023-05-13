using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator.Method;

public struct MethodParameterDefinition
{
    public string Type;
    public string Name;
    public string ParameterPrefix;
    public string ArgumentPrefix;

    public MethodParameterDefinition(IParameterSymbol parameter)
    {
        Name = parameter.Name;
        ParameterPrefix = parameter.RefKind switch
        {
            RefKind.Ref => "ref",
            RefKind.Out => "out",
            RefKind.In => "in",
            _ => ""
        };
        ArgumentPrefix = parameter.RefKind switch
        {
            RefKind.Ref => "ref",
            RefKind.Out => "out",
            _ => ""
        };

        if (parameter.Type is IPointerTypeSymbol pointerType)
        {
            if (pointerType.PointedAtType is IPointerTypeSymbol arrayType)
            {
                if (GlobalConstants.Objects.Contains(arrayType.PointedAtType.Name))
                {
                    Type = $"{GlobalConstants.NativePointerArrayType}<{arrayType.PointedAtType}>";
                }
                else
                {
                    Type = $"{GlobalConstants.NativeStructArrayType}<{arrayType.PointedAtType}>";
                }
            }
            else if (GlobalConstants.Objects.Contains(pointerType.PointedAtType.Name))
            {
                Type = $"{GlobalConstants.NativePtrType}<{pointerType.PointedAtType}>";
            }
            else
            {
                Type = parameter.Type.ToString();
            }
        }
        else
        {
            Type = parameter.Type.ToString();
        }
    }
}