using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    internal class WebView2DocumentStateChangedEventHandler : HandlerBase<DocumentStateChangedEventArgs>, 
        IWebView2DocumentStateChangedEventHandler
    {
        public WebView2DocumentStateChangedEventHandler(Action<DocumentStateChangedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2DocumentStateChangedEventArgs args)
        {
            DocumentStateChangedEventArgs eventArgs = new DocumentStateChangedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
