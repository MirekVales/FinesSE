using FinesSE.Outil.Actions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void Click(string locator)
            => se.InvokeVoid<Click>(locator);

        public void ClickAt(string locator, string coordinates)
            => se.InvokeVoid<ClickAt>(locator, coordinates);

        public void DoubleClick(string locator)
            => se.InvokeVoid<DoubleClick>(locator);

        public void Close()
            => kernel.Context.Dispose();

        public string Execute(string javascript)
            => se.Invoke<Execute>(javascript);

        public void Focus(string locator)
            => se.InvokeVoid<Focus>(locator);

        public string GetText(string locator)
            => se.Invoke<GetText>(locator);

        public void Highlight(string locator)
            => se.InvokeVoid<Highlight>(locator);

        public void MouseDown(string locator)
            => se.InvokeVoid<MouseDown>(locator);

        public void MouseDownAt(string locator, string coordinates)
            => se.InvokeVoid<MouseDownAt>(locator, coordinates);

        public void MouseUp(string locator)
            => se.InvokeVoid<MouseUp>(locator);

        public void MouseUpAt(string locator, string coordinates)
            => se.InvokeVoid<MouseUpAt>(locator, coordinates);

        public void Open(string url)
            => se.InvokeVoid<Open>(url);

        public void Pause(string ms)
            => se.InvokeVoid<Pause>(ms);

        public void Refresh()
            => se.InvokeVoid<Refresh>();

        public void Type(string locator, string keys)
            => se.InvokeVoid<Type>(locator, keys);

        public void WaitForCondition(string javascriptCondition, string msTimeout)
            => se.InvokeVoid<WaitForCondition>(javascriptCondition, msTimeout);

        public void WindowFocus()
            => se.InvokeVoid<WindowFocus>();

        public void WindowMaximize()
            => se.InvokeVoid<WindowMaximize>();
    }
}
