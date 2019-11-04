using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
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
