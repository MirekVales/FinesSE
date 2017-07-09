namespace FinesSE.Contracts.Infrastructure
{
    public interface ISeleneseProxy
    {
        string Invoke<T>(params string[] arguments);
    }
}
