namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public interface IFieldDefinition
{
    // TODO: Get any dispose methods.
    // TODO: Bool to say whether or not to include in ToString.
    public string GetPropertyDeclaration(NativeAccessor accessor);
}