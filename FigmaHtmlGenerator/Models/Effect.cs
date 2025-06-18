namespace FigmaHtmlGenerator.Models
{
    public class Effect
    {
        public EffectType Type { get; set; }
        public bool Visible { get; set; } = true;
        public Offset Offset { get; set; } = new(0,0);
        public double Radius { get; set; }
        public Color Color { get; set; } = new(0,0,0,1);
    }
}
