namespace FinesSE.Contracts.Invokable
{
    public interface ILocator
    {
        string Regex { get; }

        LocatedElements Locate(string value);
    }
}
