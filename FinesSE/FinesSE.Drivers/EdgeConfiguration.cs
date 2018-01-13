using FinesSE.Contracts;
using OpenQA.Selenium;

namespace FinesSE.Drivers
{
    public class EdgeConfiguration : WebDriverConfiguration
    {
        public PageLoadStrategy PageLoadStrategy { get; set; }
    }
}
