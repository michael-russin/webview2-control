using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
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
