using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator;

public abstract class BaseStructDefinition
{
    public string Name;
    public string OriginalType;
    public List<IFieldDefinition> Fields { get; set; }

    public BaseStructDefinition(INamedTypeSymbol type)
    {
        // Set struct name
        Name = "TEMP" + type.Name; // TODO: Remove prefix!
        OriginalType = type.ToString();
        
        // Get all struct members
        var members = type.GetMembers();
        
        // Process fields and collect definitions
        var definitions = new List<IFieldDefinition>();

        // Collect and process fields.
        foreach (var field in members.Where(e => e is IFieldSymbol).OfType<IFieldSymbol>())
        {
            if (field.Name is "Chain" or "NextInChain")
                continue;
            ProcessField(field, ref definitions);
        }

        // Submit to list
        Fields = definitions;
    }
    
    private void ProcessField(IFieldSymbol field, ref List<IFieldDefinition> definitions)
    {
        // TODO: Add all of the definition types and checks.
        definitions.Add(new SimpleFieldDefinition(field));
    }

    public abstract void WriteSource(StringBuilder builder);
}