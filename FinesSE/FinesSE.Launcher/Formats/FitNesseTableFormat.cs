namespace FinesSE.Launcher.Formats
{
    public class FitNesseTableFormat : IFormatParser
    {
        public TableFormat Format => TableFormat.FitNesseTable;

        public string Parse(string input)
            => input;
    }
}
