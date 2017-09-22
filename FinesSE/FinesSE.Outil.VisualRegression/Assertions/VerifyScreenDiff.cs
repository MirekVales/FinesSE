﻿using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;
using FinesSE.VisualRegression.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Assertions
{
    public class VerifyScreenDiff : IAction
    {
        public IExecutionContext Context { get; set; }

        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(
                parameters.First() as LocatedElements,
                parameters.ElementAt(1) as string);

        public string Invoke(LocatedElements elements, string toleranceValue)
        {
            elements.ConstraintCount(c => c > 0);

            var configuration = Context.ConfigurationProvider.Get(VisualRegressionConfiguration.Default);
            var baseVersionId = configuration.ScreenshotStoreBaseVersionId;
            var referenceVersionId = configuration.ScreenshotStoreReferenceVersionId;
            var tolerance = ParseToleranceLevel(toleranceValue.FallbackEmptyString(() => configuration.ScreenshotDiffTolerance));

            foreach (var element in elements.Elements)
            {
                var screenshot = element.TakeScreenshot(Context.Driver, Context.ConfigurationProvider);
                var elementId = IdentityProvider.GetIdentifier(Context.Driver, elements, element);
                ScreenshotStore.Store(screenshot, Context.TopicId, elementId, referenceVersionId);

                var diff = ScreenshotStore.Compare(Context.TopicId, elementId, baseVersionId, referenceVersionId);
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
