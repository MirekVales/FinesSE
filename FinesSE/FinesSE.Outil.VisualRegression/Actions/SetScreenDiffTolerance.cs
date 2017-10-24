using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.VisualRegression;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class SetScreenDiffTolerance : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IConfigurationProvider ConfigurationProvider { get; set; }

        [EntryPoint]
        public void Invoke(string screenshotDiffTolerance)
            => ConfigurationProvider
            .Get(VisualRegressionConfiguration.Default)
            .ScreenshotDiffTolerance = screenshotDiffTolerance;
    }
}
