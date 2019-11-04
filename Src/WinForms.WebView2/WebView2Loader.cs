using System;
using System.Runtime.InteropServices;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms.Handlers;
using static Russinsoft.WinForms.SafeNativeMethods;

namespace Russinsoft.WinForms
{
    public class WebView2Loader
    {
        private WebView2Loader() { }

        public static int CreateEnvironmentWithDetails(
            string browserExecutableFolder,
            string userDataFolder,
            string additionalBrowserArguments,
            Action<EnvironmentCreatedEventArgs> callback)
        {
            SetProcessDpiAwarenessContext(DpiAwarenessContext.PER_MONITOR_AWARE_V2);

            EnvironmentCompletedHandler handler = new EnvironmentCompletedHandler(callback);
            int hr = Globals.CreateWebView2EnvironmentWithDetails(browserExecutableFolder,
                userDataFolder,
                additionalBrowserArguments,
                handler);
            return hr;
        }

        public static int CreateEnvironment(Action<EnvironmentCreatedEventArgs> callback)
        {
            SetProcessDpiAwarenessContext(DpiAwarenessContext.PER_MONITOR_AWARE_V2);

            EnvironmentCompletedHandler handler = new EnvironmentCompletedHandler(callback);
            int hr = Globals.CreateWebView2Environment(handler);
            return hr;
        }

        public static int GetWebView2BrowserVersionInfo(string browserExecutableFolder, string versionInfo)
        {
            int hr = Globals.GetWebView2BrowserVersionInfo(browserExecutableFolder, versionInfo);
            return hr;
        }
    }
}
