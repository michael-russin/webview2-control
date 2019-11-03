using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("0E682B9A-B686-4327-9A56-E0305705A3DB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2WebMessageReceivedEventHandler
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Invoke([In] IWebView2WebView webview, [In] IWebView2WebMessageReceivedEventArgs args);
    }
}
