using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class NewWindowRequestedEventHandler : HandlerBase<NewWindowRequestedEventArgs>, IWebView2NewWindowRequestedEventHandler
    {
        public NewWindowRequestedEventHandler(Action<NewWindowRequestedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2NewWindowRequestedEventArgs args)
        {
            NewWindowRequestedEventArgs eventArgs = new NewWindowRequestedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
