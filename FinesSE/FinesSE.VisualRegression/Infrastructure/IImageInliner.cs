namespace FinesSE.VisualRegression.Infrastructure
{
    public interface IImageInliner
    {
        bool TryInlineImage(string imagePath, int width, int maxLength, out string base64src);
    }
}
