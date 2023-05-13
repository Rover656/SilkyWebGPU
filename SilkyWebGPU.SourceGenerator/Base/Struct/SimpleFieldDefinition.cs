using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public class SimpleFieldDefinition : IFieldDefinition
{
    private readonly string _name;
    private readonly string _type;
    private readonly bool _unsafe;

    public SimpleFieldDefinition(IFieldSymbol field)
    {
        _name = field.Name;
        _type = field.Type.ToString();
        _unsafe = field.Type.Kind == SymbolKind.PointerType;
    }

    public string GetPropertyDeclaration(NativeAccessor accessor)
    {
        var prefix = _unsafe || accessor is NativeAccessor.Pointer ? "unsafe " : "";
        return $@"
    public {prefix}{_type} {_name}
    {{
        get => Native{accessor.GetAccessor()}{_name};
        set => Native{accessor.GetAccessor()}{_name} = value;
    }}";
    }
}