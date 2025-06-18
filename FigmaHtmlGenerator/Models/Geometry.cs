namespace FigmaHtmlGenerator.Models
{
    public record Offset(double X, double Y);

    public record Color(double R, double G, double B, double A = 1.0)
    {
        public string ToCssRgba() => $"rgba({R * 255:F0}, {G * 255:F0}, {B * 255:F0}, {A})";
    }
}
