using System.Text;
using Datacute.IncrementalGeneratorExtensions;

namespace Datacute.SourceGeneratorContext;

public static class  DocCommentDescriptionExtensions
{
    public static void AddComment(this StringBuilder sb, string propertyName, object? value)
    {
        if (value == null) return;
        var valueString = value.ToString();
        sb.AppendFormat(Templates.OptionsLine, propertyName, Templates.EscapeStringForDocComments(valueString));
    }

    public static void AddComment(this IndentingLineAppender sb, string propertyName, object? value)
    {
        if (value == null) return;
        var valueString = value.ToString();
        sb.AppendFormatLines(Templates.OptionsLine, propertyName, Templates.EscapeStringForDocComments(valueString));
    }
}