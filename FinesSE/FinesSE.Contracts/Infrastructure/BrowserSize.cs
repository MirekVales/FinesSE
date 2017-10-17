using System.Collections.Generic;
using System.Drawing;

namespace FinesSE.Contracts.Infrastructure
{
    public class BrowserSize
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public BrowserSize(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

        public static IEnumerable<BrowserSize> GetDefaultBrowserSizes()
        {
            yield return new BrowserSize("V320x480", 320, 480);
            yield return new BrowserSize("V480x720", 480, 720);
            yield return new BrowserSize("V480x800", 480, 800);
            yield return new BrowserSize("V640x960", 640, 960);
            yield return new BrowserSize("V768x1024", 768, 1024);

            yield return new BrowserSize("H480x320", 480, 320);
            yield return new BrowserSize("H720x480", 720, 480);
            yield return new BrowserSize("H800x480", 800, 480);
            yield return new BrowserSize("H960x640", 960, 640);
            yield return new BrowserSize("H1024x600", 1024, 600);
            yield return new BrowserSize("H1024x768", 1024, 768);
            yield return new BrowserSize("H1280x800", 1280, 800);
            yield return new BrowserSize("H1366x768", 1366, 768);
            yield return new BrowserSize("H1440x900", 1440, 900);
            yield return new BrowserSize("H1920x1080", 1920, 1080);
        }

        public Size AsDrawingSize()
            => new Size(Width, Height);
    }
}
