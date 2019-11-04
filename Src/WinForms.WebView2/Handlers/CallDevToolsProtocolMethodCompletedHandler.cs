using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
