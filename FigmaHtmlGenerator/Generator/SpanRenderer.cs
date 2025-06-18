using FigmaHtmlGenerator.Models;
using System.Text;

namespace FigmaHtmlGenerator.Generator
{
    public static class SpanRenderer
    {
        public static string Render(TextNode parent, StyledTextSegmentData segment, bool styleTextSegments)
        {
            var sb = new StringBuilder();
            var characters = segment.Characters;

            if (!string.IsNullOrEmpty(segment.Hyperlink))
                sb.Append($"<a href=\"{segment.Hyperlink}\" target=\"_blank\">");

            if (!segment.IsBaseStyle && styleTextSegments)
                sb.Append($"<span style=\"{segment.Styles.String}\">");
            if (segment.IsOtherWeight.HasValue && styleTextSegments)
                sb.Append($"<span style=\"font-weight: {segment.IsOtherWeight.Value}\">");
            if (segment.IsItalic)
                sb.Append("<i>");
            if (segment.IsBold)
                sb.Append("<b>");

            sb.Append(characters);

            if (segment.IsBold)
                sb.Append("</b>");
            if (segment.IsItalic)
                sb.Append("</i>");
            if (segment.IsOtherWeight.HasValue && styleTextSegments)
                sb.Append("</span>");
            if (!segment.IsBaseStyle && styleTextSegments)
                sb.Append("</span>");
            if (!string.IsNullOrEmpty(segment.Hyperlink))
                sb.Append("</a>");

            return sb.ToString();
        }
    }
}
