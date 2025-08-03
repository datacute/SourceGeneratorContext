using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Datacute.SourceGeneratorContext;

public readonly struct ParseOptionsDescription
{
    public readonly string DocComments;

    public ParseOptionsDescription(ParseOptions parseOptions)
    {
        var sb = new StringBuilder();
        sb.AddComment("Kind", parseOptions.Kind);
        sb.AddComment("SpecifiedKind", parseOptions.SpecifiedKind);
        sb.AddComment("DocumentationMode", parseOptions.DocumentationMode);
        sb.AddComment("Language", parseOptions.Language);
        if (parseOptions is CSharpParseOptions cSharpParseOptions)
        {
            sb.AddComment("CSharp SpecifiedLanguageVersion", cSharpParseOptions.SpecifiedLanguageVersion);
            sb.AddComment("CSharp LanguageVersion", cSharpParseOptions.LanguageVersion);
        }
        sb.AddComment("Features Count", parseOptions.Features.Count);
        sb.AddComment("PreprocessorSymbolNames Count", parseOptions.PreprocessorSymbolNames.Count());
        foreach (var preprocessorSymbolName in parseOptions.PreprocessorSymbolNames)
        {
            sb.AddComment($"PreprocessorSymbolName", preprocessorSymbolName);
        }

        DocComments = sb.ToString();
    }

    public static ParseOptionsDescription Selector(ParseOptions parseOptions, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return new ParseOptionsDescription(parseOptions);
    }
}