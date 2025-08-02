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
    /// [SourceGeneratorContext(IncludeFlags.All)]
    /// internal partial class Example;
    /// </code>
    /// </remarks>
    public SourceGeneratorContextAttribute(IncludeFlags flags = IncludeFlags.Summary)
    {
        IncludeFlags = flags;
    }

    /// <summary>
    /// Flags to control what information is included in the generated doc-comments.
    /// </summary>
    public IncludeFlags IncludeFlags { get; set; }

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
            string exampleOptionalParameter) // only used for demonstrating working with Constructor Arguments
    {
        // The constructor arguments do not need to be assigned to fields or properties
        // as the source of the supplied values is what is available to the source generator
    }

    #endregion
}