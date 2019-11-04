using System;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms.Handlers;

namespace Russinsoft.WinForms.Handlers
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
