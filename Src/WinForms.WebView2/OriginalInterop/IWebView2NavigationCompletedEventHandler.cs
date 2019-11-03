using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("DCEB3A27-C8C0-4DE7-889D-AF3DE80EDB3C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2NavigationCompletedEventHandler
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Invoke([In] IWebView2WebView webview, [In] IWebView2NavigationCompletedEventArgs args);
    }
}
