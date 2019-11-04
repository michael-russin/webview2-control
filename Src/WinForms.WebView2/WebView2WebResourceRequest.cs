using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class WebView2WebResourceRequest : IWebView2WebResourceRequest
    {
        private IWebView2WebResourceRequest _request;

        internal WebView2WebResourceRequest(IWebView2WebResourceRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// The request URI.
        /// </summary>
        public string Uri
        {
            get => _request.Uri;
            set => _request.Uri = value;
        }

        /// <summary>
        /// The HTTP request method.
        /// </summary>
        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }

        /// <summary>
        /// The HTTP request message body as stream. POST data would be here.
        /// If a stream is set, which will override the message body, the stream must
        /// have all the content data available by the time this
        /// response's WebResourceRequested event deferral is completed. Stream
        /// should be agile or be created from a background STA to prevent performance
        /// impact to the UI thread. Null means no content data. IStream semantics
        /// apply (return S_OK to Read calls until all data is exhausted)
        /// </summary>
        public IStream Content
        {
            get { return _request.Content; }
            set { _request.Content = value; }
        }

        /// <summary>
        /// The mutable HTTP request headers
        /// </summary>
        public WebView2HttpRequestHeaderCollection Headers
        {
            get { return new WebView2HttpRequestHeaderCollection(_request.Headers); }
        }

        public string uri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IWebView2HttpRequestHeaders IWebView2WebResourceRequest.Headers => throw new NotImplementedException();
    }
}
