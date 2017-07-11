using System;
using System.Collections.Generic;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using FinesSE.Contracts.Infrastructure;
using System.Linq;
using FinesSE.Core;
using FinesSE.VisualRegression;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class TakeBaseScreen : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }
        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }
        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>);

        public string Invoke(IEnumerable<IWebElement> elements)
        {
            var configuration = ConfigurationProvider.Get(Configuration.Default);
            var versionId = configuration.ScreenshotStoreBaseVersionId;

            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, elementId, versionId);

                return ScreenshotStore.GetPath(elementId, versionId);
            }
            return "";
        }
    }
}
