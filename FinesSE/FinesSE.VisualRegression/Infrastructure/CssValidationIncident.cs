namespace FinesSE.VisualRegression.Infrastructure
{
    public class CssValidationIncident
    {
        public int Column { get; }
        public int Line { get; }
        public string Message { get; }
        public object Tag { get; set; }

        public CssValidationIncident(int column, int line, string message)
        {
            Column = column;
            Line = line;
            Message = message;
        }

        public override string ToString()
            => $"Column {Column} line {Line}: {Message}";
    }
}
