using System.Text;
using Microsoft.CodeAnalysis;

namespace Datacute.SourceGeneratorContext;

public readonly record struct MetadataReferenceDescription
{
    public readonly string DocComments;
    private MetadataReferenceDescription(MetadataReference metadataReference)
    {
        var sb = new StringBuilder();
        sb.AddComment("Display", metadataReference.Display);
        //sb.AddComment("Properties Kind", metadataReference.Properties.Kind);
        //sb.AddComment("Properties EmbedInteropTypes", metadataReference.Properties.EmbedInteropTypes);
        //sb.AddComment("Properties Aliases Length", metadataReference.Properties.Aliases.Length);

        DocComments = sb.ToString();
    }

    public static MetadataReferenceDescription Selector(MetadataReference metadataReference, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return new MetadataReferenceDescription(metadataReference);
    }
}