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
using MtrDev.WinForms.Handlers;

namespace MtrDev.WinForms
{
    public class WebView2Environment 
    {
        private IWebView2Environment2 _environment;

        internal WebView2Environment(IWebView2Environment environment)
        {
            _environment = (IWebView2Environment2)environment;
        }

        public void CreateWebResourceResponse(IStream Content, int StatusCode, string ReasonPhrase, string Headers, ref IWebView2WebResourceResponse Response)
        {
            throw new NotImplementedException();
        }

        public void CreateWebView(IntPtr parentHwnd, Action<CreateWebViewCompletedEventArgs> handler)
        {
            CreateWebViewCompletedHandler callback = new CreateWebViewCompletedHandler(handler);

            //            _RemotableHandle rh = new _RemotableHandle();
            //            rh.fContext = 0x50746457;
            //            rh.u.hInproc = parentHwnd.ToInt32();

            //            _environment.CreateWebView(parentHwnd, callback);
            //uint hwnd = (uint)parentHwnd.ToInt32();
            _environment.CreateWebView(parentHwnd, callback);
        }

        #region IWebView2Environment2

        /// <summary>
        /// The browser version info of the current IWebView2Environment,
        /// including channel name if it is not the stable channel.
        /// This matches the format of the GetWebView2BrowserVersionInfo API.
        /// Channel names are 'beta', 'dev', and 'canary'.
        /// </summary>
        public string BrowserVersionInfo
        {
            get
            {
                return _environment.BrowserVersionInfo;
            }
        }
        #endregion

        #region IWebView2Environment3
        // HRESULT add_NewVersionAvailable([in] IWebView2NewVersionAvailableEventHandler* eventHandler, [out] EventRegistrationToken* token);

        /// Remove an event handler previously added with add_NewVersionAvailable.
        // HRESULT remove_NewVersionAvailable( [in] EventRegistrationToken token);
        #endregion
    }
}
