using System;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
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

        private HttpRequestHeaderCollection _httpHeaderCollection;
        public HttpRequestHeaderCollection HttpHeaderCollection
        {
            get
            {
                if (_httpHeaderCollection == null)
                {
                    _httpHeaderCollection = new HttpRequestHeaderCollection(_args.RequestHeaders);
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
