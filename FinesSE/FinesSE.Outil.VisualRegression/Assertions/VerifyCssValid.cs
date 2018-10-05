using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression.Contracts;
using FinesSE.VisualRegression.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Assertions
{
    public class VerifyCssValid : IStringAction, IReportable
    {
        public IExecutionContext Context { get; set; }
        public ICssValidator Validator { get; set; }
        public ILog Log { get; set; }
        public IInvoker Invoker { get; set; }

        public string Name { get; } = "Verify Css Validity";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke()
        {
            foreach (var url in GetCssUrls())
            {
                Invoker.AddInfo($"Validating {url}");

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

            return "true";
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
