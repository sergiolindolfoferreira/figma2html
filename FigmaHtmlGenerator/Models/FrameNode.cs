using System.Collections.Generic;

namespace FigmaHtmlGenerator.Models
{
    public class FrameNode
    {
        public string Name { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public List<TextNode> TextNodes { get; set; } = new();
    }
}
