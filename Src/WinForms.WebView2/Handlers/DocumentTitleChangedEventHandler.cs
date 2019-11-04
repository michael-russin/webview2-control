using System;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms.Handlers
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
