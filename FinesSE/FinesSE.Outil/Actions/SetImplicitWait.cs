using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using System;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Actions
{
    public class SetImplicitWait : IVoidAction
    {
        public IExecutionContext Context { get; set; }
        public IConfigurationProvider ConfigurationProvider { get; set; }

        [EntryPoint]
        public void Invoke(string msTimeout)
        {
            var timeoutString = Regex.Match(msTimeout, @"\d+").Value;
            var timeout = TimeSpan.FromMilliseconds(int.Parse(timeoutString));

            var configuration = ConfigurationProvider.Get(CoreConfiguration.Default);

            Context
            .Driver
            .Manage()
            .Timeouts()
            .ImplicitWait
            = configuration.ImplicitWait = timeout;
        }
    }
}