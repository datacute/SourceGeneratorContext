# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0] - 2025-08-03

### Changed

- Massive simplification by using Datacute.IncrementalGeneratorExtensions
- Changed to use a builder style pattern for the pipeline Combines.
- Replaced Numerous Attribute "Include x" Properties with a single flags enum.
- Generate flags enum and attribute rather than including them in the package. (This results in more consistent package treatment as a source generator.)

### Added

- More docs

## [0.0.2-alpha] - 2025-07-12

### Fixed

- Packaging

## [0.0.1-alpha] - 2025-07-12

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

[Unreleased]: https://github.com/datacute/SourceGeneratorContext/compare/1.0.0...develop
[1.0.0]: https://github.com/datacute/SourceGeneratorContext/releases/tag/1.0.0
[0.0.2-alpha]: https://github.com/datacute/SourceGeneratorContext/releases/tag/0.0.2-alpha
[0.0.1-alpha]: https://github.com/datacute/SourceGeneratorContext/releases/tag/0.0.1-alpha
