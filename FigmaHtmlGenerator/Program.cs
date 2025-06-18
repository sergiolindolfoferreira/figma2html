using System;
using FigmaHtmlGenerator.Generator;
using FigmaHtmlGenerator.Models;

namespace FigmaHtmlGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage with placeholder frame data
            var frame = ExampleData.CreateSampleFrame();
            var config = new ExportConfig
            {
                Filename = "example",
                Format = ImageFormat.Png,
                Fluid = true,
                AltText = "Sample image",
                StyleTextSegments = true
            };

            string html = HtmlWrapper.Generate(new[] { frame }, config, null);
            Console.WriteLine(html);
        }
    }
}
