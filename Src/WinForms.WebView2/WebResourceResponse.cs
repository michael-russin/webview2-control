using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp
{
    public class WebResourceResponse : IWebView2WebResourceResponse
    {
        internal IWebView2WebResourceResponse _response;

        internal WebResourceResponse(IWebView2WebResourceResponse response)
        {
            _response = response;
        }

        public IStream Content
        {
            get { return _response.Content; }
            set { _response.Content = value; }
        }

        public HttpResponseHeaderCollection Headers
        {
            get { return new HttpResponseHeaderCollection(_response.Headers); }
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
