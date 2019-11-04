using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class WebView2WebResourceResponse : IWebView2WebResourceResponse
    {
        internal IWebView2WebResourceResponse _response;

        internal WebView2WebResourceResponse(IWebView2WebResourceResponse response)
        {
            _response = response;
        }

        public IStream Content
        {
            get { return _response.Content; }
            set { _response.Content = value; }
        }

        public WebView2HttpResponseHeaderCollection Headers
        {
            get { return new WebView2HttpResponseHeaderCollection(_response.Headers); }
        }

        public int StatusCode
        {
            get { return _response.StatusCode; }
            set { _response.StatusCode = value; }
        }

        public string ReasonPhrase
        {
            get { return _response.ReasonPhrase; }
            set { _response.ReasonPhrase = value; }
        }

        IWebView2HttpResponseHeaders IWebView2WebResourceResponse.Headers => throw new NotImplementedException();
    }
}
