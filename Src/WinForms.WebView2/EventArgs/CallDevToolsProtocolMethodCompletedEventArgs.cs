using System;

namespace Russinsoft.WinForms
{
    public class CallDevToolsProtocolMethodCompletedEventArgs : EventArgs
    {
        internal CallDevToolsProtocolMethodCompletedEventArgs(int errorCode, string response)
        {
            ErrorCode = errorCode;
            Response = response;
        }

        public int ErrorCode { get; private set; }

        public string Response { get; private set; }
    }
}
