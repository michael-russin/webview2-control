using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
{
    class MoveFocusRequestedEventHandler : HandlerBase<MoveFocusRequestedEventArgs>, 
        IWebView2MoveFocusRequestedEventHandler
    {
        public MoveFocusRequestedEventHandler(Action<MoveFocusRequestedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView webview, IWebView2MoveFocusRequestedEventArgs args)
        {
            MoveFocusRequestedEventArgs eventArgs = new MoveFocusRequestedEventArgs(args);
            Callback.Invoke(eventArgs);
        }
    }
}
