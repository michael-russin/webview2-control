using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
