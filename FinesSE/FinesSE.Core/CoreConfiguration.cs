using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Core
{
    public class CoreConfiguration : IConfigurationKeys
    {
        public WebDrivers DefaultBrowser { get; set; }
        public TimeSpan WaitForDocumentCompleteState { get; set; }

        public static CoreConfiguration Default
            => new CoreConfiguration()
            {
                DefaultBrowser = WebDrivers.IE,
                WaitForDocumentCompleteState = TimeSpan.FromSeconds(1)
            };

    }
}
