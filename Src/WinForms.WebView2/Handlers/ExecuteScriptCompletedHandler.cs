using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class ExecuteScriptCompletedHandler : HandlerBase<ExecuteScriptCompletedEventArgs>, IWebView2ExecuteScriptCompletedHandler
    {
        public ExecuteScriptCompletedHandler(Action<ExecuteScriptCompletedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(int errorCode, string resultObjectAsJson)
        {
            ExecuteScriptCompletedEventArgs eventArgs = new ExecuteScriptCompletedEventArgs(errorCode, resultObjectAsJson);
            Callback.Invoke(eventArgs);
        }
    }
}
