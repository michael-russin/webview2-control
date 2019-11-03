using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Events
{
    public class AddScriptToExecuteOnDocumentCreatedCompletedEventArgs
    {
        internal AddScriptToExecuteOnDocumentCreatedCompletedEventArgs(int errorCode, string id)
        {
            ErrorCode = errorCode;
            Id = id;
        }

        public int ErrorCode { get; private set; }

        public string Id { get; private set; }
    }
}
