using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class NavigationCompletedEventArgs : EventArgs, IWebView2NavigationCompletedEventArgs
    {
        private IWebView2NavigationCompletedEventArgs _args;

        public NavigationCompletedEventArgs(IWebView2NavigationCompletedEventArgs args)
        {
            _args = args;
        }

        public bool IsSuccess
        {
            get => _args.IsSuccess;
        }

        public WEBVIEW2_WEB_ERROR_STATUS WebErrorStatus
        {
            get => _args.WebErrorStatus;
        }
    }
}
