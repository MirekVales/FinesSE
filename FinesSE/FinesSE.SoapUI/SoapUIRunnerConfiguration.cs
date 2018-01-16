using FinesSE.Contracts.Infrastructure;

namespace FinesSE.SoapUI
{
    public class SoapUIRunnerConfiguration : IConfigurationKeys
    {
        public string RunnerPath { get; set; }
        public string SettingsFilePath { get; set; }

        public static SoapUIRunnerConfiguration Default =>
            new SoapUIRunnerConfiguration()
            {
                RunnerPath = null,
                SettingsFilePath = null
            };
    }
}
