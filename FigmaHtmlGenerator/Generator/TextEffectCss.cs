using System.Collections.Generic;
using System.Text;
using FigmaHtmlGenerator.Models;

namespace FigmaHtmlGenerator.Generator
{
    public static class TextEffectCss
    {
        public static string Build(IEnumerable<Effect> effects)
        {
            var sb = new StringBuilder();
            var shadows = new List<string>();
            foreach (var effect in effects)
            {
                if (effect.Type == EffectType.DropShadow && effect.Visible)
                {
                    var color = effect.Color.ToCssRgba();
                    shadows.Add($"{effect.Offset.X}px {effect.Offset.Y}px {effect.Radius}px {color}");
                }
            }
            if (shadows.Count > 0)
            {
                sb.Append("text-shadow: ");
                sb.Append(string.Join(", ", shadows));
                sb.Append("; ");
            }
            foreach (var effect in effects)
            {
                if (effect.Type == EffectType.LayerBlur && effect.Visible)
                {
                    sb.Append($"-webkit-filter: blur({effect.Radius}px); filter: blur({effect.Radius}px);");
                }
            }
            return sb.ToString();
        }
    }
}
