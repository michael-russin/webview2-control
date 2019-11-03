using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Events
{
    public class ExecuteScriptCompletedEventArgs : EventArgs
    {
        internal ExecuteScriptCompletedEventArgs(int errorCode, string resultObjectAsJson)
        {
            ErrorCode = errorCode;
            ResultAsJson = resultObjectAsJson;
        }

        public int ErrorCode { get; private set; }

        public string ResultAsJson { get; private set; }
    }
}
