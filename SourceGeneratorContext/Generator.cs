using Datacute.IncrementalGeneratorExtensions;
using Microsoft.CodeAnalysis;

namespace Datacute.SourceGeneratorContext;

[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        LightweightTrace.Add(GeneratorStage.Initialize);

        context.RegisterPostInitializationOutput(static postInitializationContext =>
        {
            postInitializationContext.AddSource(
                Templates.IncludeFlagsHintName,
                Templates.IncludeFlags);
            postInitializationContext.AddSource(
                Templates.AttributeHintName,
                Templates.SourceGeneratorContextAttribute);
        });

        var attributeContexts =
            context.SelectAttributeContexts(
                Templates.AttributeFullyQualified,
                AttributeData.Collector);

        var globalOptionsDescriptionValueProvider = 
            context.AnalyzerConfigOptionsProvider
                .Select(AnalyzerConfigOptionsDescription.Selector)
                .WithTrackingName(GeneratorStage.AnalyzerConfigOptionsProviderSelect);

        var compilationDescriptionValueProvider = 
            context.CompilationProvider
                .Select(CompilationDescription.Selector)
                .WithTrackingName(GeneratorStage.CompilationProviderSelect);

        var parseOptionsDescriptionValueProvider = 
            context.ParseOptionsProvider
                .Select(ParseOptionsDescription.Selector)
                .WithTrackingName(GeneratorStage.ParseOptionsProviderSelect);

        var additionalTextDescriptionsValuesProvider = 
            context.AdditionalTextsProvider
                .Combine(context.AnalyzerConfigOptionsProvider)
                .Select(AdditionalTextDescription.Selector)
                .WithTrackingName(GeneratorStage.AdditionalTextsProviderSelect);

        var metadataReferenceDescriptionsValuesProvider = 
            context.MetadataReferencesProvider
                .Select(MetadataReferenceDescription.Selector)
                .WithTrackingName(GeneratorStage.MetadataReferencesProviderSelect);

        var source = attributeContexts
            .Combine(globalOptionsDescriptionValueProvider)
            .Select(SourceBuilder.WithOptions)
            .Combine(compilationDescriptionValueProvider)
            .Select(SourceBuilder.WithCompilation)
            .Combine(parseOptionsDescriptionValueProvider)
            .Select(SourceBuilder.WithParseOptions)
            .CombineEquatable(additionalTextDescriptionsValuesProvider)
            .Select(SourceBuilder.WithAdditionalTexts)
            .CombineEquatable(metadataReferenceDescriptionsValuesProvider)
            .Select(SourceBuilder.WithMetadataReferences);
        
        context.RegisterSourceOutput(source, Action);
    }

    private void Action(SourceProductionContext sourceProductionContext, SourceBuilder source)
    {
        LightweightTrace.Add(GeneratorStage.RegisterSourceOutput);

        var attributeContext = source.AttributeContext;

        var cancellationToken = sourceProductionContext.CancellationToken;
        cancellationToken.ThrowIfCancellationRequested(0);

        var codeGenerator = new CodeGenerator(source.Build(), cancellationToken);

        var hintName = attributeContext.CreateHintName("SourceGeneratorContext");
        var generatedSource = codeGenerator.GetSourceText();
        sourceProductionContext.AddSource(hintName, generatedSource);
    }
}