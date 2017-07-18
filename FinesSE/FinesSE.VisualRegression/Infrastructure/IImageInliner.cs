namespace FinesSE.VisualRegression.Infrastructure
{
    public interface IImageInliner
    {
        bool TryInlineImage(string imagePath, string width, long maxLength, out string base64src);
    }
}
