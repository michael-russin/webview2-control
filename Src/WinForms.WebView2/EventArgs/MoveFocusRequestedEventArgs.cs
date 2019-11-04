using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class MoveFocusRequestedEventArgs: EventArgs, IWebView2MoveFocusRequestedEventArgs
    {
        private IWebView2MoveFocusRequestedEventArgs _args;

        public MoveFocusRequestedEventArgs(IWebView2MoveFocusRequestedEventArgs args)
        {
            _args = args;
        }

        public WEBVIEW2_MOVE_FOCUS_REASON Reason
        {
            get => _args.Reason;
        }

        public bool Handled
        {
            get => _args.Handled;

            set => _args.Handled = value;
        }
    }
}
