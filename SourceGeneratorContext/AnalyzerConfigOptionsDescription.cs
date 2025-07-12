using System.Text;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Datacute.SourceGeneratorContext;

public readonly struct AnalyzerConfigOptionsDescription
{
    public readonly string DocComments;

    public AnalyzerConfigOptionsDescription(AnalyzerConfigOptions options)
    {
        var sb = new StringBuilder();
        foreach (var key in options.Keys)
        {
            var v = options.TryGetValue(key, out var value) ? value : string.Empty;
            sb.AddComment(key, v);
        }
        DocComments = sb.ToString();
    }

    public static AnalyzerConfigOptionsDescription Select(AnalyzerConfigOptionsProvider provider, CancellationToken token)
    {
        LightweightTrace.Add(TrackingNames.AnalyzerConfigOptionsDescription_Select);

        token.ThrowIfCancellationRequested();
        return new AnalyzerConfigOptionsDescription(provider.GlobalOptions);
    }
}