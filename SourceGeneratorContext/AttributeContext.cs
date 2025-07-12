using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Datacute.SourceGeneratorContext;

public readonly struct AttributeContext
{
    // Store parent classes with their modifiers
    public record struct ParentClassInfo(
        string Name, 
        bool IsStatic, 
        Accessibility Accessibility,
        string RecordStructOrClass,
        string[] TypeParameters);
    public readonly ParentClassInfo[] ParentClasses { get; }
    public bool HasParentClasses => ParentClasses.Length > 0;

    public readonly bool IncludeSummary;
    public readonly bool IncludeAll;

    public readonly bool IncludeAttributeContextTargetSymbol;
    public readonly bool IncludeAttributeContextTypeSymbol;
    public readonly bool IncludeAttributeContextNamedTypeSymbol;
    public readonly bool IncludeAttributeContextTargetNode;
    public readonly bool IncludeAttributeContextAttributes;
    public readonly bool IncludeAttributeContextAllAttributes;

    public readonly bool IncludeGlobalOptions;
    public readonly bool IncludeCompilation;
    public readonly bool IncludeCompilationOptions;
    public readonly bool IncludeCompilationAssembly;
    public readonly bool IncludeCompilationReferences;
    public readonly bool IncludeParseOptions;
    public readonly bool IncludeAdditionalTexts;
    public readonly bool IncludeAdditionalTextsOptions;
    public readonly bool IncludeMetadataReferences;

    public readonly string TargetSymbolDocComments;
    public readonly string TypeSymbolDocComments;
    public readonly string NamedTypeSymbolDocComments;
    public readonly string TargetNodeDocComments;
    public readonly string AttributesDocComments;
    public readonly string AllAttributesDocComments;

    public readonly bool ContainingNamespaceIsGlobalNamespace;
    public readonly string ContainingNamespaceDisplayString;

    public readonly Accessibility DeclaredAccessibility; // public
    public readonly bool IsStatic;                       // static
    public readonly string RecordStructOrClass;          // (partial) class
    public readonly string Name;                         // ClassName
    public readonly string[] TypeParameters;               // <T, U>
    public readonly string DisplayString;                // Namespace.ClassName
    
    public AttributeContext(in GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext)
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
            TypeParameters = namedTypeTargetSymbol.TypeParameters.Select(tp => tp.Name).ToArray();
            sb.AddComment("Arity", namedTypeTargetSymbol.Arity);
            sb.AddComment("IsGenericType", namedTypeTargetSymbol.IsGenericType);
            sb.AddComment("IsUnboundGenericType", namedTypeTargetSymbol.IsUnboundGenericType);
            sb.AddComment("IsScriptClass", namedTypeTargetSymbol.IsScriptClass);
            sb.AddComment("IsImplicitClass", namedTypeTargetSymbol.IsImplicitClass);
            sb.AddComment("IsComImport", namedTypeTargetSymbol.IsComImport);
            sb.AddComment("IsFileLocal", namedTypeTargetSymbol.IsFileLocal);
            sb.AddComment("MemberNames", namedTypeTargetSymbol.MemberNames.Count());
            sb.AddComment("TypeParameters", TypeParameters.Length);
            for (var n = 0; n < TypeParameters.Length; n++)
            {
                var typeParameter = TypeParameters[n];
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
            TypeParameters = Array.Empty<string>();
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

        (
            IncludeSummary,
            IncludeAll,
            IncludeAttributeContextTargetSymbol,
            IncludeAttributeContextTypeSymbol,
            IncludeAttributeContextNamedTypeSymbol,
            IncludeAttributeContextTargetNode,
            IncludeAttributeContextAttributes,
            IncludeAttributeContextAllAttributes,
            IncludeGlobalOptions,
            IncludeCompilation,
            IncludeCompilationOptions,
            IncludeCompilationAssembly,
            IncludeCompilationReferences,
            IncludeParseOptions,
            IncludeAdditionalTexts,
            IncludeAdditionalTextsOptions,
            IncludeMetadataReferences) = AddAttributes(sb, generatorAttributeSyntaxContext.Attributes, true);

        AttributesDocComments = sb.ToString();

        sb.Clear();

        AddAttributes(sb, targetSymbol.GetAttributes(), false);

        AllAttributesDocComments = sb.ToString();

        sb.Clear();

        // Repeated above, but pulled out for ease of code generation
        var attributeTargetSymbol = (ITypeSymbol)generatorAttributeSyntaxContext.TargetSymbol;

        ContainingNamespaceIsGlobalNamespace = attributeTargetSymbol.ContainingNamespace.IsGlobalNamespace;
        ContainingNamespaceDisplayString = attributeTargetSymbol.ContainingNamespace.ToDisplayString();

        DeclaredAccessibility = attributeTargetSymbol.DeclaredAccessibility;
        IsStatic = attributeTargetSymbol.IsStatic;
        RecordStructOrClass = GetRecordStructOrClass(attributeTargetSymbol);
        Name = attributeTargetSymbol.Name;
        DisplayString = attributeTargetSymbol.ToDisplayString();
        
        // Parse parent classes from symbol's containing types
        var parentClasses = new List<ParentClassInfo>();
        var containingType = attributeTargetSymbol.ContainingType;
        while (containingType != null)
        {
            var typeParams = containingType.TypeParameters.Select(tp => tp.Name).ToArray();
            
            parentClasses.Insert(0, new ParentClassInfo(
                containingType.Name, 
                containingType.IsStatic,
                containingType.DeclaredAccessibility,
                GetRecordStructOrClass(containingType),
                typeParams));
            containingType = containingType.ContainingType;
        }

        ParentClasses = parentClasses.ToArray();
    }

    private static (
        bool includeSummary,
        bool includeAll,
        bool includeAttributeContextTargetSymbol, 
        bool includeAttributeContextTypeSymbol,
        bool includeAttributeContextNamedTypeSymbol,
        bool includeAttributeContextTargetNode,
        bool includeAttributeContextAttributes,
        bool includeAttributeContextAllAttributes,
        bool includeGlobalOptions,
        bool includeCompilation,
        bool includeCompilationOptions,
        bool includeCompilationAssembly,
        bool includeCompilationReferences,
        bool includeParseOptions,
        bool includeAdditionalTexts,
        bool includeAdditionalTextsOptions,
        bool includeMetadataReferences) 
        AddAttributes(StringBuilder sb, ImmutableArray<AttributeData> attributes, bool capture)
    {
        bool includeAll = false;
        bool includeSummary = true;
        bool includeAttributeContextTargetSymbol = false;
        bool includeAttributeContextTypeSymbol = false;
        bool includeAttributeContextNamedTypeSymbol = false;
        bool includeAttributeContextTargetNode = false;
        bool includeAttributeContextAttributes = false;
        bool includeAttributeContextAllAttributes = false;
        bool includeGlobalOptions = false;
        bool includeCompilation = false;
        bool includeCompilationOptions = false;
        bool includeCompilationAssembly = false;
        bool includeCompilationReferences = false;
        bool includeParseOptions = false;
        bool includeAdditionalTexts = false;
        bool includeAdditionalTextsOptions = false;
        bool includeMetadataReferences = false;

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
                
                includeSummary = false;
                includeAttributeContextAttributes = true;
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
                    includeSummary = false;

                    var argumentValue = namedArgument.Value is true;
                    switch (argName)
                    {
                        case "IncludeAll":
                            includeAll = argumentValue;
                            break;
                        case "IncludeAttributeContextTargetSymbol":
                            includeAttributeContextTargetSymbol |= argumentValue;
                            break;
                        case "IncludeAttributeContextTypeSymbol":
                            includeAttributeContextTypeSymbol |= argumentValue;
                            break;
                        case "IncludeAttributeContextNamedTypeSymbol":
                            includeAttributeContextNamedTypeSymbol |= argumentValue;
                            break;
                        case "IncludeAttributeContextTargetNode":
                            includeAttributeContextTargetNode |= argumentValue;
                            break;
                        case "IncludeAttributeContextAttributes":
                            includeAttributeContextAttributes |= argumentValue;
                            break;
                        case "IncludeAttributeContextAllAttributes":
                            includeAttributeContextAllAttributes |= argumentValue;
                            break;
                        case "IncludeGlobalOptions":
                            includeGlobalOptions |= argumentValue;
                            break;
                        case "IncludeCompilation":
                            includeCompilation |= argumentValue;
                            break;
                        case "IncludeCompilationOptions":
                            includeCompilationOptions |= argumentValue;
                            break;
                        case "IncludeCompilationAssembly":
                            includeCompilationAssembly |= argumentValue;
                            break;
                        case "IncludeCompilationReferences":
                            includeCompilationReferences |= argumentValue;
                            break;
                        case "IncludeParseOptions":
                            includeParseOptions |= argumentValue;
                            break;
                        case "IncludeAdditionalTexts":
                            includeAdditionalTexts |= argumentValue;
                            break;
                        case "IncludeAdditionalTextsOptions":
                            includeAdditionalTextsOptions |= argumentValue;
                            break;
                        case "IncludeMetadataReferences":
                            includeMetadataReferences |= argumentValue;
                            break;
                        default:
                            includeAttributeContextAttributes = true;
                            break;
                    } 
                }
            }
        }

        return (
            includeSummary,
            includeAll,
            includeAttributeContextTargetSymbol,
            includeAttributeContextTypeSymbol,
            includeAttributeContextNamedTypeSymbol,
            includeAttributeContextTargetNode,
            includeAttributeContextAttributes,
            includeAttributeContextAllAttributes,
            includeGlobalOptions,
            includeCompilation,
            includeCompilationOptions,
            includeCompilationAssembly,
            includeCompilationReferences,
            includeParseOptions,
            includeAdditionalTexts,
            includeAdditionalTextsOptions,
            includeMetadataReferences);
    }

    private static string GetRecordStructOrClass(ITypeSymbol typeSymbol)
    {
        if (typeSymbol.IsRecord && typeSymbol.IsReferenceType)
            return "record";
        if (typeSymbol.IsRecord)
            return "record struct";
        if (typeSymbol.TypeKind == TypeKind.Interface)
            return "interface";
        if (typeSymbol.IsReferenceType)
            return "class";
        return "struct";
    }

    public static bool Predicate(SyntaxNode syntaxNode, CancellationToken token) => true;  //syntaxNode is TypeDeclarationSyntax,

    public static AttributeContext Transform(GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return new AttributeContext(generatorAttributeSyntaxContext);
    }
}