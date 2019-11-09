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
    public class NewWindowRequestedEventArgs : EventArgs, IWebView2NewWindowRequestedEventArgs
    {
        private IWebView2NewWindowRequestedEventArgs _args;

        internal NewWindowRequestedEventArgs(IWebView2NewWindowRequestedEventArgs args)
        {
            _args = args;
        }

        public bool Handled { get => _args.Handled; set => _args.Handled = value; }

        public bool IsUserInitiated => _args.IsUserInitiated;

        //public IWebView2WebView NewWindow { get => _args.NewWindow; set => _args.NewWindow = value; }
        public void put_NewWindow(IWebView2WebView newWindow)
        {
            _args.put_NewWindow(newWindow);
        }
        /// Gets the new window.
        public void get_NewWindow(out IWebView2WebView newWindow)
        {
            _args.get_NewWindow(out newWindow);
        }

        public string Uri => _args.Uri;

        public WebView2Deferral GetDeferal()
        {
            return new WebView2Deferral(_args.GetDeferral());
        }

        IWebView2Deferral IWebView2NewWindowRequestedEventArgs.GetDeferral()
        {
            return _args.GetDeferral();
        }
    }
}
