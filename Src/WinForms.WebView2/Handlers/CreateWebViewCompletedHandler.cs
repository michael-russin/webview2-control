using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    internal class CreateWebViewCompletedHandler : HandlerBase<CreateWebViewCompletedEventArgs>, IWebView2CreateWebViewCompletedHandler
    {
        internal CreateWebViewCompletedHandler(Action<CreateWebViewCompletedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(int result, IWebView2WebView3 webView)
        {
            CreateWebViewCompletedEventArgs eventArgs = new CreateWebViewCompletedEventArgs(result, webView);
            Callback.Invoke(eventArgs);
        }
    }
}
