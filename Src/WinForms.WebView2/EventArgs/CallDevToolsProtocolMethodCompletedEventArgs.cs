using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Events
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
