using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Handlers
{
    internal class EnvironmentCompletedHandler : HandlerBase<EnvironmentCreatedEventArgs>, IWebView2CreateWebView2EnvironmentCompletedHandler
    {
        public EnvironmentCompletedHandler(Action<EnvironmentCreatedEventArgs> callback) : base(callback)
        {
        }

        public void Invoke(int result, IWebView2Environment webViewEnvironment)
        {
            EnvironmentCreatedEventArgs eventArgs = new EnvironmentCreatedEventArgs(result, webViewEnvironment);

            Callback.Invoke(eventArgs);
        }
    }
}
