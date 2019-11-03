using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Handlers;
using static WebView2Sharp.SafeNativeMethods;

namespace WebView2Sharp
{
    public class WebView2Loader
    {
        // for Windows 10 version RS2 and above
        [DllImport(ExternDll.WebView2Loader, SetLastError = true)]
        private static extern int CreateWebView2EnvironmentWithDetails(
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string browserExecutableFolder,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string userDataFolder,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string additionalBrowserArguments,
                IWebView2CreateWebView2EnvironmentCompletedHandler environment_created_handler);


        public static int CreateEnvironmentWithDetails(
            string browserExecutableFolder,
            string userDataFolder,
            string additionalBrowserArguments,
            Action<EnvironmentCreatedEventArgs> callback)
        {
            SetProcessDpiAwarenessContext(DpiAwarenessContext.PER_MONITOR_AWARE_V2);

            EnvironmentCompletedHandler handler = new EnvironmentCompletedHandler(callback);
            int hr = CreateWebView2EnvironmentWithDetails(browserExecutableFolder,
                userDataFolder,
                additionalBrowserArguments,
                handler);
            return hr;
        }
    }
}
