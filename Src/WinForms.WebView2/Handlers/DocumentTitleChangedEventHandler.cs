using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp.Handlers
{
    internal class DocumentTitleChangedEventHandler : HandlerBase<DocumentTitleChangedEventArgs>, 
        IWebView2DocumentTitleChangedEventHandler
    {
        public DocumentTitleChangedEventHandler(Action<DocumentTitleChangedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(IWebView2WebView3 webview, object args)
        {
            DocumentTitleChangedEventArgs completedArgs = new DocumentTitleChangedEventArgs(webview);
            Callback(completedArgs);
        }
    }
}
