using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class ScriptDialogOpeningEventHandler : HandlerBase<ScriptDialogOpeningEventArgs>, IWebView2ScriptDialogOpeningEventHandler
    {
        public ScriptDialogOpeningEventHandler(Action<ScriptDialogOpeningEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2ScriptDialogOpeningEventArgs args)
        {
            ScriptDialogOpeningEventArgs eventArgs = new ScriptDialogOpeningEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
