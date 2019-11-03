using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class DevToolsProtocolEventReceivedEventHandler : HandlerBase<DevToolsProtocolEventReceivedEventArgs>,
        IWebView2DevToolsProtocolEventReceivedEventHandler
    {
        private string _eventName;

        public DevToolsProtocolEventReceivedEventHandler(string eventName, Action<DevToolsProtocolEventReceivedEventArgs> callback) :
            base(callback)
        {
            _eventName = eventName;
        }

        public void Invoke(IWebView2WebView webview, IWebView2DevToolsProtocolEventReceivedEventArgs args)
        {
            DevToolsProtocolEventReceivedEventArgs eventArgs = new DevToolsProtocolEventReceivedEventArgs(_eventName, args);
            Callback.Invoke(eventArgs);
        }
    }
}
