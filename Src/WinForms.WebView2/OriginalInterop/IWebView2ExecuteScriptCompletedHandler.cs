using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("F5AC0E3B-8B92-45E5-ABEF-DB8518EFFF27")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2ExecuteScriptCompletedHandler
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Invoke([In] int errorCode, [In] string resultObjectAsJson);
    }
}
