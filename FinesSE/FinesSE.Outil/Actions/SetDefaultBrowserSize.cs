using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class SetDefaultBrowserSize : IVoidAction
    {
        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First());

        public void Invoke(string identifier)
        {
            var configuration = ConfigurationProvider.Get(CoreConfiguration.Default);

            var browserSize = configuration
                .BrowserSizes
                .First(size => string.Equals(
                                size.Name,
                                identifier,
                                StringComparison.InvariantCultureIgnoreCase));

            configuration.DefaultBrowserSize = browserSize;

            Context
            .Driver
            .Manage()
            .Window
            .Size = browserSize.AsDrawingSize();
        }
    }
}
