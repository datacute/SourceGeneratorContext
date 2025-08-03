using Datacute.IncrementalGeneratorExtensions;

namespace Datacute.SourceGeneratorContext;

public readonly record struct GeneratorSourceData(
    AttributeContextAndData<AttributeData> AttributeContext,
    AnalyzerConfigOptionsDescription Options,
    CompilationDescription Compilation,
    ParseOptionsDescription ParseOptions,
    EquatableImmutableArray<AdditionalTextDescription> AdditionalTexts,
    EquatableImmutableArray<MetadataReferenceDescription> MetadataReferences);