using FinesSE.Outil.Actions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void Back()
            => p.InvokeVoid<Back>();

        public void Clear(string locator)
            => p.InvokeVoid<Clear>(locator);

        public void Click(string locator)
            => p.InvokeVoid<Click>(locator);

        public void ClickAt(string locator, string coordinates)
            => p.InvokeVoid<ClickAt>(locator, coordinates);

        public void Deselect(string locator, string optionLocator)
            => p.InvokeVoid<Deselect>(locator, optionLocator);

        public void DoubleClick(string locator)
            => p.InvokeVoid<DoubleClick>(locator);

        public void CaptureEntirePageScreenshot(string path)
            => p.InvokeVoid<CaptureEntirePageScreenshot>(path);

        public string Count(string locator)
            => p.Invoke<Count>(locator);

        public void Close()
            => kernel.Context.Dispose();

        public void DeleteAllCookies()
            => p.InvokeVoid<DeleteAllCookies>();

        public void DeleteCookieNamed(string name)
            => p.InvokeVoid<DeleteCookieNamed>(name);

        public string Execute(string javascript)
            => p.Invoke<Execute>(javascript);

        public void Focus(string locator)
            => p.InvokeVoid<Focus>(locator);

        public void Forward()
            => p.InvokeVoid<Forward>();

        public string GetAttribute(string locator, string attributeName)
            => p.Invoke<GetAttribute>(locator, attributeName);

        public string GetCookies()
            => p.Invoke<GetCookies>();

        public string GetCookieNamed(string name)
            => p.Invoke<GetCookieNamed>(name);

        public string GetCurrentUrl()
            => p.Invoke<GetCurrentUrl>();

        public string GetPageSource()
            => p.Invoke<GetPageSource>();

        public string GetText(string locator)
            => p.Invoke<GetText>(locator);

        public string GetTitle()
            => p.Invoke<GetTitle>();

        public void Highlight(string locator)
            => p.InvokeVoid<Highlight>(locator);

        public string IsDisplayed(string locator)
            => p.Invoke<IsDisplayed>(locator);

        public void MouseDown(string locator)
            => p.InvokeVoid<MouseDown>(locator);

        public void MouseDownAt(string locator, string coordinates)
            => p.InvokeVoid<MouseDownAt>(locator, coordinates);

        public void MouseUp(string locator)
            => p.InvokeVoid<MouseUp>(locator);

        public void MouseUpAt(string locator, string coordinates)
            => p.InvokeVoid<MouseUpAt>(locator, coordinates);

        public void Open(string url)
            => p.InvokeVoid<Open>(url);

        public void Pause(string ms)
            => p.InvokeVoid<Pause>(ms);

        public void Refresh()
            => p.InvokeVoid<Refresh>();

        public void Select(string locator, string optionLocator)
            => p.InvokeVoid<Select>(locator, optionLocator);

        public void SetDefaultBrowserSize(string browserSizeName)
            => p.InvokeVoid<SetDefaultBrowserSize>(browserSizeName);

        public void SetPageLoadTimeout(string pageLoadTimeout)
            => p.InvokeVoid<SetPageLoadTimeout>(pageLoadTimeout);

        public void SetZoom(string zoomLevel)
            => p.InvokeVoid<SetZoom>(zoomLevel);

        public void SetWindowSize(string width, string height)
            => p.InvokeVoid<SetWindowSize>(width, height);

        public void Submit(string locator)
            => p.InvokeVoid<Submit>(locator);

        public void Type(string locator, string value)
            => p.InvokeVoid<Type>(locator, value);

        public void TypeKeys(string locator, string keys)
            => p.InvokeVoid<TypeKeys>(locator, keys);

        public void WaitForCondition(string javascriptCondition, string msTimeout)
            => p.InvokeVoid<WaitForCondition>(javascriptCondition, msTimeout);

        public void WindowFocus()
            => p.InvokeVoid<WindowFocus>();

        public void WindowMaximize()
            => p.InvokeVoid<WindowMaximize>();
    }
}