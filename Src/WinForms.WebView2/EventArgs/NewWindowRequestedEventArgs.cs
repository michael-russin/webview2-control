using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class NewWindowRequestedEventArgs : EventArgs, IWebView2NewWindowRequestedEventArgs
    {
        private IWebView2NewWindowRequestedEventArgs _args;

        internal NewWindowRequestedEventArgs(IWebView2NewWindowRequestedEventArgs args)
        {
            _args = args;
        }

        public bool Handled { get => _args.Handled; set => _args.Handled = value; }

        public bool IsUserInitiated => _args.IsUserInitiated;

        public IWebView2WebView NewWindow { get => _args.NewWindow; set => _args.NewWindow = value; }

        public string Uri => _args.Uri;

        public WebView2Deferral GetDeferal()
        {
            return new WebView2Deferral(_args.GetDeferral());
        }

        IWebView2Deferral IWebView2NewWindowRequestedEventArgs.GetDeferral()
        {
            return _args.GetDeferral();
        }
    }
}
