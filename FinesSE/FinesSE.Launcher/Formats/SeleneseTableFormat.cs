namespace FinesSE.Launcher.Formats
{
    public class SeleneseTableFormat : IFormatParser
    {
        public TableFormat Format => TableFormat.SeleneseTable;

        public string Parse(string input)
            => input;
    }
}
