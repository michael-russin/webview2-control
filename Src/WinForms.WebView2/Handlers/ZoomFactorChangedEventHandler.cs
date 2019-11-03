using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class WebView2ZoomFactorChangedEventHandler : IWebView2ZoomFactorChangedEventHandler
    {
        Action<ZoomFactorCompletedEventArgs> _callback;

        public WebView2ZoomFactorChangedEventHandler(Action<ZoomFactorCompletedEventArgs> callback)
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
