namespace FigmaHtmlGenerator.Models
{
    public record FontName(string Family, string Style);

    public class LineHeight
    {
        public string Unit { get; set; } = "AUTO"; // AUTO, PIXELS, PERCENT
        public double Value { get; set; }
    }

    public class LetterSpacing
    {
        public string Unit { get; set; } = "PIXELS"; // PIXELS or PERCENT
        public double Value { get; set; }
    }
}
