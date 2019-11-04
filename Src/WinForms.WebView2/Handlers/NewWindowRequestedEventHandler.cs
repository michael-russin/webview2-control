using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    internal class NewWindowRequestedEventHandler : HandlerBase<NewWindowRequestedEventArgs>, IWebView2NewWindowRequestedEventHandler
    {
        public NewWindowRequestedEventHandler(Action<NewWindowRequestedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2NewWindowRequestedEventArgs args)
        {
            NewWindowRequestedEventArgs eventArgs = new NewWindowRequestedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
