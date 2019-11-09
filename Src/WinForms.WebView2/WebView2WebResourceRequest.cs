#region License
// Copyright (c) 2019 Michael T. Russin
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using MtrDev.WebView2.Interop;

namespace MtrDev.WinForms
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
