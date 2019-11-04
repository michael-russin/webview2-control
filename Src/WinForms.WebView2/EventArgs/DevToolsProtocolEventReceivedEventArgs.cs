using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
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
