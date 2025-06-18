using System.Text.RegularExpressions;

namespace FigmaHtmlGenerator.Utils
{
    public static class Slugify
    {
        public static string Slug(string str)
        {
            var slug = Regex.Replace(str, "[^a-zA-Z0-9\s]", "");
            slug = Regex.Replace(slug, "\s+", "-");
            return slug.ToLowerInvariant();
        }
    }
}
