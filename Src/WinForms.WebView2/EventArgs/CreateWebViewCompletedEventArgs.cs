using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class CreateWebViewCompletedEventArgs : EventArgs
    {
        internal CreateWebViewCompletedEventArgs(int result, IWebView2WebView webView)
        {
            Result = result;
            WebView = new WebView2WebView(webView);
        }

        public int Result { get; private set; }

        public WebView2WebView WebView { get; private set; }
    }
}
