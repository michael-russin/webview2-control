using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    public class WebMessageReceivedEventHandler : IWebView2WebMessageReceivedEventHandler
    {
        Action<WebMessageReceivedEventArgs> _callback;

        public WebMessageReceivedEventHandler(Action<WebMessageReceivedEventArgs> callback)
        {
            _callback = callback;
        }

        public void Invoke(IWebView2WebView webview, IWebView2WebMessageReceivedEventArgs args)
        {
            WebMessageReceivedEventArgs eventArgs = new WebMessageReceivedEventArgs(args);
            _callback.Invoke(eventArgs);
        }
    }
}
