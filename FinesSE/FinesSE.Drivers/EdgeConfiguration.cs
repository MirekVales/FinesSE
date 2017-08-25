using FinesSE.Contracts;
using OpenQA.Selenium.Edge;

namespace FinesSE.Drivers
{
    public class EdgeConfiguration : WebDriverConfiguration
    {
        public EdgePageLoadStrategy PageLoadStrategy { get; set; }
    }
}
