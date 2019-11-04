using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class ProcessFailedEventArgs : EventArgs, IWebView2ProcessFailedEventArgs
    {
        private IWebView2ProcessFailedEventArgs _args;

        internal ProcessFailedEventArgs(IWebView2ProcessFailedEventArgs args)
        {
            _args = args;
        }

        public WEBVIEW2_PROCESS_FAILED_KIND ProcessFailedKind
        {
            get { return _args.ProcessFailedKind;  }
        }
    }
}
