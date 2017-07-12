using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.VisualRegression;
using FinesSE.VisualRegression.Contracts;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Assertions
{
    public class VerifyScreenDiff : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }
        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }
        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(
                parameters.First() as IEnumerable<IWebElement>, 
                parameters.ElementAt(1) as string);

        public string Invoke(IEnumerable<IWebElement> elements, string toleranceValue)
        {
            var configuration = ConfigurationProvider.Get(Configuration.Default);
            var baseVersionId = configuration.ScreenshotStoreBaseVersionId;
            var referenceVersionId = configuration.ScreenshotStoreReferenceVersionId;
            var tolerance = ParseToleranceLevel(toleranceValue.FallbackEmptyString(() => configuration.ScreenshotDiffTolerance));
            
            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, DriverProvider.TopicId, elementId, referenceVersionId);

                var diff = ScreenshotStore.Compare(DriverProvider.TopicId, elementId, baseVersionId, referenceVersionId);
                if (diff > tolerance)
                    throw new ComparisonAssertionException(elementId, baseVersionId, referenceVersionId, diff * 100, tolerance * 100);
            }

            return "true";
        }

        private double ParseToleranceLevel(string v)
        {
            if (v.EndsWith("%"))
                return double.Parse(v.TrimEnd('%')) / 100;

            return double.Parse(v);
        }
    }
}
