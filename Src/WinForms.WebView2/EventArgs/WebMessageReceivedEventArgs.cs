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
