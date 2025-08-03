# Datacute Source Generator Context
A source generator to help creators of source generators learn what information is available, 
by creating doc-comments showing the available generation context.

# Audience

This source generator is intended for developers creating source generators,
who want to understand the context available to them during the generation process.

# Installation

Add the NuGet package `Datacute.SourceGeneratorContext`

The package includes both the source generator and a marker attribute dll.
The marker attribute is used to indicate whereabouts in your project that the
source generator context should be generated.

The marker attribute does not need to be included in your project's output assembly,
so the package reference in you `.csproj` file can be marked as `PrivateAssets="all"  ExcludeAssets="runtime"` as follows:

```xml
<ItemGroup>
  <PackageReference Include="Datacute.SourceGeneratorContext" Version="1.0.0"
                    PrivateAssets="all" ExcludeAssets="runtime" />
</ItemGroup>
```

# Usage

To use the source generator, create a partial class in your project and decorate it with the `[SourceGeneratorContext]` attribute.

```csharp
using Datacute.SourceGeneratorContext;

[SourceGeneratorContext]
public partial class MySourceGeneratorContext
{
    // This class will be populated with doc-comments showing the available context for source generation.
}
```

# Attribute Flags

The `SourceGeneratorContext` attribute can be configured with flags to control which parts of the context are included in the generated output.

If any value is null or empty, the row is not included in the generated output.

### No flags or `IncludeFlags.Summary` (default)
If no other specific flags are set,  `IncludeFlags.AttributeContextTargetSymbol` is used.

(This will change in a future version to be a small selection of various properties.)

### `IncludeFlags.AttributeContextTargetSymbol`
Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol. This is the default information provided if no flags are specified (see `Summary`).

| Property                              | Value                                                         |
|---------------------------------------|---------------------------------------------------------------|
| Type                                  | The runtime Type of this symbol                               |
| Kind                                  | The kind of this symbol                                       |
| Language                              | The language (C# or VB) that this symbol was declared in      |
| DeclaredAccessibility                 | The declared accessibility of the symbol                      |
| ContainingSymbol Kind                 | The kind of the containing symbol                             |
| ContainingSymbol Name                 | The name of the containing symbol                             |
| ContainingAssembly Name               | The name of the containing assembly                           |
| ContainingModule Name                 | The name of the containing module                             |
| ContainingType Name                   | The name of the containing type                               |
| ContainingType Generic Types          | The number of generic type parameters for the containing type |
| ContainingType Generic Type n         | The display string of the generic type parameter              |
| ContainingNamespace Name              | The name of the containing namespace                          |
| ContainingNamespace IsGlobalNamespace | True if the containing namespace is the global namespace      |
| Name                                  | The name of the symbol                                        |
| MetadataName                          | The name of the symbol as it appears in metadata              |
| MetadataToken                         | The metadata token of the symbol                              |
| IsDefinition                          | True if this symbol is a definition                           |
| IsStatic                              | True if this symbol is static                                 |
| IsVirtual                             | True if this symbol is virtual                                |
| IsOverride                            | True if this symbol is an override                            |
| IsAbstract                            | True if this symbol is abstract (or an interface)             |
| IsSealed                              | True if this symbol is sealed (or a struct)                   |
| IsExtern                              | True if this symbol is extern                                 |
| IsImplicitlyDeclared                  | True if this symbol was implicitly declared                   |
| CanBeReferencedByName                 | True if this symbol can be referenced by name                 |

### `IncludeFlags.AttributeContextTypeSymbol`
Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol when cast to an ITypeSymbol.

| Property             | Value                                           |
|----------------------|-------------------------------------------------|
| TypeKind             | The kind of the type                            |
| BaseType Name        | The name of the base type                       |
| Interfaces Length    | The number of directly implemented interfaces   |
| Interface n          | The display string of the implemented interface |
| AllInterfaces Length | The number of all implemented interfaces        |
| AllInterfaces n      | The display string of the implemented interface |
| IsReferenceType      | True if the type is a reference type            |
| IsValueType          | True if the type is a value type                |
| IsAnonymousType      | True if the type is an anonymous type           |
| IsTupleType          | True if the type is a tuple type                |
| IsNativeIntegerType  | True if the type is a native integer type       |
| SpecialType          | The special type of the type                    |
| IsRefLikeType        | True if the type is a ref-like type             |
| IsUnmanagedType      | True if the type is an unmanaged type           |
| IsReadOnly           | True if the type is read-only                   |
| IsRecord             | True if the type is a record                    |
| NullableAnnotation   | The nullable annotation of the type             |

### `IncludeFlags.AttributeContextNamedTypeSymbol`
Includes details about the GeneratorAttributeSyntaxContext.TargetSymbol when cast to an INamedTypeSymbol.

| Property                     | Value                                            |
|------------------------------|--------------------------------------------------|
| Arity                        | The number of type parameters                    |
| IsGenericType                | True if the type is generic                      |
| IsUnboundGenericType         | True if the type is an unbound generic type      |
| IsScriptClass                | True if the type is a script class               |
| IsImplicitClass              | True if the type is an implicit class            |
| IsComImport                  | True if the type is a COM import                 |
| IsFileLocal                  | True if the type is a file-local type            |
| MemberNames                  | The number of member names                       |
| TypeParameters               | The number of type parameters                    |
| TypeParameter n              | The name of the type parameter                   |
| InstanceConstructors         | The number of instance constructors              |
| StaticConstructors           | The number of static constructors                |
| MightContainExtensionMethods | True if the type might contain extension methods |
| IsSerializable               | True if the type is serializable                 |

### `IncludeFlags.AttributeContextTargetNode`
Includes details about the GeneratorAttributeSyntaxContext.TargetNode.

| Property            | Value                                             |
|---------------------|---------------------------------------------------|
| Type                | The runtime Type of this node                     |
| RawKind             | The raw kind of the node                          |
| Kind                | The kind of the node                              |
| Language            | The language of the node                          |
| Span.Start          | The start position of the node in the source text |
| Span.Length         | The length of the node in the source text         |
| ContainsAnnotations | True if the node contains annotations             |
| ContainsDiagnostics | True if the node contains diagnostics             |
| ContainsDirectives  | True if the node contains directives              |
| ContainsSkippedText | True if the node contains skipped text            |
| IsMissing           | True if the node is missing                       |
| HasLeadingTrivia    | True if the node has leading trivia               |
| HasStructuredTrivia | True if the node has structured trivia            |
| HasTrailingTrivia   | True if the node has trailing trivia              |
| IsStructuredTrivia  | True if the node is structured trivia             |

### `IncludeFlags.AttributeContextAttributes`
Includes details about the GeneratorAttributeSyntaxContext.Attributes.

| Property                                   | Value                                     |
|--------------------------------------------|-------------------------------------------|
| Attribute Count                            | The number of attributes                  |
| [n] AttributeClass                         | The display string of the attribute class |
| [n] ConstructorArguments Count             | The number of constructor arguments       |
| [n] AttributeConstructor Parameters x Name | The name of the constructor parameter     |
| [n] ConstructorArgument x Kind             | The kind of the constructor argument      |
| [n] ConstructorArgument x Type             | The name of the constructor argument type |
| [n] ConstructorArgument x Value            | The value of the constructor argument     |
| [n] NamedArguments Count                   | The number of named arguments             |
| [n] NamedArgument x Key                    | The name of the named argument            |
| [n] NamedArgument x Value Kind             | The kind of the named argument value      |
| [n] NamedArgument x Value Type             | The name of the named argument value type |
| [n] NamedArgument x Value Value            | The value of the named argument           |

### `IncludeFlags.AttributeContextAllAttributes`
Includes details about all attributes applied to the target symbol (using GetAttributes()).

| Property                                   | Value                                     |
|--------------------------------------------|-------------------------------------------|
| Attribute Count                            | The number of attributes                  |
| [n] AttributeClass                         | The display string of the attribute class |
| [n] ConstructorArguments Count             | The number of constructor arguments       |
| [n] AttributeConstructor Parameters x Name | The name of the constructor parameter     |
| [n] ConstructorArgument x Kind             | The kind of the constructor argument      |
| [n] ConstructorArgument x Type             | The name of the constructor argument type |
| [n] ConstructorArgument x Value            | The value of the constructor argument     |
| [n] NamedArguments Count                   | The number of named arguments             |
| [n] NamedArgument x Key                    | The name of the named argument            |
| [n] NamedArgument x Value Kind             | The kind of the named argument value      |
| [n] NamedArgument x Value Type             | The name of the named argument value type |
| [n] NamedArgument x Value Value            | The value of the named argument           |

### `IncludeFlags.GlobalOptions`
Includes details from the `AnalyzerConfigOptionsProvider`'s `GlobalOptions`. Each key-value pair in the options will be added to the generated output.

### `IncludeFlags.Compilation`
Includes general details about the Compilation.

| Property              | Value                                     |
|-----------------------|-------------------------------------------|
| AssemblyName          | The name of the assembly                  |
| Language              | The language of the compilation           |
| IsCaseSensitive       | True if the compilation is case-sensitive |
| DynamicType           | The dynamic type                          |
| GlobalNamespace       | The global namespace                      |
| ObjectType            | The object type                           |
| ScriptClass           | The script class                          |
| SourceModule          | The source module                         |
| ScriptCompilationInfo | The script compilation info               |
| SyntaxTrees Count     | The number of syntax trees                |

### `IncludeFlags.CompilationOptions`
Includes details about the Compilation's options.

| Property                          | Value                                             |
|-----------------------------------|---------------------------------------------------|
| Language                          | The language of the options                       |
| OutputKind                        | The output kind of the compilation                |
| ModuleName                        | The name of the module                            |
| MainTypeName                      | The name of the main type                         |
| ScriptClassName                   | The name of the script class                      |
| CryptoKeyContainer                | The crypto key container                          |
| CryptoKeyFile                     | The crypto key file                               |
| CryptoPublicKey Length            | The length of the crypto public key               |
| DelaySign                         | True if delay signing is enabled                  |
| CheckOverflow                     | True if overflow checking is enabled              |
| Platform                          | The platform of the compilation                   |
| GeneralDiagnosticOption           | The general diagnostic option                     |
| WarningLevel                      | The warning level                                 |
| ReportSuppressedDiagnostics       | True if suppressed diagnostics should be reported |
| OptimizationLevel                 | The optimization level                            |
| ConcurrentBuild                   | True if concurrent build is enabled               |
| Deterministic                     | True if the build is deterministic                |
| MetadataImportOptions             | The metadata import options                       |
| PublicSign                        | True if public signing is enabled                 |
| NullableContextOptions            | The nullable context options                      |
| SpecificDiagnosticOptions Count   | The number of specific diagnostic options         |
| SpecificDiagnosticOptions '{key}' | The value of the specific diagnostic option       |

### `IncludeFlags.CompilationAssembly`
Includes details about the Compilation's assembly.

| Property                     | Value                                                |
|------------------------------|------------------------------------------------------|
| Identity Name                | The name of the assembly identity                    |
| Identity Version             | The version of the assembly identity                 |
| Identity CultureName         | The culture name of the assembly identity            |
| Identity Flags               | The flags of the assembly identity                   |
| Identity ContentType         | The content type of the assembly identity            |
| Identity HasPublicKey        | True if the assembly identity has a public key       |
| Identity PublicKey Length    | The length of the public key                         |
| Identity IsStrongName        | True if the assembly identity has a strong name      |
| Identity IsRetargetable      | True if the assembly identity is retargetable        |
| IsInteractive                | True if the assembly is interactive                  |
| Modules Count                | The number of modules                                |
| TypeNames Count              | The number of type names                             |
| NamespaceNames Count         | The number of namespace names                        |
| MightContainExtensionMethods | True if the assembly might contain extension methods |
| Language                     | The language of the assembly                         |
| Name                         | The name of the assembly                             |
| MetadataName                 | The metadata name of the assembly                    |
| MetadataToken                | The metadata token of the assembly                   |
| Locations Length             | The number of locations                              |
| DeclaredAccessibility        | The declared accessibility of the assembly           |

### `IncludeFlags.CompilationReferences`
Includes counts and names of Compilation's references (e.g., References, DirectiveReferences).

| Property                      | Value                                   |
|-------------------------------|-----------------------------------------|
| References Count              | The number of references                |
| DirectiveReferences Count     | The number of directive references      |
| ExternalReferences Count      | The number of external references       |
| ReferencedAssemblyNames Count | The number of referenced assembly names |

### `IncludeFlags.ParseOptions`
Includes details about the ParseOptionsProvider's ParseOptions.

| Property                        | Value                                   |
|---------------------------------|-----------------------------------------|
| Kind                            | The kind of the parse options           |
| SpecifiedKind                   | The specified kind of the parse options |
| DocumentationMode               | The documentation mode                  |
| Language                        | The language of the parse options       |
| CSharp SpecifiedLanguageVersion | The specified C# language version       |
| CSharp LanguageVersion          | The C# language version                 |
| Features Count                  | The number of features                  |
| PreprocessorSymbolNames Count   | The number of preprocessor symbol names |
| PreprocessorSymbolName          | A preprocessor symbol name              |

### `IncludeFlags.AdditionalTexts`
Includes details about AdditionalTextsProvider's AdditionalText entries.

| Property          | Value                                     |
|-------------------|-------------------------------------------|
| Path              | The path to the additional text file      |
| Length            | The length of the source text             |
| Encoding          | The encoding of the source text           |
| Lines             | The number of lines in the source text    |
| ChecksumAlgorithm | The checksum algorithm of the source text |
| CanBeEmbedded     | True if the source text can be embedded   |

### `IncludeFlags.AdditionalTextsOptions`
Includes details about `AdditionalTextsProvider`'s `AdditionalText` options (from `AnalyzerConfigOptions`).
For each additional text file, each key-value pair in the options will be added to the generated output.

### `IncludeFlags.MetadataReferences`
Includes details about `MetadataReferencesProvider`'s `MetadataReference` entries.
A `MetadataReference` represents a reference to a metadata assembly, such as a .NET assembly.
This allows a source generator to inspect the types and members defined in referenced assemblies,
which can be useful for generating code that interacts with existing libraries.

Due to the potentially large number of metadata references,
the generated output is limited to the display string of each reference.

| Property                      | Value                                    |
|-------------------------------|------------------------------------------|
| Display                       | The display string of the metadata reference |

### `IncludeFlags.All`
Includes all available details about the source generation context, combining the output of all other flags.

### Not all available properties are included

While ths lists above are extensive, there are more properties and 'getter' methods available in the context objects,
and not all the information is included in the generated output.

Additionally, new versions of the Roslyn compiler may add new properties
to the context, which this source generator will not show.

---
<small>
<small>
<small>

###### Datacute - Acute Information Revelation Tools

</small>
</small>
</small>
