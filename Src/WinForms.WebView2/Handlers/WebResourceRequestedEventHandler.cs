using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
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
