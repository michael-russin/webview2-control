using System;
using System.Runtime.InteropServices;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class WebMessageReceivedEventArgs : EventArgs, IWebView2WebMessageReceivedEventArgs
    {
        private IWebView2WebMessageReceivedEventArgs _args;

        internal WebMessageReceivedEventArgs(IWebView2WebMessageReceivedEventArgs args)
        {
            _args = args;
        }

        /// <summary>
        /// The URI of the document that sent this web message.
        /// </summary>
        public string Source
        {
            get
            {
                return _args.Source;
            }
        }

        /// <summary>
        /// The message posted from the webview content to the host converted to a
        /// JSON string. Use this to communicate via JavaScript objects.
        ///
        /// For example the following postMessage calls result in the
        /// following WebMessageAsJson values:
        ///
        /// ```
        ///    postMessage({'a': 'b'})      L"{\"a\": \"b\"}"
        ///    postMessage(1.2)             L"1.2"
        ///    postMessage('example')       L"\"example\""
        /// ```
        /// </summary>
        public string WebMessageAsJson
        {
            get
            {
                return _args.WebMessageAsJson;
            }
        }

        /// <summary>
        /// If the message posted from the webview content to the host is a
        /// string type, this method will return the value of that string. If the
        /// message posted is some other kind of JavaScript type this method will fail
        /// with E_INVALIDARG. Use this to communicate via simple strings.
        ///
        /// For example the following postMessage calls result in the
        /// following WebMessageAsString values:
        ///
        /// ```
        ///    postMessage({'a': 'b'})      E_INVALIDARG
        ///    postMessage(1.2)             E_INVALIDARG
        ///    postMessage('example')       L"example"
        /// ```
        /// </summary>
        public string WebMessageAsString
        {
            get
            {
                try
                {
                    return _args.WebMessageAsString;
                }
                catch(Exception)
                {
                }
                return null;
            }
            
        }
    }
}
