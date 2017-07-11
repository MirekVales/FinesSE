using ImageMagick;
using System;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class MagickImageComparer : IImageComparer
    {
        public double Compare(string path1, string path2)
        {
            using (var image1 = new MagickImage(path1))
            using (var image2 = new MagickImage(path2))
            {
                var pixels = image1.Width * image1.Height;
                return Math.Round(image1.Compare(image2, ErrorMetric.Absolute, Channels.All) / pixels, 2);
            }
        }
    }
}
