using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global Properties getters are not used as the source generator reads the source code.
// ReSharper disable UnusedParameter.Local Unused parameters are used to demonstrate behaviour.

namespace Datacute.SourceGeneratorContext;

/// <summary>
/// Add this attribute to a partial class to generate doc-comments detailing the source generation context.
/// </summary>
[System.Diagnostics.Conditional("DATACUTE_SOURCEGENERATORCONTEXTATTRIBUTE_USAGES")]
[AttributeUsage(
    validOn: AttributeTargets.Class | 
             AttributeTargets.Interface | 
             AttributeTargets.Struct, // Method and Property should be allowed too
    Inherited = true, // Inherited to show how SyntaxProvider.ForAttributeWithMetadataName doesn't support inheritance
    AllowMultiple = true)] // AllowMultiple to show the differences when multiple attributes are applied
public class SourceGeneratorContextAttribute : Attribute
{
    /// <remarks>
    /// There is a huge amount of information available, but Visual Studio does not scroll doc-comments.
    /// So either IncludeAll and view the generated source, or set one of the named parameters to control what gets output:
    /// <code>
    /// [SourceGeneratorContext(IncludeAll = true)]
    /// internal partial class Example;
    /// </code>
    /// </remarks>
    public SourceGeneratorContextAttribute()
    {
    }

    /// <summary>
    /// Set to true to include all available details.
    /// </summary>
    public bool IncludeAll { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.TargetSymbol details.
    /// </summary>
    public bool IncludeAttributeContextTargetSymbol { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.TargetSymbol as ITypeSymbol details.
    /// </summary>
    public bool IncludeAttributeContextTypeSymbol { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.TargetSymbol as INamedTypeSymbol details.
    /// </summary>
    public bool IncludeAttributeContextNamedTypeSymbol { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.TargetNode details.
    /// </summary>
    public bool IncludeAttributeContextTargetNode { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.Attributes details.
    /// </summary>
    public bool IncludeAttributeContextAttributes { get; set; }

    /// <summary>
    /// Set to true to include the GeneratorAttributeSyntaxContext.GetAttributes() details.
    /// </summary>
    public bool IncludeAttributeContextAllAttributes { get; set; }

    /// <summary>
    /// Set to true to include the AnalyzerConfigOptionsProvider's GlobalOptions details.
    /// </summary>
    public bool IncludeGlobalOptions { get; set; }

    /// <summary>
    /// Set to true to include the CompilationProvider's Compilation details.
    /// </summary>
    public bool IncludeCompilation { get; set; }

    /// <summary>
    /// Set to true to include the CompilationProvider's Compilation.Options details.
    /// </summary>
    public bool IncludeCompilationOptions { get; set; }

    /// <summary>
    /// Set to true to include the CompilationProvider's Compilation.Assembly details.
    /// </summary>
    public bool IncludeCompilationAssembly { get; set; }

    /// <summary>
    /// Set to true to include the Counts of CompilationProvider's Compilation.References, Compilation.DirectiveReferences, Compilation.ExternalReferences, and Compilation.ReferencedAssemblyNames.
    /// </summary>
    public bool IncludeCompilationReferences { get; set; }

    /// <summary>
    /// Set to true to include the ParseOptionsProvider's ParseOptions details.
    /// </summary>
    public bool IncludeParseOptions { get; set; }

    /// <summary>
    /// Set to true to include the AdditionalTextsProvider's AdditionalText details.
    /// </summary>
    public bool IncludeAdditionalTexts { get; set; }

    /// <summary>
    /// Set to true to include the AdditionalTextsProvider's AdditionalText details combined with AnalyzerConfigOptionsProvider's AnalyzerConfigOptions for the AdditionalText.
    /// </summary>
    public bool IncludeAdditionalTextsOptions { get; set; }

    /// <summary>
    /// Set to true to include the MetadataReferencesProvider's MetadataReference details.
    /// </summary>
    public bool IncludeMetadataReferences { get; set; }


    #region Demonstration purposes only

    /// <summary>
    /// Example of a named parameter.
    /// </summary>
    public string ExampleNamedParameter { get; set; } =
        string.Empty; // only used for demonstrating working with Named Parameters

    /// <summary>
    /// Example of an optional parameter.
    /// </summary>
    /// <param name="exampleOptionalParameter"></param>
    public
        SourceGeneratorContextAttribute(
            string? exampleOptionalParameter =
                null) // only used for demonstrating working with Constructor Arguments
    {
        // The constructor arguments do not need to be assigned to fields or properties
        // as the source of the supplied values is what is available to the source generator
    }

    #endregion
}