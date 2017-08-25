using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Contracts
{
    public class WebDriverConfiguration : IConfigurationKeys
    {
        public string WebDriverDirectory { get; set; }
        public TimeSpan CommandTimeout { get; set; }
        public Dictionary<string, string> AdditionalCapability { get; set; }

        public void SetAdditionalCapability(DriverOptions options)
        {
            if (AdditionalCapability == null)
                return;

            foreach (var additionalCapability in AdditionalCapability)
                options.AddAdditionalCapability(
                    additionalCapability.Key,
                    additionalCapability.Value);
        }
    }
}
