using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class CallDevToolsProtocolMethodCompletedHandler : HandlerBase<CallDevToolsProtocolMethodCompletedEventArgs>, IWebView2CallDevToolsProtocolMethodCompletedHandler
    {
        public CallDevToolsProtocolMethodCompletedHandler(Action<CallDevToolsProtocolMethodCompletedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(int errorCode, string returnObjectAsJson)
        {
            CallDevToolsProtocolMethodCompletedEventArgs eventArgs = new CallDevToolsProtocolMethodCompletedEventArgs(errorCode, returnObjectAsJson);
            Callback(eventArgs);
        }
    }
}
