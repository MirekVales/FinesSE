using FinesSE.Outil.Actions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void Click(string locator)
            => p.InvokeVoid<Click>(locator);

        public void ClickAt(string locator, string coordinates)
            => p.InvokeVoid<ClickAt>(locator, coordinates);

        public void Deselect(string locator, string optionLocator)
            => p.InvokeVoid<Deselect>(locator, optionLocator);

        public void DoubleClick(string locator)
            => p.InvokeVoid<DoubleClick>(locator);

        public void Close()
            => kernel.Context.Dispose();

        public string Execute(string javascript)
            => p.Invoke<Execute>(javascript);

        public void Focus(string locator)
            => p.InvokeVoid<Focus>(locator);

        public string GetCookies()
            => p.Invoke<GetCookies>();

        public string GetCookieNamed(string name)
            => p.Invoke<GetCookieNamed>(name);

        public string GetText(string locator)
            => p.Invoke<GetText>(locator);

        public void Highlight(string locator)
            => p.InvokeVoid<Highlight>(locator);

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

        public void SetZoom(string zoomLevel)
            => p.InvokeVoid<SetZoom>(zoomLevel);

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
