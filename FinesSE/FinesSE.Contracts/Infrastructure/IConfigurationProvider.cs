namespace FinesSE.Contracts.Infrastructure
{
    public interface IConfigurationProvider
    {
        bool ConfigurationFound { get; }

        T Get<T>(T defaultFallback) where T : IConfigurationKeys;
    }
}
