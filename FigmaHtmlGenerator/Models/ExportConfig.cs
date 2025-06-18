namespace FigmaHtmlGenerator.Models
{
    public class ExportConfig
    {
        public string Filename { get; set; } = "export";
        public ImageFormat Format { get; set; } = ImageFormat.Png;
        public bool Fluid { get; set; } = true;
        public string AltText { get; set; } = string.Empty;
        public bool StyleTextSegments { get; set; } = true;
        public bool Centered { get; set; } = false;
        public int? MaxWidth { get; set; }
    }
}
