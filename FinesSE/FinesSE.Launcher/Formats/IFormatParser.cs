namespace FinesSE.Launcher.Formats
{
    public interface IFormatParser
    {
        TableFormat Format { get; }

        string Parse(string input);
    }
}
