using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover656.SilkyWebGPU.BaseSourceGenerator.Method;

public struct MethodDefinition
{
    public MethodReturnType ReturnType;
    public ManagedOwnerDefinition Owner;
    public string Name;
    public string OriginalName;
    public List<MethodParameterDefinition> Parameters;
    public List<TypeParameterDefinition> TypeParameters;

    public string ArgumentParameterDecl
    {
        get
        {
            if (Parameters.Count <= 0)
                return "";

            var builder = new StringBuilder(", ");
            foreach (var parameter in Parameters)
            {
                if (!string.IsNullOrEmpty(parameter.ArgumentPrefix))
                    builder.Append($"{parameter.ArgumentPrefix} ");
                builder.Append($"{parameter.Name}, ");
            }

            return builder.ToString().TrimEnd(',', ' ');
        }
    }

    public string ParameterDecl
    {
        get
        {
            if (Parameters.Count <= 0)
                return "";

            var builder = new StringBuilder(", ");
            foreach (var parameter in Parameters)
            {
                if (!string.IsNullOrEmpty(parameter.ParameterPrefix))
                    builder.Append($"{parameter.ParameterPrefix} ");
                builder.Append($"{parameter.Type} {parameter.Name}, ");
            }

            return builder.ToString().TrimEnd(',', ' ');
        }
    }

    public string GetMethodCall(string apiHandle)
    {
        var builder = new StringBuilder(ReturnType.Type == "void" ? "" : "return ");

        if (ReturnType.ClassObject) builder.Append($"{ReturnType}.MakeStrong(");

        builder.Append($"{apiHandle}.{OriginalName}({Owner.ParameterName}{ArgumentParameterDecl})");

        if (ReturnType.ClassObject) builder.Append(')');
        builder.Append(';');

        return builder.ToString();
    }

    public string TypeParameterDecl =>
        TypeParameters.Count <= 0 ? "" : $"<{string.Join(", ", TypeParameters.Select(e => e.Name))}>";

    public string ConstraintDecl => TypeParameters.Count <= 0
        ? ""
        : string.Join(Environment.NewLine, TypeParameters.Select(e => e.ConstraintDecl));

    // TODO: Get original definition string for inheritdoc.
}