using System.Text;
using Microsoft.CodeAnalysis;

namespace Datacute.SourceGeneratorContext;

public readonly struct MetadataReferenceDescription
{
    public readonly string DocComments;
    public MetadataReferenceDescription(MetadataReference metadataReference)
    {
        var sb = new StringBuilder();
        sb.AddComment("Display", metadataReference.Display);
        //sb.AddComment("Properties Kind", metadataReference.Properties.Kind);
        //sb.AddComment("Properties EmbedInteropTypes", metadataReference.Properties.EmbedInteropTypes);
        //sb.AddComment("Properties Aliases Length", metadataReference.Properties.Aliases.Length);

        DocComments = sb.ToString();
    }

    public static MetadataReferenceDescription Select(MetadataReference metadataReference, CancellationToken token)
    {
        //LightweightTrace.Add(TrackingNames.MetadataReferenceDescription_Select);

        token.ThrowIfCancellationRequested();
        return new MetadataReferenceDescription(metadataReference);
    }
}