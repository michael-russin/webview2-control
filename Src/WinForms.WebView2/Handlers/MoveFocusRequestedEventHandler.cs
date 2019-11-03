using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
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
