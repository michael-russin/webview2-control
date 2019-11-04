using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
