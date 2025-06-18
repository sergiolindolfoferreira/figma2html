using System.Collections.Generic;

namespace FigmaHtmlGenerator.Models
{
    public class TextNode
    {
        public string Name { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Opacity { get; set; } = 1.0;
        public double Rotation { get; set; }
        public string TextAlignHorizontal { get; set; } = "LEFT";
        public string TextAlignVertical { get; set; } = "TOP";
        public string TextAutoResize { get; set; } = "NONE"; // WIDTH_AND_HEIGHT etc
        public List<StyledTextSegment> Segments { get; set; } = new();
        public List<Effect> Effects { get; set; } = new();
    }
}
