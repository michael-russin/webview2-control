using System;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class DocumentStateChangedEventArgs : EventArgs
    {
        private IWebView2DocumentStateChangedEventArgs _args;
        internal DocumentStateChangedEventArgs(IWebView2DocumentStateChangedEventArgs args)
        {
            _args = args;
        }

        public bool IsNewDocument
        {
            get { return Convert.ToBoolean(_args.IsNewDocument); }
        }

        public bool IsErrorPage
        {
            get { return Convert.ToBoolean(_args.IsErrorPage); }
        }
    }
}
