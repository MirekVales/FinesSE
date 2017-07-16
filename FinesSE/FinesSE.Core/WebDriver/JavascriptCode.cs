namespace FinesSE.Core.WebDriver
{
    public static class JavascriptCode
    {
        public static string ReturnPageWidth
            => "return Math.max(document.body.scrollWidth, document.body.offsetWidth, document.documentElement.clientWidth, document.documentElement.scrollWidth, document.documentElement.offsetWidth);";
        public static string ReturnPageHeight
            => "return Math.max(document.body.scrollHeight, document.body.offsetHeight, document.documentElement.clientHeight, document.documentElement.scrollHeight, document.documentElement.offsetHeight);";

        public static string ReturnViewWidth
            => "return Math.max(document.documentElement.clientWidth, window.innerWidth || 0);";
        public static string ReturnViewHeight
            => "return Math.max(document.documentElement.clientHeight, window.innerHeight || 0);";

        public static string ReturnPageOffsetX
            => "return (window.pageXOffset || document.documentElement.scrollLeft) - (document.documentElement.clientLeft || 0);";
        public static string ReturnPageOffsetY
            => "return (window.pageYOffset || document.documentElement.scrollTop)  - (document.documentElement.clientTop || 0);";

        public static string ScrollTo(int x, int y)
            => $"scrollTo({x},{y});";
    }
}
