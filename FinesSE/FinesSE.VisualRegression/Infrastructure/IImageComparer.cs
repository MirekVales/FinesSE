namespace FinesSE.VisualRegression.Infrastructure
{
    public interface IImageComparer
    {
        double Compare(string path1, string path2);

        void CreateDiffImage(string path1, string path2, string outputPath);
    }
}
