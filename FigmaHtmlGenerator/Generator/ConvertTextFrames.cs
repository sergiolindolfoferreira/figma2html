using System.Collections.Generic;
using System.Linq;
using FigmaHtmlGenerator.Models;
using FigmaHtmlGenerator.Utils;

namespace FigmaHtmlGenerator.Generator
{
    public class TextFrameData
    {
        public TextNode Node { get; set; }
        public string Tag { get; set; } = "p";
        public string? CustomClasses { get; set; }
        public Dictionary<string, string?> CustomAttributes { get; set; } = new();
        public string Class { get; set; } = string.Empty;
        public string ElId { get; set; } = string.Empty;
        public List<StyledTextSegmentData> Segments { get; set; } = new();
        public StyleData BaseStyle { get; set; } = new();
        public string X { get; set; } = "0%";
        public string Y { get; set; } = "0%";
        public string HorizontalAlignment { get; set; } = "LEFT";
        public string VerticalAlignment { get; set; } = "TOP";
        public string Width { get; set; } = "auto";
        public double Opacity { get; set; }
        public string Translate { get; set; } = "0%,0%";
        public double Rotation { get; set; }
        public List<Effect> Effect { get; set; } = new();
    }

    public class StyledTextSegmentData
    {
        public string Characters { get; set; } = string.Empty;
        public int Start { get; set; }
        public int End { get; set; }
        public string? Hyperlink { get; set; }
        public StyleData Styles { get; set; } = new();
        public bool IsBaseStyle { get; set; }
        public bool IsBold { get; set; }
        public double? IsOtherWeight { get; set; }
        public bool IsItalic { get; set; }
    }

    public static class ConvertTextFrames
    {
        static readonly string[] BaseStyleFields = new[]
        {
            "font-family","font-size","letter-spacing","color","line-height","mix-blend-mode","text-decoration","text-transform"
        };

        public static List<TextFrameData> Convert(IEnumerable<TextNode> textFrames, FrameNode artboard)
        {
            var frames = new List<TextFrameData>();
            int idx = 0;
            foreach (var textFrame in textFrames)
            {
                var elId = $"f2h-text-{idx++}";
                var textSegments = new List<StyledTextSegmentData>();

                var segments = textFrame.Segments;
                var baseStyle = segments.FirstOrDefault() != null ? StyleProps.Styles(segments.First()) : new StyleData();

                int segIdx = 0;
                foreach (var seg in segments)
                {
                    var styles = StyleProps.Styles(seg);
                    bool isBase = segIdx == 0 || !BaseStyleFields.Any(f => styles.Object.TryGetValue(f, out var v1) && baseStyle.Object.TryGetValue(f, out var v2) && v1 != v2);
                    bool isBold = isBase && styles.Object.TryGetValue("font-weight", out var fw) && fw == "700";
                    double? isOtherWeight = isBase && styles.Object.TryGetValue("font-weight", out var fw2) && fw2 != "400" && fw2 != "700" ? double.Parse(fw2) : null;
                    bool isItalic = isBase && styles.Object.TryGetValue("font-style", out var fs) && fs == "italic";

                    textSegments.Add(new StyledTextSegmentData
                    {
                        Characters = seg.Characters,
                        Hyperlink = seg.Hyperlink,
                        Styles = styles,
                        IsBaseStyle = isBase,
                        IsBold = isBold,
                        IsOtherWeight = isOtherWeight,
                        IsItalic = isItalic
                    });
                    segIdx++;
                }

                double x = 0, y = 0, translateX = 0, translateY = 0;
                switch (textFrame.TextAlignHorizontal)
                {
                    case "CENTER":
                        x = (textFrame.X / artboard.Width + (textFrame.Width / artboard.Width) / 2) * 100;
                        translateX = -50;
                        break;
                    case "RIGHT":
                        x = ((textFrame.X + textFrame.Width) / artboard.Width) * 100;
                        translateX = -100;
                        break;
                    default:
                        x = (textFrame.X / artboard.Width) * 100;
                        break;
                }

                switch (textFrame.TextAlignVertical)
                {
                    case "CENTER":
                        y = ((textFrame.Y + textFrame.Height / 2) / artboard.Height) * 100;
                        translateY = -50;
                        break;
                    case "BOTTOM":
                        y = ((textFrame.Y + textFrame.Height) / artboard.Height) * 100;
                        translateY = -100;
                        break;
                    default:
                        y = (textFrame.Y / artboard.Height) * 100;
                        break;
                }

                frames.Add(new TextFrameData
                {
                    Node = textFrame,
                    Tag = "p",
                    Class = string.Empty,
                    ElId = elId,
                    Segments = textSegments,
                    BaseStyle = baseStyle,
                    X = $"{x:F2}%",
                    Y = $"{y:F2}%",
                    HorizontalAlignment = textFrame.TextAlignHorizontal,
                    VerticalAlignment = textFrame.TextAlignVertical,
                    Width = textFrame.TextAutoResize == "WIDTH_AND_HEIGHT" ? "auto" : $"{textFrame.Width:F2}px",
                    Opacity = textFrame.Opacity,
                    Translate = $"{translateX}%, {translateY}%",
                    Rotation = -textFrame.Rotation,
                    Effect = textFrame.Effects
                });
            }
            return frames;
        }
    }
}
