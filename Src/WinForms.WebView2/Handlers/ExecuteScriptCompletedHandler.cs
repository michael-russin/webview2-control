using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
