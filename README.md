# Datacute Source Generator Context
A source generator to help creators of source generators, by creating doc-comments showing the available generation context.

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

Many more examples of the flags available to the attribute can be found in the example project on GitHub:

https://github.com/datacute/SourceGeneratorContext/blob/main/SourceGeneratorContextExample/Program.cs

---
<small>
<small>
<small>

###### Datacute - Acute Information Revelation Tools

</small>
</small>
</small>
