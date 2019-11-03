using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp
{
    public class Deferral : IWebView2Deferral
    {
        private IWebView2Deferral _deferral;

        internal Deferral(IWebView2Deferral deferral)
        {
            _deferral = deferral;
        }

        public void Complete()
        {
            _deferral.Complete();
        }
    }
}
