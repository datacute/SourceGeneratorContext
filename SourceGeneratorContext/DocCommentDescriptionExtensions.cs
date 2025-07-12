using System.Text;

namespace Datacute.SourceGeneratorContext;

public static class  DocCommentDescriptionExtensions
{
    public static void AddComment(this StringBuilder sb, string propertyName, object? value)
    {
        if (value == null) return;
        var valueString = value.ToString();
        sb.AppendFormat(Templates.OptionsLine, propertyName, Templates.EscapeStringForDocComments(valueString));
    }
}