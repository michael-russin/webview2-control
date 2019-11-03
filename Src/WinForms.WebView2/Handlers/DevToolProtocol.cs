using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class DevToolProtocol
    {
        public DevToolProtocol(string eventName, Action<DevToolsProtocolEventReceivedEventArgs> callback)
        {
            EventName = eventName;
            Callback = callback;
        }

        public string EventName { get; private set; }

        public Action<DevToolsProtocolEventReceivedEventArgs> Callback { get; private set; }
    }
}
