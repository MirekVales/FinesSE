namespace FinesSE.Contracts.Invokable
{
    public class OptionLocator
    {
        public string Locator
            => $"{Type}={Value}";
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
