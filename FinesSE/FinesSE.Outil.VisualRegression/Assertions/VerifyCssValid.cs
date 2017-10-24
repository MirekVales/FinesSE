using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression.Contracts;
using FinesSE.VisualRegression.Infrastructure;
using log4net;
using System;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Assertions
{
    public class VerifyCssValid : IVoidAction
    {
        public IExecutionContext Context { get; set; }
        public ICssValidator Validator { get; set; }
        public ILog Log { get; set; }

        [EntryPoint]
        public void Invoke()
        {
            foreach (var url in GetCssUrls())
            {
                var resource = Context.Driver.DownloadResource(url);
                Log.DebugFormat(
                    "Resource (length {0}) retrieved from {1}",
                    resource.Length,
                    url);

                var incidents = Validator.Validate(resource).ToArray();
                if (incidents.Any())
                    using (var e = new CssValidationException(incidents))
                        Log.Fatal("CSS validation incidents found", e);
            }
        }

        private string[] GetCssUrls()
        {
            var currentHost = new Uri(Context.Driver.Url).Host.ToLower();
            var urls = Context
                .Driver
                .GetLinkedCssUrls()
                .Where(x => x.ToLower().Contains(currentHost))
                .ToArray();
            Log.DebugFormat("{0} referenced stylesheets detected", urls.Length);
            return urls;
        }
    }
}
