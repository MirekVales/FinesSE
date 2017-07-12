﻿using FinesSE.Contracts.Infrastructure;
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
            yield return typeof(string);
            yield return typeof(double);
        }

        public string Invoke(params object[] parameters)
            => Invoke(
                parameters.First() as IEnumerable<IWebElement>, 
                parameters.ElementAt(1) as string, 
                parameters.ElementAt(2) as string, 
                (double)parameters.ElementAt(3));

        public string Invoke(IEnumerable<IWebElement> elements, string baseVersionId, string referenceVersionId, double tolerance)
        {
            var configuration = ConfigurationProvider.Get(Configuration.Default);
            baseVersionId = baseVersionId.FallbackEmptyString(() => configuration.ScreenshotStoreBaseVersionId);
            referenceVersionId = referenceVersionId.FallbackEmptyString(() => configuration.ScreenshotStoreReferenceVersionId);

            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, DriverProvider.TopicId, elementId, referenceVersionId);

                var diff = ScreenshotStore.Compare(DriverProvider.TopicId, elementId, baseVersionId, referenceVersionId);
                if (diff > tolerance)
                    throw new ComparisonAssertionException(elementId, baseVersionId, referenceVersionId, diff, tolerance);
            }

            return "true";
        }
    }
}
