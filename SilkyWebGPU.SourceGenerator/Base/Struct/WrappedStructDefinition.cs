using System.Text;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public class WrappedStructDefinition : BaseStructDefinition
{
    public WrappedStructDefinition(INamedTypeSymbol type) : base(type)
    {
    }

    public override void WriteSource(StringBuilder builder)
    {
        // File header
        builder.Append(
            $@"
public sealed class {Name} : {GlobalConstants.WrappedBaseClass}<{OriginalType}>
{{");
        
        // TODO: Write constructors/implicits

        // Write fields
        foreach (var field in Fields)
        {
            builder.AppendLine(field.GetPropertyDeclaration(NativeAccessor.Value));
        }
        
        // TODO: Write methods
        
        builder.Append('}');
    }
}