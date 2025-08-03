using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Datacute.SourceGeneratorContext;

public readonly record struct AttributeData
{
    public readonly IncludeFlags Flags;

    public bool IncludeSummary => Flags == IncludeFlags.Summary;
    
    public bool IncludeAttributeContextTargetSymbol => Flags.HasFlag(IncludeFlags.AttributeContextTargetSymbol);
    public bool IncludeAttributeContextTypeSymbol => Flags.HasFlag(IncludeFlags.AttributeContextTypeSymbol);
    public bool IncludeAttributeContextNamedTypeSymbol => Flags.HasFlag(IncludeFlags.AttributeContextNamedTypeSymbol);
    public bool IncludeAttributeContextTargetNode => Flags.HasFlag(IncludeFlags.AttributeContextTargetNode);
    public bool IncludeAttributeContextAttributes => Flags.HasFlag(IncludeFlags.AttributeContextAttributes);
    public bool IncludeAttributeContextAllAttributes => Flags.HasFlag(IncludeFlags.AttributeContextAllAttributes);

    public bool IncludeGlobalOptions => Flags.HasFlag(IncludeFlags.GlobalOptions);
    public bool IncludeCompilation => Flags.HasFlag(IncludeFlags.Compilation);
    public bool IncludeCompilationOptions => Flags.HasFlag(IncludeFlags.CompilationOptions);
    public bool IncludeCompilationAssembly => Flags.HasFlag(IncludeFlags.CompilationAssembly);
    public bool IncludeCompilationReferences => Flags.HasFlag(IncludeFlags.CompilationReferences);
    public bool IncludeParseOptions => Flags.HasFlag(IncludeFlags.ParseOptions);
    public bool IncludeAdditionalTexts => Flags.HasFlag(IncludeFlags.AdditionalTexts);
    public bool IncludeAdditionalTextsOptions => Flags.HasFlag(IncludeFlags.AdditionalTextsOptions);
    public bool IncludeMetadataReferences => Flags.HasFlag(IncludeFlags.MetadataReferences);

    public readonly string TargetSymbolDocComments;
    public readonly string TypeSymbolDocComments;
    public readonly string NamedTypeSymbolDocComments;
    public readonly string TargetNodeDocComments;
    public readonly string AttributesDocComments;
    public readonly string AllAttributesDocComments;

    
    public AttributeData(in GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext)
    {
        var sb = new StringBuilder();
        var targetSymbol = generatorAttributeSyntaxContext.TargetSymbol;
        sb.AddComment("Type", targetSymbol.GetType().Name);
        sb.AddComment("Kind", targetSymbol.Kind);
        sb.AddComment("Language", targetSymbol.Language);
        sb.AddComment("DeclaredAccessibility", targetSymbol.DeclaredAccessibility);
        sb.AddComment("ContainingSymbol Kind", targetSymbol.ContainingSymbol?.Kind);
        sb.AddComment("ContainingSymbol Name", targetSymbol.ContainingSymbol?.Name);
        sb.AddComment("ContainingAssembly Name", targetSymbol.ContainingAssembly?.Name);
        sb.AddComment("ContainingModule Name", targetSymbol.ContainingModule?.Name);
        sb.AddComment("ContainingType Name", targetSymbol.ContainingType?.Name);
        var containingTypeTypeParameters = targetSymbol.ContainingType?.TypeParameters;
        sb.AddComment("ContainingType Generic Types", containingTypeTypeParameters?.Length);
        if (containingTypeTypeParameters != null)
        {
            for (var n = 0; n < containingTypeTypeParameters.Value.Length; n++)
            {
                var typeParameter = containingTypeTypeParameters.Value[n];
                sb.AddComment($"ContainingType Generic Type {n+1}", typeParameter.ToDisplayString());
            }
        }
        sb.AddComment("ContainingNamespace Name", targetSymbol.ContainingNamespace.Name);
        sb.AddComment("ContainingNamespace IsGlobalNamespace", targetSymbol.ContainingNamespace.IsGlobalNamespace);
        sb.AddComment("Name", targetSymbol.Name);
        sb.AddComment("MetadataName", targetSymbol.MetadataName);
        sb.AddComment("MetadataToken", targetSymbol.MetadataToken);
        sb.AddComment("IsDefinition", targetSymbol.IsDefinition);
        sb.AddComment("IsStatic", targetSymbol.IsStatic);
        sb.AddComment("IsVirtual", targetSymbol.IsVirtual);
        sb.AddComment("IsOverride", targetSymbol.IsOverride);
        sb.AddComment("IsAbstract", targetSymbol.IsAbstract);
        sb.AddComment("IsSealed", targetSymbol.IsSealed);
        sb.AddComment("IsExtern", targetSymbol.IsExtern);
        sb.AddComment("IsImplicitlyDeclared", targetSymbol.IsImplicitlyDeclared);
        sb.AddComment("CanBeReferencedByName", targetSymbol.CanBeReferencedByName);

        TargetSymbolDocComments = sb.ToString();

        sb.Clear();

        if (generatorAttributeSyntaxContext.TargetSymbol is ITypeSymbol typeTargetSymbol)
        {
            sb.AddComment("TypeKind", typeTargetSymbol.TypeKind);
            sb.AddComment("BaseType Name", typeTargetSymbol.BaseType?.Name ?? string.Empty);
            var interfaces = typeTargetSymbol.Interfaces;
            sb.AddComment("Interfaces Length", interfaces.Length);
            for (var n = 0; n < interfaces.Length; n++)
            {
                var directInterface = interfaces[n];
                sb.AddComment($"Interface {n+1}", directInterface.ToDisplayString());
            }
            var allInterfaces = typeTargetSymbol.AllInterfaces;
            sb.AddComment("AllInterfaces Length", allInterfaces.Length);
            for (var n = 0; n < allInterfaces.Length; n++)
            {
                var implementedInterface = allInterfaces[n];
                sb.AddComment($"Interface {n+1}", implementedInterface.ToDisplayString());
            }
            sb.AddComment("IsReferenceType", typeTargetSymbol.IsReferenceType);
            sb.AddComment("IsValueType", typeTargetSymbol.IsValueType);
            sb.AddComment("IsAnonymousType", typeTargetSymbol.IsAnonymousType);
            sb.AddComment("IsTupleType", typeTargetSymbol.IsTupleType);
            sb.AddComment("IsNativeIntegerType", typeTargetSymbol.IsNativeIntegerType);
            sb.AddComment("SpecialType", typeTargetSymbol.SpecialType);
            sb.AddComment("IsRefLikeType", typeTargetSymbol.IsRefLikeType);
            sb.AddComment("IsUnmanagedType", typeTargetSymbol.IsUnmanagedType);
            sb.AddComment("IsReadOnly", typeTargetSymbol.IsReadOnly);
            sb.AddComment("IsRecord", typeTargetSymbol.IsRecord);
            sb.AddComment("NullableAnnotation", typeTargetSymbol.NullableAnnotation);

            TypeSymbolDocComments = sb.ToString();

            sb.Clear();
        }
        else
        {
            TypeSymbolDocComments = string.Empty;
        }
        
        if (generatorAttributeSyntaxContext.TargetSymbol is INamedTypeSymbol namedTypeTargetSymbol)
        {
            var typeParameters = namedTypeTargetSymbol.TypeParameters.Select(tp => tp.Name).ToArray();
            sb.AddComment("Arity", namedTypeTargetSymbol.Arity);
            sb.AddComment("IsGenericType", namedTypeTargetSymbol.IsGenericType);
            sb.AddComment("IsUnboundGenericType", namedTypeTargetSymbol.IsUnboundGenericType);
            sb.AddComment("IsScriptClass", namedTypeTargetSymbol.IsScriptClass);
            sb.AddComment("IsImplicitClass", namedTypeTargetSymbol.IsImplicitClass);
            sb.AddComment("IsComImport", namedTypeTargetSymbol.IsComImport);
            sb.AddComment("IsFileLocal", namedTypeTargetSymbol.IsFileLocal);
            sb.AddComment("MemberNames", namedTypeTargetSymbol.MemberNames.Count());
            sb.AddComment("TypeParameters", typeParameters.Length);
            for (var n = 0; n < typeParameters.Length; n++)
            {
                var typeParameter = typeParameters[n];
                sb.AddComment($"TypeParameter {n+1}", typeParameter);
            }
            sb.AddComment("InstanceConstructors", namedTypeTargetSymbol.InstanceConstructors.Length);
            sb.AddComment("StaticConstructors", namedTypeTargetSymbol.StaticConstructors.Length);
            sb.AddComment("MightContainExtensionMethods", namedTypeTargetSymbol.MightContainExtensionMethods);
            sb.AddComment("IsSerializable", namedTypeTargetSymbol.IsSerializable);

            NamedTypeSymbolDocComments = sb.ToString();

            sb.Clear();
        }
        else
        {
            NamedTypeSymbolDocComments = string.Empty;
        }
        
        var targetNode = generatorAttributeSyntaxContext.TargetNode;
        sb.AddComment("Type", targetNode.GetType().Name);
        sb.AddComment("RawKind", targetNode.RawKind);
        sb.AddComment("Kind", targetNode.Kind());
        sb.AddComment("Language", targetNode.Language);
        sb.AddComment("Span.Start", targetNode.Span.Start);
        sb.AddComment("Span.Length", targetNode.Span.Length);
        sb.AddComment("ContainsAnnotations", targetNode.ContainsAnnotations);
        sb.AddComment("ContainsDiagnostics", targetNode.ContainsDiagnostics);
        sb.AddComment("ContainsDirectives", targetNode.ContainsDirectives);
        sb.AddComment("ContainsSkippedText", targetNode.ContainsSkippedText);
        sb.AddComment("IsMissing", targetNode.IsMissing);
        sb.AddComment("HasLeadingTrivia", targetNode.HasLeadingTrivia);
        sb.AddComment("HasStructuredTrivia", targetNode.HasStructuredTrivia);
        sb.AddComment("HasTrailingTrivia", targetNode.HasTrailingTrivia);
        sb.AddComment("IsStructuredTrivia", targetNode.IsStructuredTrivia);

        
        TargetNodeDocComments = sb.ToString();

        sb.Clear();

        Flags = AddAttributes(sb, generatorAttributeSyntaxContext.Attributes, true);

        AttributesDocComments = sb.ToString();

        sb.Clear();

        AddAttributes(sb, targetSymbol.GetAttributes(), false);

        AllAttributesDocComments = sb.ToString();

        sb.Clear();
    }

    private static IncludeFlags AddAttributes(StringBuilder sb, ImmutableArray<Microsoft.CodeAnalysis.AttributeData> attributes, bool capture)
    {
        var flags = IncludeFlags.Summary;

        sb.AddComment("Attribute Count", attributes.Length);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attribute = attributes[i];
            sb.AddComment($"[{i}] AttributeClass", attribute.AttributeClass?.ToDisplayString());

            var constructorArguments = attribute.ConstructorArguments;
            var constructorArgumentsLength = constructorArguments.Length;
            sb.AddComment($"[{i}] ConstructorArguments Count", constructorArgumentsLength);
            for (var c = 0; c < constructorArgumentsLength; c++)
            {
                var constructorArgument = constructorArguments[c];
                var attributeName = attribute.AttributeConstructor?.Parameters[c].Name;
                sb.AddComment($"[{i}] AttributeConstructor Parameters {c+1} Name", attributeName);
                sb.AddComment($"[{i}] ConstructorArgument {c+1} Kind", constructorArgument.Kind);
                sb.AddComment($"[{i}] ConstructorArgument {c+1} Type", constructorArgument.Type?.Name);
                sb.AddComment($"[{i}] ConstructorArgument {c+1} Value", constructorArgument.Value);
                
                if (capture)
                {
                    if (constructorArgument.Type?.Name == "IncludeFlags" && constructorArgument.Value != null)
                    {
                        flags |= (IncludeFlags)constructorArgument.Value;
                    }
                    else
                    {
                        // if we're looking at the example of named arguments, include the attribute details
                        flags = IncludeFlags.AttributeContextAttributes;
                    }
                }
            }

            var namedArguments = attribute.NamedArguments;
            var namedArgumentsLength = namedArguments.Length;
            sb.AddComment($"[{i}] NamedArguments Count", namedArgumentsLength);
            for (var n = 0; n < namedArgumentsLength; n++)
            {
                var kvp = namedArguments[n];
                var argName = kvp.Key;
                var namedArgument = kvp.Value;
                sb.AddComment($"[{i}] NamedArgument {n+1} Key", argName);
                sb.AddComment($"[{i}] NamedArgument {n+1} Value Kind", namedArgument.Kind);
                sb.AddComment($"[{i}] NamedArgument {n+1} Value Type", namedArgument.Type?.Name);
                sb.AddComment($"[{i}] NamedArgument {n+1} Value Value", namedArgument.Value);
                if (capture)
                {
                    // if we're looking at the example of named arguments, include the attribute details
                    flags = IncludeFlags.AttributeContextAttributes;
                }
            }
        }

        return flags;
    }
    
    
    public static AttributeData Collector(GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext) => new(generatorAttributeSyntaxContext);
}