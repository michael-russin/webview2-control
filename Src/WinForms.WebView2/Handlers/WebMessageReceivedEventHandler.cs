using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class WebView2WebMessageReceivedEventHandler : IWebView2WebMessageReceivedEventHandler
    {
        Action<WebMessageReceivedEventArgs> _callback;

        public WebView2WebMessageReceivedEventHandler(Action<WebMessageReceivedEventArgs> callback)
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
