using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
{
    public class WebView2Deferral : IWebView2Deferral
    {
        private IWebView2Deferral _deferral;

        internal WebView2Deferral(IWebView2Deferral deferral)
        {
            _deferral = deferral;
        }

        public void Complete()
        {
            _deferral.Complete();
        }
    }
}
