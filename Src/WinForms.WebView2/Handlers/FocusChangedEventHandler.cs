using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Handlers;

namespace WebView2Sharp.Events
{
    internal class WebView2FocusChangedEventHandler : HandlerBase<FocusChangedEventEventArgs>,
        IWebView2FocusChangedEventHandler
    {
        internal WebView2FocusChangedEventHandler(Action<FocusChangedEventEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, object args)
        {
            //            FocusChangedEventEventArgs eventArgs = new FocusChangedEventEventArgs(new WebView2WebView(webview), args);
            FocusChangedEventEventArgs eventArgs = new FocusChangedEventEventArgs(null, args);
            Callback.Invoke(eventArgs);
        }
    }
}
