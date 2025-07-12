# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased][Unreleased]


## [0.0.1-alpha]

### Added

- `SourceGeneratorContext` library to help creators of source generators.
- `[SourceGeneratorContext]` attribute to mark partial classes for generation.
- Generate doc-comments to partial classes showing different portions of the context available to source generators.
  - IncludeAll
    - include all available details.
  - IncludeAttributeContextTargetSymbol
    - GeneratorAttributeSyntaxContext.TargetSymbol details
  - IncludeAttributeContextTypeSymbol
    - GeneratorAttributeSyntaxContext.TargetSymbol as ITypeSymbol details
  - IncludeAttributeContextNamedTypeSymbol
    - GeneratorAttributeSyntaxContext.TargetSymbol as INamedTypeSymbol details
  - IncludeAttributeContextTargetNode
    - GeneratorAttributeSyntaxContext.TargetNode details
  - IncludeAttributeContextAttributes
    - GeneratorAttributeSyntaxContext.Attributes details
  - IncludeAttributeContextAllAttributes
    - GeneratorAttributeSyntaxContext.GetAttributes() details
  - IncludeGlobalOptions
    - AnalyzerConfigOptionsProvider's GlobalOptions details
  - IncludeCompilation
    - CompilationProvider's Compilation details
  - IncludeCompilationOptions
    - CompilationProvider's Compilation.Options details
  - IncludeCompilationAssembly
    - CompilationProvider's Compilation.Assembly details
  - IncludeCompilationReferences
    - Counts of CompilationProvider's:
      - Compilation.References
      - Compilation.DirectiveReferences
      - Compilation.ExternalReferences
      - Compilation.ReferencedAssemblyNames
  - IncludeParseOptions
    - ParseOptionsProvider's ParseOptions details
  - IncludeAdditionalTexts
    - AdditionalTextsProvider's AdditionalText details
  - IncludeAdditionalTextsOptions
    - AdditionalTextsProvider's AdditionalText details combined with AnalyzerConfigOptionsProvider's AnalyzerConfigOptions for the AdditionalText
  - IncludeMetadataReferences
    - MetadataReferencesProvider's MetadataReference details
- Diagnostic log of the source generation process and timing included.

[Unreleased]: https://github.com/datacute/SourceGeneratorContext/compare/0.0.1-alpha...HEAD
[0.0.1-alpha]: https://github.com/datacute/SourceGeneratorContext/releases/tag/0.0.1-alpha