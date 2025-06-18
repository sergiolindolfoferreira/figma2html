using System.Collections.Generic;
using System.Linq;
using System.Text;
using FigmaHtmlGenerator.Models;
using FigmaHtmlGenerator.Utils;

namespace FigmaHtmlGenerator.Generator
{
    public static class HtmlWrapper
    {
        public static string Generate(IEnumerable<FrameNode> assets, ExportConfig config, object? variables)
        {
            var containerId = $"{config.Filename}-box";
            var frameCss = new StringBuilder();
            var frameHtml = new StringBuilder();

            var widthRange = WidthRange.Calculate(assets);

            foreach (var asset in assets)
            {
                var frame = HtmlFrameGenerator.Generate(asset, config.Filename, widthRange, config.AltText, config);
                frameCss.Append("\n\n" + frame.Css);
                frameHtml.Append("\n\n" + frame.Html);
            }

            var css = new StringBuilder();
            css.AppendLine($"#{containerId} {{ max-width: {(config.MaxWidth.HasValue ? config.MaxWidth + "px" : "none")}; margin: {(config.Centered ? "0 auto" : "0")}; }}");
            css.AppendLine($"#{containerId} .f2h-frame {{ margin: {(config.Centered ? "0 auto" : "0")}; }}");
            css.Append(frameCss.ToString());

            var html = new StringBuilder();
            html.AppendLine($"<style type='text/css'>{css}</style>");
            html.AppendLine($"<div id=\"{containerId}\" class=\"figma2html\">{frameHtml}</div>");

            return html.ToString();
        }
    }
}
