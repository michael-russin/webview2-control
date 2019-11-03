using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Demo.WinForms
{
    public class EnvironmentCompletedHandler : IWebView2CreateWebView2EnvironmentCompletedHandler
    {
        public void Invoke(int result, IWebView2Environment webViewEnvironment)
        {
            IWebView2Environment2 e2 = webViewEnvironment as IWebView2Environment2;
            string ver = e2.BrowserVersionInfo;
        }
    }
}
