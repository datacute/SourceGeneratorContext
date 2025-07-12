namespace Datacute.SourceGeneratorContext;

internal static class NameGenerators
{
    public static string GetHintName(this string typeDisplayName) =>
        $"{typeDisplayName.Replace('<', '_').Replace('>', '_')}.g.cs";
}