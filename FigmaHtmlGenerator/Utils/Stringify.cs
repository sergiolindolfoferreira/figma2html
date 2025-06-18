using System.Collections.Generic;
using System.Linq;

namespace FigmaHtmlGenerator.Utils
{
    public static class Stringify
    {
        public static string Attrs(Dictionary<string, string?> attributes)
        {
            return string.Join(" ", attributes
                .Where(kv => !string.IsNullOrEmpty(kv.Value))
                .Select(kv => $"{kv.Key}=\"{kv.Value}\""));
        }

        public static string Styles(Dictionary<string, string?> styles)
        {
            return string.Join(" ", styles
                .Where(kv => !string.IsNullOrEmpty(kv.Value))
                .Select(kv => $"{kv.Key}: {kv.Value};"));
        }
    }
}
