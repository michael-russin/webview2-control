using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
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
