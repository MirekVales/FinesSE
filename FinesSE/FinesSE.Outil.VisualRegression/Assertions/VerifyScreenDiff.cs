using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;
using FinesSE.VisualRegression.Contracts;
using System.Collections.Generic;

namespace FinesSE.Outil.VisualRegression.Assertions
{
    public class VerifyScreenDiff : IStringAction, IReportable
    {
        public IExecutionContext Context { get; set; }

        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }
        public IInvoker Invoker { get; set; }

        public string Name { get; } = "Verify Screen Difference";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
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

                Invoker.AddImage(ScreenshotStore.GetPath(Context.TopicId, elementId, configuration.ScreenshotStoreBaseVersionId));
                Invoker.AddImage(ScreenshotStore.GetPath(Context.TopicId, elementId, configuration.ScreenshotStoreReferenceVersionId));
                Invoker.AddImage(ScreenshotStore.GetPath(Context.TopicId, elementId, configuration.ScreenshotStoreDiffVersionId));

                var diff = ScreenshotStore.Compare(Context.TopicId, elementId, baseVersionId, referenceVersionId);
                Invoker.AddInfo($"Total difference {diff}");
                if (diff > tolerance)
                    throw new ComparisonAssertionException(elementId, baseVersionId, referenceVersionId, diff * 100, tolerance * 100);
            }

            return "true";
        }

        private double ParseToleranceLevel(string v)
        {
            if (v.EndsWith("%"))
                return double.Parse(v.TrimEnd('%', ' ')) / 100;

            return double.Parse(v);
        }
    }
}