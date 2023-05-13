using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator.Method;

public struct TypeParameterDefinition
{
    public string Name;
    public List<string> Constraints;

    public TypeParameterDefinition(ITypeParameterSymbol typeParameter)
    {
        Name = typeParameter.Name;
        Constraints = new List<string>();
            
        // Add type parameter constraints
        if (typeParameter.HasUnmanagedTypeConstraint) Constraints.Add("unmanaged");
        if (typeParameter.HasConstructorConstraint) Constraints.Add("new()");
        if (typeParameter.HasNotNullConstraint) Constraints.Add("notnull");
        if (typeParameter.HasReferenceTypeConstraint) Constraints.Add("class");
        if (typeParameter.HasValueTypeConstraint && !typeParameter.HasUnmanagedTypeConstraint)
            Constraints.Add("struct");
        foreach (var constraintType in typeParameter.ConstraintTypes) Constraints.Add(constraintType.ToString());
    }

    public string ConstraintDecl => $"where {Name} : {string.Join(", ", Constraints)}";
}