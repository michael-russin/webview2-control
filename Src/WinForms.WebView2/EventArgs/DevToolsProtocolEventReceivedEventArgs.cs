using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class DevToolsProtocolEventReceivedEventArgs : EventArgs, IWebView2DevToolsProtocolEventReceivedEventArgs
    {
        internal DevToolsProtocolEventReceivedEventArgs(string eventName, IWebView2DevToolsProtocolEventReceivedEventArgs args)
        {
            ParameterObjectAsJson = args.ParameterObjectAsJson;
            EventName = eventName;
        }

        public string EventName { get; private set; }

        public string ParameterObjectAsJson
        {
            get; private set;
        }
    }
}
