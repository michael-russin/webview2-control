using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Events
{
    public class FocusChangedEventEventArgs : EventArgs
    {
        internal FocusChangedEventEventArgs(WebView2WebView webview, object args)
        {
            WebView2WebView = webview;
            Args = args;
        }

        public WebView2WebView WebView2WebView { get; private set; }

        public object Args { get; private set; }
    }
}
