using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Datacute.SourceGeneratorContext;

public readonly struct AdditionalTextDescription
{
    public readonly string DocComments;
    public readonly string OptionsComments;

    public AdditionalTextDescription(AdditionalText additionalText, AnalyzerConfigOptions? options = null)
    {
        var sb = new StringBuilder();
        sb.AddComment("Path", additionalText.Path);
        var sourceText = additionalText.GetText();
        sb.AddComment("Length", sourceText?.Length);
        sb.AddComment("Encoding", sourceText?.Encoding?.EncodingName);
        sb.AddComment("Lines", sourceText?.Lines.Count);
        sb.AddComment("ChecksumAlgorithm", sourceText?.ChecksumAlgorithm);
        sb.AddComment("CanBeEmbedded", sourceText?.CanBeEmbedded);

        DocComments = sb.ToString();
        
        sb.Clear();

        if (options != null)
        {
            foreach (var key in options.Keys)
            {
                var v = options.TryGetValue(key, out var value) ? value : string.Empty;
                sb.AddComment(key, v);
            }
        }
        
        OptionsComments = sb.ToString();
    }

    public static AdditionalTextDescription Select(AdditionalText additionalText, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return new AdditionalTextDescription(additionalText);
    }

    public static AdditionalTextDescription Select((AdditionalText additionalText, AnalyzerConfigOptionsProvider optionsProvider) args, CancellationToken token)
    {
        LightweightTrace.Add(TrackingNames.AdditionalTextDescription_Select);

        token.ThrowIfCancellationRequested();
        var options = args.optionsProvider.GetOptions(args.additionalText);
        return new AdditionalTextDescription(args.additionalText, options);
    }
}