using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    /// <summary>
    /// Fires when an HTTP request is made in the webview. The host can override
    /// request, response headers and response content.
    /// </summary>
    internal class WebResourceRequestedEventHandler : HandlerBase<WebResourceRequestedEventArgs>, IWebView2WebResourceRequestedEventHandler
    {
        public WebResourceRequestedEventHandler(Action<WebResourceRequestedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2WebResourceRequestedEventArgs args)
        {
            WebResourceRequestedEventArgs eventArgs = new WebResourceRequestedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
