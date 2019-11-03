using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("64AF5AE3-27A1-47E0-8901-95119C1BA95B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2MoveFocusRequestedEventArgs
    {
        [DispId(1610678273)]
        int Handled
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            set;
        }

        [DispId(1610678272)]
        WEBVIEW2_MOVE_FOCUS_REASON reason
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}
