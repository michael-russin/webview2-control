using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    internal class AddScriptToExecuteOnDocumentCreatedCompletedHandler : HandlerBase<AddScriptToExecuteOnDocumentCreatedCompletedEventArgs>,
        IWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler
    {
        public AddScriptToExecuteOnDocumentCreatedCompletedHandler(Action<AddScriptToExecuteOnDocumentCreatedCompletedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(int errorCode, string id)
        {
            AddScriptToExecuteOnDocumentCreatedCompletedEventArgs eventArgs = 
                new AddScriptToExecuteOnDocumentCreatedCompletedEventArgs(errorCode, id);
            Callback.Invoke(eventArgs);
        }
    }
}
