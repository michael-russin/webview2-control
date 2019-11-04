using System;

namespace Russinsoft.WinForms.Handlers
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
