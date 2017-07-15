using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.VisualRegression;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class GetScreenDiff : IAction
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
            var baseVersionId = configuration.ScreenshotStoreBaseVersionId;
            var referenceVersionId =configuration.ScreenshotStoreReferenceVersionId;

            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get(), ConfigurationProvider);
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, DriverProvider.TopicId, elementId, referenceVersionId);

                return ScreenshotStore.Compare(DriverProvider.TopicId, elementId, baseVersionId, referenceVersionId).ToString();
            }

            return null;
        }
    }
}
