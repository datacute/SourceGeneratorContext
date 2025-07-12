using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Datacute.SourceGeneratorContext;

// Record structs to replace complex tuple types
public record struct AttributeAndOptions(AttributeContext AttributeContext, AnalyzerConfigOptionsDescription Options);
public record struct AttributeOptionsAndCompilation(AttributeAndOptions AttributeAndOptions, CompilationDescription Compilation);
public record struct AttributeOptionsCompilationAndParseOptions(AttributeOptionsAndCompilation AttributeOptionsAndCompilation, ParseOptionsDescription ParseOptions);
public record struct AttributeOptionsCompilationParseAndAdditionalTexts(AttributeOptionsCompilationAndParseOptions AttributeOptionsCompilationAndParseOptions, ImmutableArray<AdditionalTextDescription> AdditionalTexts);
public record struct GeneratorSourceData(AttributeOptionsCompilationParseAndAdditionalTexts Core, ImmutableArray<MetadataReferenceDescription> MetadataReferences);

[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        LightweightTrace.Add(TrackingNames.Generator_Initialize);

        var attributeContexts =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                    fullyQualifiedMetadataName: Templates.AttributeFullyQualified, 
                    predicate: AttributeContext.Predicate, 
                    transform: AttributeContext.Transform)
                .WithTrackingName(TrackingNames.InitialExtraction);

        var globalOptionsDescriptionValueProvider = context.AnalyzerConfigOptionsProvider.Select(AnalyzerConfigOptionsDescription.Select);
        var compilationDescriptionValueProvider = context.CompilationProvider.Select(CompilationDescription.Select);
        var parseOptionsDescriptionValueProvider = context.ParseOptionsProvider.Select(ParseOptionsDescription.Select);

        // It is possible to get config options FOR each additional text, but showing them is very repetitive
        var additionalTextDescriptionsValuesProvider = context.AdditionalTextsProvider.Combine(context.AnalyzerConfigOptionsProvider).Select(AdditionalTextDescription.Select);
        // var additionalTextDescriptionsValuesProvider = context.AdditionalTextsProvider.Select(AdditionalTextDescription.Select);
        var metadataReferenceDescriptionsValuesProvider = context.MetadataReferencesProvider.Select(MetadataReferenceDescription.Select);

        var source = attributeContexts
            .Combine(globalOptionsDescriptionValueProvider)
            .Select((x, _) => new AttributeAndOptions(x.Left, x.Right))
            .Combine(compilationDescriptionValueProvider)
            .Select((x, _) => new AttributeOptionsAndCompilation(x.Left, x.Right))
            .Combine(parseOptionsDescriptionValueProvider)
            .Select((x, _) => new AttributeOptionsCompilationAndParseOptions(x.Left, x.Right))
            .Combine(additionalTextDescriptionsValuesProvider.Collect())
            .Select((x, _) => new AttributeOptionsCompilationParseAndAdditionalTexts(x.Left, x.Right))
            .Combine(metadataReferenceDescriptionsValuesProvider.Collect())
            .Select((x, _) => new GeneratorSourceData(x.Left, x.Right))
            .WithTrackingName(TrackingNames.Combine);

        context.RegisterSourceOutput(source, Action);
    }

    private void Action(SourceProductionContext sourceProductionContext, GeneratorSourceData source)
    {
        LightweightTrace.Add(TrackingNames.Generator_Action);

        var attributeContext = source.Core.AttributeOptionsCompilationAndParseOptions.AttributeOptionsAndCompilation.AttributeAndOptions.AttributeContext;

        var cancellationToken = sourceProductionContext.CancellationToken;
        cancellationToken.ThrowIfCancellationRequested();

        var codeGenerator = new CodeGenerator(source, cancellationToken);

        var hintName = attributeContext.DisplayString.GetHintName();
        var generatedSource = codeGenerator.GenerateSource();
        sourceProductionContext.AddSource(hintName, generatedSource);
    }
    
    
}