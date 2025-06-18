using System.Collections.Generic;
using System.Linq;
using FigmaHtmlGenerator.Models;

namespace FigmaHtmlGenerator.Generator
{
    public class WidthRangeInfo
    {
        public List<double> Widths { get; set; } = new();
        public List<(double Min, double Max?)> Ranges { get; set; } = new();
    }

    public static class WidthRange
    {
        public static WidthRangeInfo Calculate(IEnumerable<FrameNode> assets)
        {
            var info = new WidthRangeInfo();
            foreach (var asset in assets)
            {
                var width = asset.Width;
                info.Widths.Add(width);
            }
            info.Widths.Sort();
            for (int i = 0; i < info.Widths.Count; i++)
            {
                double width = info.Widths[i];
                if (i == 0)
                    info.Ranges.Add((0, info.Widths.Count > 1 ? info.Widths[1] - 1 : (double?)null));
                else if (i < info.Widths.Count - 1)
                    info.Ranges.Add((info.Widths[i], info.Widths[i + 1] - 1));
                else
                    info.Ranges.Add((width, null));
            }
            return info;
        }
    }
}
