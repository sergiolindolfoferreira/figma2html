using System.Collections.Generic;
using System.Linq;
using FigmaHtmlGenerator.Models;

namespace FigmaHtmlGenerator.Generator
{
    public class StyleData
    {
        public Dictionary<string, string?> Object { get; set; } = new();
        public string String => string.Join(" ", Object.Where(o => !string.IsNullOrEmpty(o.Value)).Select(o => $"{o.Key}: {o.Value};"));
    }

    public static class StyleProps
    {
        public static StyleData Styles(StyledTextSegment segment)
        {
            if (segment == null) return new StyleData();

            string? color = null;
            string? blend = null;
            if (segment.Fills.Any())
            {
                var fill = segment.Fills.First();
                color = fill.Color.ToCssRgba();
                blend = fill.BlendMode.ToLowerInvariant();
            }

            var obj = new Dictionary<string, string?>
            {
                ["font-family"] = segment.FontName.Family,
                ["font-style"] = segment.FontName.Style.Contains("Italic") ? "italic" : "normal",
                ["font-weight"] = segment.FontWeight.ToString(),
                ["font-size"] = segment.FontSize + "px",
                ["text-decoration"] = segment.TextDecoration.ToLowerInvariant(),
                ["text-transform"] = segment.TextCase == "ORIGINAL" ? "none" : segment.TextCase.ToLowerInvariant(),
                ["line-height"] = segment.LineHeight.Unit == "AUTO" ? "normal" : segment.LineHeight.Unit == "PERCENT" && segment.LineHeight.Value > 0 ? (segment.LineHeight.Value/100).ToString() : segment.LineHeight.Value + "px",
                ["letter-spacing"] = segment.LetterSpacing.Unit == "PERCENT" && segment.LetterSpacing.Value > 0 ? (segment.LetterSpacing.Value/100).ToString() : segment.LetterSpacing.Value + "px",
                ["color"] = color,
                ["mix-blend-mode"] = blend
            };

            return new StyleData { Object = obj };
        }
    }
}
