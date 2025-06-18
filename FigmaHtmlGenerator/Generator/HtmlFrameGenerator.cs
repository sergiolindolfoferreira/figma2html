using System.Text;
using System.Linq;
using FigmaHtmlGenerator.Models;
using FigmaHtmlGenerator.Utils;

namespace FigmaHtmlGenerator.Generator
{
    public class FrameContent
    {
        public string Html { get; set; } = string.Empty;
        public string Css { get; set; } = string.Empty;
    }

    public static class HtmlFrameGenerator
    {
        public static FrameContent Generate(FrameNode node, string filename, WidthRangeInfo widthRange, string alt, ExportConfig config)
        {
            var inlineStyle = new StringBuilder();
            var content = new FrameContent();

            double width = node.Width;
            double height = node.Height;
            double aspectRatio = width / height;
            var index = widthRange.Widths.IndexOf(width);
            var range = widthRange.Ranges[index];

            string frameClass = "f2h-frame";
            string id = $"f2h-frame-{width}";
            string format = config.Format.ToString().ToLowerInvariant();

            content.Css += $"\t#{id} {{ position: relative; overflow: hidden; display: none; }}\n";

            var textData = ConvertTextFrames.Convert(node.TextNodes, node);

            if (!config.Fluid)
                inlineStyle.Append($"width: {width}px;");

            content.Html += $"\n\t<!-- Frame: {filename} -->\n";
            content.Html += $"\t<div {Stringify.Attrs(new Dictionary<string, string?>
            {
                ["id"] = id,
                ["class"] = $"{frameClass} frame artboard",
                ["data-aspect-ratio"] = aspectRatio.ToString("F3"),
                ["data-min-width"] = range.Min.ToString(),
                ["data-max-width"] = range.Max?.ToString(),
                ["style"] = inlineStyle.ToString()
            })}>";

            content.Html += $"\n\t\t<div {Stringify.Attrs(new Dictionary<string, string?>
            {
                ["class"] = "spacer",
                ["style"] = Stringify.Styles(new Dictionary<string, string?>
                {
                    ["padding"] = "0 0 0 0",
                    ["min-width"] = width > 0 ? $"{width}px" : "auto",
                    ["max-width"] = range.Max.HasValue ? $"{range.Max.Value}px" : "none"
                })
            })}></div>";

            content.Html += $"\n\t\t<picture>\n\t\t\t<img {Stringify.Attrs(new Dictionary<string, string?>
            {
                ["id"] = "img-" + id,
                ["class"] = "f2h-img",
                ["alt"] = alt,
                ["data-src"] = filename + "." + format,
                ["src"] = "data:image/gif;base64,R0lGODlhCgAKAIAAAB8fHwAAACH5BAEAAAAALAAAAAAKAAoAAAIIhI+py+0PYysAOw==",
                ["loading"] = "lazy",
                ["draggable"] = "false",
                ["decoding"] = "async",
                ["width"] = width.ToString("F2"),
                ["height"] = !config.Fluid ? height.ToString("F2") : null
            })}/>\n\t\t</picture>\n";

            if (textData.Any())
            {
                var baseStyles = textData.Select(t => t.BaseStyle).ToList();
                var pStyle = baseStyles
                    .GroupBy(s => s.String)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.FirstOrDefault();
                if (config.StyleTextSegments && pStyle != null && !string.IsNullOrEmpty(pStyle.String))
                {
                    content.Css += $"\n\t#{id} {pStyle.Tag} {{ {pStyle.String} }}";
                }

                foreach (var text in textData)
                {
                    var style = new Dictionary<string, string?>
                    {
                        ["top"] = text.Y,
                        ["left"] = text.X,
                        ["opacity"] = text.Opacity.ToString(),
                        ["width"] = text.Width
                    };

                    style["transform"] = $"translate({text.Translate}) rotate({text.Rotation}deg)";
                    style["transform-origin"] = "left top";
                    style["text-align"] = text.HorizontalAlignment.ToLowerInvariant();

                    var attrs = Stringify.Attrs(new Dictionary<string, string?>
                    {
                        ["class"] = "f2h-text",
                        ["style"] = Stringify.Styles(style)
                    });

                    content.Html += $"<div {attrs}>";

                    var elements = new List<(string Tag, List<StyledTextSegmentData> Segments)>();
                    for (int i = 0; i < text.Segments.Count; i++)
                    {
                        var seg = text.Segments[i];
                        var prevEndsNew = i > 0 && text.Segments[i - 1].Characters.EndsWith("\n");
                        var thisEndsNew = seg.Characters.EndsWith("\n");
                        var thisIncludesNew = seg.Characters.Contains("\n");
                        bool notNewEl = i > 0 && !prevEndsNew && !(thisIncludesNew && !thisEndsNew);
                        if (notNewEl)
                            elements.Last().Segments.Add(seg);
                        else
                            elements.Add((text.Tag, new List<StyledTextSegmentData> { seg }));
                    }

                    foreach (var element in elements)
                    {
                        content.Html += $"\n\t\t\t<{element.Tag}>";
                        foreach (var seg in element.Segments)
                        {
                            content.Html += SpanRenderer.Render(text.Node, seg, config.StyleTextSegments);
                        }
                        content.Html += $"</{element.Tag}>\n";
                    }

                    content.Html += "\t\t</div>\n";
                }
            }

            content.Html += "\t</div>\n";
            return content;
        }
    }
}
