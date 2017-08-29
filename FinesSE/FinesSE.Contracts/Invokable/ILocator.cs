namespace FinesSE.Contracts.Invokable
{
    public interface ILocator
    {
        string Id { get; }

        string Regex { get; }

        LocatedElements Locate(string value, string modifiers);
    }
}
