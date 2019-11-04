using System;
using Russinsoft.WebView2.Interop;


namespace Russinsoft.WinForms.Handlers
{
    class ProcessFailedEventHandler : HandlerBase<ProcessFailedEventArgs>, IWebView2ProcessFailedEventHandler
    {
        public ProcessFailedEventHandler(Action<ProcessFailedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2ProcessFailedEventArgs args)
        {
            ProcessFailedEventArgs eventArgs = new ProcessFailedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
