using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    class ProcessFailedEventHandler : HandlerBase<ProcessFailedEventArgs>, IWebView2ProcessFailedEventHandler
    {
        public ProcessFailedEventHandler(Action<ProcessFailedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2ProcessFailedEventArgs args)
        {
            ProcessFailedEventArgs eventArgs = new ProcessFailedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
