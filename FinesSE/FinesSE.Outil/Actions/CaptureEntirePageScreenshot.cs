using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class CaptureEntirePageScreenshot : IVoidAction
    {
        public IExecutionContext Context { get; set; }
        public IConfigurationProvider Configuration { get; set; }

        [EntryPoint]
        public void Invoke(string path)
        {
            using (var screenshotTaker = new ScreenshotTaker(Context.Driver, Configuration.Get(ScreenshotTakerConfiguration.Default)))
                screenshotTaker.SaveScreenshot(path);
        }
    }
}
