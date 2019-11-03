using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class DocumentTitleChangedEventArgs : EventArgs
    {
        internal DocumentTitleChangedEventArgs(IWebView2WebView3 webview)
        {
            string title;
            webview.DocumentTitle(out title);
            Title = title;
        }

        public string Title { get; private set; }
    }
}
