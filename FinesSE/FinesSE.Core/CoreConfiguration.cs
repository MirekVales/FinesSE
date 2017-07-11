using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Core
{
    public class CoreConfiguration : IConfigurationKeys
    {
        public WebDrivers DefaultBrowser { get; set; }

        public static CoreConfiguration Default
            => new CoreConfiguration()
            {
                DefaultBrowser = WebDrivers.IE
            };

    }
}
