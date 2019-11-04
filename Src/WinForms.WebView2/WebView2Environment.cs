using System;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms.Handlers;

namespace Russinsoft.WinForms
{
    public class WebView2Environment 
    {
        private IWebView2Environment _environment;

        internal WebView2Environment(IWebView2Environment environment)
        {
            _environment = environment;
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
    }
}
