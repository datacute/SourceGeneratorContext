using Datacute.SourceGeneratorContext;

Console.WriteLine("View the doc-comments for the items with the [SourceGeneratorContext] attribute");

// In order to demonstrate various scenarios, we're creating a lot of unused code, so...
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedType.Local
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedTypeParameter
#pragma warning disable CA1050 // Declare types in namespaces

#region Simple example

// class needs to be partial to allow for the source generator to extend it (adding doc comments in this case)
[SourceGeneratorContext]
public static partial class ClassInGlobalNamespace;

#endregion

#region All 'include' options

[SourceGeneratorContext(IncludeFlags.AttributeContextTargetSymbol)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextTargetSymbol;

[SourceGeneratorContext(IncludeFlags.AttributeContextTypeSymbol)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextTypeSymbol;

[SourceGeneratorContext(IncludeFlags.AttributeContextNamedTypeSymbol)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextNamedTypeSymbol;

[SourceGeneratorContext(IncludeFlags.AttributeContextTargetNode)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextTargetNode;

[SourceGeneratorContext(IncludeFlags.AttributeContextAttributes)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextAttributes;

[SourceGeneratorContext(IncludeFlags.AttributeContextAllAttributes)]
public partial class ViewClassDocsToSeeGeneratorAttributeSyntaxContextAllAttributes;

[SourceGeneratorContext(IncludeFlags.GlobalOptions)]
public partial class ViewClassDocsToSeeAnalyzerConfigOptionsProviderGlobalOptions;

[SourceGeneratorContext(IncludeFlags.Compilation)]
public partial class ViewClassDocsToSeeCompilation;

[SourceGeneratorContext(IncludeFlags.CompilationOptions)]
public partial class ViewClassDocsToSeeCompilationOptions;

[SourceGeneratorContext(IncludeFlags.CompilationAssembly)]
public partial class ViewClassDocsToSeeCompilationAssembly;

[SourceGeneratorContext(IncludeFlags.CompilationReferences)]
public partial class ViewClassDocsToSeeCompilationReferences;

[SourceGeneratorContext(IncludeFlags.ParseOptions)]
public partial class ViewClassDocsToSeeParseOptions;

[SourceGeneratorContext(IncludeFlags.AdditionalTexts)]
public partial class ViewClassDocsToSeeAdditionalTexts;

[SourceGeneratorContext(IncludeFlags.MetadataReferences)]
public partial class ViewClassDocsToSeeMetadataReferences;

// IncludeAdditionalTextsOptions seems really repetitive, so is not included in "includeAll"
[SourceGeneratorContext(IncludeFlags.AdditionalTexts | IncludeFlags.AdditionalTextsOptions)]
public partial class ViewClassDocsToSeeAdditionalTextsWithOptions;

[SourceGeneratorContext(IncludeFlags.All)]
public partial class ViewClassDocsToSeeAllAvailableDetails;

#endregion

#region Inner classes

[SourceGeneratorContext]
public partial class ParentClassInGlobalNamespace: IFormattable
{
    public string ToString(string? format, IFormatProvider? formatProvider) => string.Empty;

    public override string ToString() => string.Empty;

    [SourceGeneratorContext]
    public static partial class InnerStaticClassWithinParentClass
    {
        [SourceGeneratorContext]
        private static partial class InnerClassWithinMultipleParents;

        [SourceGeneratorContext]
        internal partial class InnerNonStaticClassWithinMultipleParents;
    }

    // parent class needs to be partial to allow the child classes to be partial
    internal partial class InnerNonStaticClassWithinParentClass
    {
        [SourceGeneratorContext]
        private static partial class InnerClassWithinMultipleParents;

        [SourceGeneratorContext]
        private partial class InnerNonStaticClassWithinMultipleParents;
    }
}

#endregion

#region Inheritance scenarios

// Visual Studio only shows these below comments, not the generated comments.
// Jetbrains Rider shows both the generated comments then the below comments.
/// <summary>
/// This is an example of doc comments on both the user written class, and the generated class. 
/// </summary>
/// <remarks>Example remarks</remarks>
[SourceGeneratorContext]
public partial class ClassWithOwnDocComments;

[SourceGeneratorContext]
public partial class SubclassExample : ParentClassInGlobalNamespace;

// Uncommenting this attribute on a repeated class will break the source generation, due to a duplicate hint
// The source generator is supposed to be able to handle repeated hint values,
// so perhaps this is IDE and/or build tool dependent
//[SourceGeneratorContext]
public partial class SubclassExample: ParentClassInGlobalNamespace;

// even though the attribute is inherited,
// the generator will still only generate the doc-comments for the class with the attribute
// this is due to the implementation of the context.SyntaxProvider.ForAttributeWithMetadataName method
public partial class OverriddenClassWithoutAttribute: ParentClassInGlobalNamespace;

// This inheritdoc DOES NOT show the doc-comments from the generated ClassWithOwnDocComments,
// but only the doc-comments from ClassWithOwnDocComments above
/// <inheritdoc/>
public partial class OverriddenClassDemonstratingInheritDocWithoutGeneratedComments: ClassWithOwnDocComments;

// This inheritdoc DOES show the doc-comments from ParentClassInGlobalNamespace,
// because ParentClassInGlobalNamespace does not have its own docs above.
// Note though that the generated comments are for the context of the attribute on the ParentClassInGlobalNamespace class
// not the context for this class
/// <inheritdoc/>
public partial class OverriddenClassDemonstratingInheritDocWithGeneratedComments : ParentClassInGlobalNamespace;

#endregion

#region Various example scenarios

namespace ExampleNamespace
{
    [SourceGeneratorContext]
    internal static partial class ClassInNamespace;

    [SourceGeneratorContext(IncludeFlags.AttributeContextTypeSymbol)]
    internal partial record RecordClass(string Example)
    {
        [SourceGeneratorContext]
        internal partial class InnerClassWithinRecord;
    }

    [SourceGeneratorContext]
    public static partial class ParentClass
    {
        [SourceGeneratorContext]
        internal static partial class InnerClassWithinParentClass
        {
            [Obsolete("Included in `TargetSymbol.AllAttributes()`")]
            [SourceGeneratorContext("Included in `Attributes` and `TargetSymbol.AllAttributes()`")]
            [SourceGeneratorContext(IncludeFlags.AttributeContextAllAttributes)]
            private static partial class InnerClassWithinMultipleParents;
        }

        [SourceGeneratorContext(exampleOptionalParameter: "Example of a supplied parameter")]
        internal partial class InnerGenericClass<T1,T2>
        {
            [SourceGeneratorContext(ExampleNamedParameter = "Example of a named parameter")]
            internal partial class InnerClassOfGenericClass;
        }
    }
}

[SourceGeneratorContext(IncludeFlags.AttributeContextTypeSymbol)]
public partial struct ExampleStructure;

[SourceGeneratorContext(IncludeFlags.AttributeContextTypeSymbol)]
public partial interface IExampleInterface;


#endregion

#pragma warning restore CA1050 // Declare types in namespaces
