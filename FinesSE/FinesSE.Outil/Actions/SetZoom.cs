using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Actions
{
    public class SetZoom : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string zoomLevelText)
        {
            var zoomLevel = Regex.Match(zoomLevelText, "\\d{1,3}");
            Context.Driver.SetZoomLevel(int.Parse(zoomLevel.Value));
        }
    }
}
