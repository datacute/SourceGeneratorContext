using System;

namespace Datacute.SourceGeneratorContext;

/// <summary>
/// Specifies the types of information to include in the generated source context documentation.
/// </summary>
[Flags]
public enum IncludeFlags : uint
{
    /// <summary>
    /// If no other specific flags are set, a basic summary of the context is included.
    /// This flag is often managed internally by the generator to provide a default output.
    /// </summary>
    Summary = 0,

    /// <summary>
    /// Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol.
    /// </summary>
    AttributeContextTargetSymbol = 1U << 0,

    /// <summary>
    /// Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol when cast to an ITypeSymbol.
    /// </summary>
    AttributeContextTypeSymbol = 1U << 1,

    /// <summary>
    /// Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol when cast to an INamedTypeSymbol.
    /// </summary>
    AttributeContextNamedTypeSymbol = 1U << 2,

    /// <summary>
    /// Includes details about the GeneratorAttributeSyntaxContext.TargetNode.
    /// </summary>
    AttributeContextTargetNode = 1U << 3,

    /// <summary>
    /// Includes details about the GeneratorAttributeSyntaxContext.Attributes.
    /// </summary>
    AttributeContextAttributes = 1U << 4,

    /// <summary>
    /// Includes details about all attributes applied to the target symbol (using GetAttributes()).
    /// </summary>
    AttributeContextAllAttributes = 1U << 5,

    /// <summary>
    /// Includes details from the AnalyzerConfigOptionsProvider's GlobalOptions.
    /// </summary>
    GlobalOptions = 1U << 6,

    /// <summary>
    /// Includes general details about the Compilation.
    /// </summary>
    Compilation = 1U << 7,

    /// <summary>
    /// Includes details about the Compilation's options.
    /// </summary>
    CompilationOptions = 1U << 8,

    /// <summary>
    /// Includes details about the Compilation's assembly.
    /// </summary>
    CompilationAssembly = 1U << 9,

    /// <summary>
    /// Includes counts and names of Compilation's references (e.g., References, DirectiveReferences).
    /// </summary>
    CompilationReferences = 1U << 10,

    /// <summary>
    /// Includes details about the ParseOptionsProvider's ParseOptions.
    /// </summary>
    ParseOptions = 1U << 11,

    /// <summary>
    /// Includes details about AdditionalTextsProvider's AdditionalText entries.
    /// </summary>
    AdditionalTexts = 1U << 12,

    /// <summary>
    /// Includes details about AdditionalTextsProvider's AdditionalText options (from AnalyzerConfigOptions).
    /// </summary>
    AdditionalTextsOptions = 1U << 13,

    /// <summary>
    /// Includes details about MetadataReferencesProvider's MetadataReference entries.
    /// </summary>
    MetadataReferences = 1U << 14,
    
    /// <summary>
    /// Includes all available details about the source generation context.
    /// </summary>
    All = ~0U

}
