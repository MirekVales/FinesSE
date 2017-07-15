using System;
using System.Collections.Generic;
using FinesSE.Contracts.Invokable;
using FinesSE.Contracts.Infrastructure;
using System.Linq;
using FinesSE.Core;
using FinesSE.VisualRegression;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class TakeScreen : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }
        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }
        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements);

        public string Invoke(LocatedElements elements)
        {
            var configuration = ConfigurationProvider.Get(Configuration.Default);
            var versionId = configuration.ScreenshotStoreReferenceVersionId;

            foreach (var element in elements.Elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get(), ConfigurationProvider);
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), elements, element);
                ScreenshotStore.Store(screenshot, DriverProvider.TopicId, elementId, versionId);

                return ScreenshotStore.GetPath(DriverProvider.TopicId, elementId, versionId);
            }
            return "";
        }
    }
}
