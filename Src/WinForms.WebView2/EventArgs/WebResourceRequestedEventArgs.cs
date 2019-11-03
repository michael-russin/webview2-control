using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class WebResourceRequestedEventArgs : EventArgs, IWebView2WebResourceRequestedEventArgs
    {
        private IWebView2WebResourceRequestedEventArgs _args;
        private WebResourceResponse _webResponse;

        internal WebResourceRequestedEventArgs(IWebView2WebResourceRequestedEventArgs args)
        {
            _args = args;
            _webResponse = new WebResourceResponse(_args.Response);
        }

        public WebResourceRequest Request
        {
            get { return new WebResourceRequest(_args.Request); }
        }

        public WebResourceResponse Response
        {
            get { return _webResponse; }
            set { _args.Response = _webResponse._response; }
        }

        public Deferral GetDeferral()
        {
            return new Deferral(_args.GetDeferral());
        }

        IWebView2Deferral IWebView2WebResourceRequestedEventArgs.GetDeferral()
        {
            throw new NotImplementedException();
        }

        IWebView2WebResourceRequest IWebView2WebResourceRequestedEventArgs.Request => throw new NotImplementedException();

        IWebView2WebResourceResponse IWebView2WebResourceRequestedEventArgs.Response { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
