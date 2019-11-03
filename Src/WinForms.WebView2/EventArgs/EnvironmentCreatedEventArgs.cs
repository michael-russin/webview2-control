using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp
{
    public class EnvironmentCreatedEventArgs : EventArgs
    {
        internal EnvironmentCreatedEventArgs(int result, IWebView2Environment webViewEnvironment)
        {
            Result = result;
            WebViewEnvironment = new WebViewEnvironment(webViewEnvironment);
        }

        public int Result
        {
            get; private set;
        }

        public WebViewEnvironment WebViewEnvironment
        {
            get; private set;
        }
    }
}
