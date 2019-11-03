using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
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
