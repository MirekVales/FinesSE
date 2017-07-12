using ImageMagick;
using System;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class ImageInliner : IImageInliner
    {
        public bool TryInlineImage(string imagePath, int width, int maxLength, out string base64src)
        {
            byte[] bytes;
            base64src = null;

            using (var image = new MagickImage(imagePath))
            {
                if (image.Width > width)
                    image.Resize(new Percentage(width / (double)image.Width));

                bytes = image.ToByteArray();
                if (bytes.Length > maxLength)
                    return false;
            }

            var base64 = Convert.ToBase64String(bytes);
            base64src = $"<img src=\"data:image/png;base64,{base64}\" width=\"{width}\"/>";
            return true;
        }
    }
}
