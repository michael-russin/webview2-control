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
    public class NavigationStartingEventArgs : EventArgs, IWebView2NavigationStartingEventArgs
    {
        private IWebView2NavigationStartingEventArgs _args;

        internal NavigationStartingEventArgs(IWebView2NavigationStartingEventArgs args)
        {
            _args = args;
        }

        /// <summary>
        /// The uri of the requested navigation.
        /// </summary>
        public string Uri
        {
            get => _args.Uri;
        }

        /// <summary>
        /// True when the navigation was initiated through a user gesture as opposed
        /// to programmatic navigation.
        /// </summary>
        public bool IsUserInitiated
        {
            get => _args.IsUserInitiated;
        }

        /// <summary>
        /// True when the navigation is redirected.
        /// </summary>
        /// <returns></returns>
        public bool IsRedirected
        {
            get => _args.IsRedirected;
        }

        private WebView2HttpRequestHeaderCollection _httpHeaderCollection;
        public WebView2HttpRequestHeaderCollection HttpHeaderCollection
        {
            get
            {
                if (_httpHeaderCollection == null)
                {
                    _httpHeaderCollection = new WebView2HttpRequestHeaderCollection(_args.RequestHeaders);
                }
                return _httpHeaderCollection;
            }
        }

        /// <summary>
        /// The host may set this flag to cancel the navigation.
        /// If set, it will be as if the navigation never happened and the current
        /// page's content will be intact. For performance reasons, GET HTTP requests
        /// may happen, while the host is responding. This means cookies can be set
        /// and used part of a request for the navigation.
        /// </summary>
        /// <returns></returns>
        public bool Cancel
        {
            get => _args.Cancel;
            set => _args.Cancel = value;
        }

        IWebView2HttpRequestHeaders IWebView2NavigationStartingEventArgs.RequestHeaders
        {
            get => throw new NotImplementedException();
        }
    }
}
