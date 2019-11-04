using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
