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
