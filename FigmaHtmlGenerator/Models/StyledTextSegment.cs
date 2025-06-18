using System.Collections.Generic;

namespace FigmaHtmlGenerator.Models
{
    public class StyledTextSegment
    {
        public string Characters { get; set; } = string.Empty;
        public FontName FontName { get; set; } = new("Arial", "Regular");
        public int FontWeight { get; set; } = 400;
        public double FontSize { get; set; }
        public string TextDecoration { get; set; } = "none";
        public string TextCase { get; set; } = "ORIGINAL";
        public LineHeight LineHeight { get; set; } = new();
        public LetterSpacing LetterSpacing { get; set; } = new();
        public List<Paint> Fills { get; set; } = new();
        public string? Hyperlink { get; set; }
    }

    public class Paint
    {
        public Color Color { get; set; } = new(0,0,0,1);
        public double Opacity { get; set; } = 1.0;
        public string BlendMode { get; set; } = "NORMAL";
    }
}
