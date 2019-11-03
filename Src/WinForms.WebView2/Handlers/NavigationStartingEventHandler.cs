using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class NavigationStartingEventHandler : HandlerBase<NavigationStartingEventArgs>,
        IWebView2NavigationStartingEventHandler
    {
        public NavigationStartingEventHandler(Action<NavigationStartingEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2NavigationStartingEventArgs args)
        {
            NavigationStartingEventArgs completedArgs = new NavigationStartingEventArgs(args);
            Callback(completedArgs);
        }
    }
}
