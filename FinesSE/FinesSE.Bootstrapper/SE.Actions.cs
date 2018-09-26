using FinesSE.Outil.Actions;
using FinesSE.Outil.Soap.Actions;

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

        public string GetCssValue(string locator, string propertyName)
            => p.Invoke<GetCssValue>(locator, propertyName);

        public string GetCurrentUrl()
            => p.Invoke<GetCurrentUrl>();

        public string GetElementHeight(string locator)
            => p.Invoke<GetElementHeight>(locator);

        public string GetElementSource(string locator)
            => p.Invoke<GetElementSource>(locator);

        public string GetElementWidth(string locator)
            => p.Invoke<GetElementWidth>(locator);

        public string GetPageSource()
            => p.Invoke<GetPageSource>();

        public string GetTagName(string locator)
            => p.Invoke<GetTagName>(locator);

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

        public void SavePageSource(string filePath)
            => p.InvokeVoid<SavePageSource>(filePath);

        public void Select(string locator, string optionLocator)
            => p.InvokeVoid<Select>(locator, optionLocator);

        public void SetDefaultBrowserSize(string browserSizeName)
            => p.InvokeVoid<SetDefaultBrowserSize>(browserSizeName);

        public void SetImplicitWait(string msTimeout)
            => p.InvokeVoid<SetImplicitWait>(msTimeout);

        public void SetPageLoadTimeout(string pageLoadTimeout)
            => p.InvokeVoid<SetPageLoadTimeout>(pageLoadTimeout);

        public void SetZoom(string zoomLevel)
            => p.InvokeVoid<SetZoom>(zoomLevel);

        public void SetWindowSize(string width, string height)
            => p.InvokeVoid<SetWindowSize>(width, height);

        public string Soap_GetResponse(string responseId = null)
            => p.Invoke<Soap_GetResponse>(responseId);

        public string Soap_GetResponseDuration(string responseId = null)
            => p.Invoke<Soap_GetResponseDuration>(responseId);

        public string Soap_GetResponseSize(string responseId = null)
            => p.Invoke<Soap_GetResponseSize>(responseId);

        public string Soap_Send(string url, string envelopeId, string messageId)
            => p.Invoke<Soap_Send>(url, envelopeId, messageId);

        public void Soap_SetCredentials(string username, string passphrase, string domain = "")
            => p.InvokeVoid<Soap_SetCredentials>(username, passphrase, domain);

        public void Soap_SetEncoding(string encoding)
            => p.InvokeVoid<Soap_SetEncoding>(encoding);

        public void Soap_SetEnvelope(string envelopeBody, string envelopeId)
            => p.InvokeVoid<Soap_SetEnvelope>(envelopeBody, envelopeId);

        public void Soap_SetMessage(string messageBody, string messageId)
            => p.InvokeVoid<Soap_SetMessage>(messageBody, messageId);

        public void Submit(string locator)
            => p.InvokeVoid<Submit>(locator);

        public void Type(string locator, string value)
            => p.InvokeVoid<Type>(locator, value);

        public void TypeKeys(string locator, string keys)
            => p.InvokeVoid<TypeKeys>(locator, keys);

        public void TypeKeysLikeHuman(string locator, string keys)
            => p.InvokeVoid<TypeKeysLikeHuman>(locator, keys);

        public void WaitForCondition(string javascriptCondition, string msTimeout)
            => p.InvokeVoid<WaitForCondition>(javascriptCondition, msTimeout);

        public void WindowFocus()
            => p.InvokeVoid<WindowFocus>();

        public void WindowMaximize()
            => p.InvokeVoid<WindowMaximize>();
    }
}