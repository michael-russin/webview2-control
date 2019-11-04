using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class EnvironmentCreatedEventArgs : EventArgs
    {
        internal EnvironmentCreatedEventArgs(int result, IWebView2Environment webViewEnvironment)
        {
            Result = result;
            WebViewEnvironment = new WebView2Environment(webViewEnvironment);
        }

        public int Result
        {
            get; private set;
        }

        public WebView2Environment WebViewEnvironment
        {
            get; private set;
        }
    }
}
