using ImageMagick;
using System;
using System.Text;
using System.Web;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class ImageInliner : IImageInliner
    {
        public bool TryInlineImage(string imagePath, string width, long maxLength, out string base64src)
        {
            byte[] bytes;
            base64src = null;

            using (var image = new MagickImage(imagePath))
            {
                if (NeedsSizeCorrection(image, width, out Percentage correction))
                    image.Resize(correction);

                bytes = image.ToByteArray();
                if (bytes.Length > maxLength)
                    return false;
            }

            var builder = new StringBuilder();
            builder.Append("<img src=\"data:image/png;base64,");
            builder.Append(Convert.ToBase64String(bytes));
            builder.Append($"\" width=\"{width}\" title=\"{imagePath}\" alt=\"{imagePath}\"");
            builder.Append($" onclick =\"javascript:window.prompt('Image path', '{HttpUtility.JavaScriptStringEncode(imagePath)}')\" />");
            base64src = builder.ToString();
            return true;
        }

        private bool NeedsSizeCorrection(MagickImage image, string width, out Percentage correction)
        {
            Func<string, string> formatToParsable =
                s => s
                .Replace(" ", "")
                .Replace("%", "");

            var maxWidth = 0d;
            if (width.EndsWith("%"))
                maxWidth = double.Parse(formatToParsable(width)) / 100 * image.Width;
            else
                maxWidth = double.Parse(formatToParsable(width));

            correction = new Percentage(maxWidth / image.Width);
            return image.Width > maxWidth;
        }
    }
}
