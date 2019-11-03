using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    internal class WebView2NavigationCompletedEventHandler : IWebView2NavigationCompletedEventHandler
    {
        Action<NavigationCompletedEventArgs> _callback;

        public WebView2NavigationCompletedEventHandler(Action<NavigationCompletedEventArgs> callback)
        {
            _callback = callback;
        }

        public void Invoke(IWebView2WebView webview, IWebView2NavigationCompletedEventArgs args)
        {
            NavigationCompletedEventArgs completedArgs = new NavigationCompletedEventArgs(args);
            _callback(completedArgs);
        }
    }
}
