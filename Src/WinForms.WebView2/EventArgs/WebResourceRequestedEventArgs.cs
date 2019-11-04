using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class WebResourceRequestedEventArgs : EventArgs, IWebView2WebResourceRequestedEventArgs
    {
        private IWebView2WebResourceRequestedEventArgs _args;
        private WebView2WebResourceResponse _webResponse;

        internal WebResourceRequestedEventArgs(IWebView2WebResourceRequestedEventArgs args)
        {
            _args = args;
            _webResponse = new WebView2WebResourceResponse(_args.Response);
        }

        public WebView2WebResourceRequest Request
        {
            get { return new WebView2WebResourceRequest(_args.Request); }
        }

        public WebView2WebResourceResponse Response
        {
            get { return _webResponse; }
            set { _args.Response = _webResponse._response; }
        }

        public WebView2Deferral GetDeferral()
        {
            return new WebView2Deferral(_args.GetDeferral());
        }

        IWebView2Deferral IWebView2WebResourceRequestedEventArgs.GetDeferral()
        {
            throw new NotImplementedException();
        }

        IWebView2WebResourceRequest IWebView2WebResourceRequestedEventArgs.Request => throw new NotImplementedException();

        IWebView2WebResourceResponse IWebView2WebResourceRequestedEventArgs.Response { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
