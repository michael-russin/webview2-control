using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class PermissionRequestedEventHandler : HandlerBase<PermissionRequestedEventArgs>,
        IWebView2PermissionRequestedEventHandler
    {
        public PermissionRequestedEventHandler(Action<PermissionRequestedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2PermissionRequestedEventArgs args)
        {
            PermissionRequestedEventArgs eventArgs = new PermissionRequestedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
