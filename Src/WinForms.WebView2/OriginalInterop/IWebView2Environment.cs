using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("33D17ECE-82FA-47D9-8978-CD17FF3C3CC6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2Environment
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateWebResourceResponse(IStream Content, int StatusCode, string ReasonPhrase, string Headers, ref IWebView2WebResourceResponse Response);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateWebView([ComAliasName("WebView2.Interop.wireHWND")] ref _RemotableHandle parentWindow, IWebView2CreateWebViewCompletedHandler handler);
    }
}
