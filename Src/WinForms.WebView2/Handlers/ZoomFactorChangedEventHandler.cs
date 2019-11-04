using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    public class ZoomFactorChangedEventHandler : IWebView2ZoomFactorChangedEventHandler
    {
        Action<ZoomFactorCompletedEventArgs> _callback;

        public ZoomFactorChangedEventHandler(Action<ZoomFactorCompletedEventArgs> callback)
        {
            _callback = callback;
        }


        public void Invoke(IWebView2WebView webview, object args)
        {
            ZoomFactorCompletedEventArgs eventArgs = new ZoomFactorCompletedEventArgs(args);
            _callback.Invoke(eventArgs);
        }
    }
}
