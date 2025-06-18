using System.Collections.Generic;

namespace FigmaHtmlGenerator.Models
{
    public static class ExampleData
    {
        public static FrameNode CreateSampleFrame()
        {
            var textSegment = new StyledTextSegment
            {
                Characters = "Hello World",
                FontSize = 24,
                FontName = new FontName("Arial", "Regular"),
                Fills = new List<Paint> { new Paint { Color = new Color(0,0,0,1), Opacity = 1.0 } }
            };

            var textNode = new TextNode
            {
                X = 0,
                Y = 0,
                Width = 200,
                Height = 30,
                Segments = new List<StyledTextSegment> { textSegment }
            };

            var frame = new FrameNode
            {
                Name = "#600px",
                Width = 600,
                Height = 400,
                TextNodes = new List<TextNode> { textNode }
            };

            return frame;
        }
    }
}
