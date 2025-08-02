using Datacute.IncrementalGeneratorExtensions;

namespace Datacute.SourceGeneratorContext;

public readonly record struct SourceBuilder(
    AttributeContextAndData<AttributeData> AttributeContext,
    AnalyzerConfigOptionsDescription? Options,
    CompilationDescription? Compilation,
    ParseOptionsDescription? ParseOptions,
    EquatableImmutableArray<AdditionalTextDescription>? AdditionalTexts,
    EquatableImmutableArray<MetadataReferenceDescription>? MetadataReferences)
{
    public static SourceBuilder WithOptions(
        (AttributeContextAndData<AttributeData> AttributeContext, AnalyzerConfigOptionsDescription Options) data,
        CancellationToken _)
        => new(data.AttributeContext, data.Options, null, null, null, null);

    public static SourceBuilder WithCompilation(
        (SourceBuilder previous, CompilationDescription Compilation) data,
        CancellationToken _)
        => data.previous with { Compilation = data.Compilation };

    public static SourceBuilder WithParseOptions(
        (SourceBuilder previous, ParseOptionsDescription ParseOptions) data,
        CancellationToken _)
        => data.previous with { ParseOptions = data.ParseOptions };

    public static SourceBuilder WithAdditionalTexts(
        (SourceBuilder previous, EquatableImmutableArray<AdditionalTextDescription> AdditionalTexts) data,
        CancellationToken _)
        => data.previous with { AdditionalTexts = data.AdditionalTexts };

    public static SourceBuilder WithMetadataReferences(
        (SourceBuilder previous, EquatableImmutableArray<MetadataReferenceDescription> MetadataReferences) data,
        CancellationToken _)
        => data.previous with { MetadataReferences = data.MetadataReferences };

    /// <summary>
    /// This returns essentially the same data, but no longer nullable.
    /// </summary>
    public GeneratorSourceData Build() => new(
        AttributeContext,
        Options!.Value,
        Compilation!.Value,
        ParseOptions!.Value,
        AdditionalTexts!,
        MetadataReferences!);
}