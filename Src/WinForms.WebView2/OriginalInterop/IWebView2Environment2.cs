using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("013124F3-02FD-4DFF-8911-06016AF1E3EE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2Environment2 : IWebView2Environment
    {
        [DispId(1610743808)]
        string BrowserVersionInfo
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateWebResourceResponse(IStream Content, int StatusCode, string ReasonPhrase, string Headers, ref IWebView2WebResourceResponse Response);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateWebView([ComAliasName("WebView2.Interop.wireHWND")] ref _RemotableHandle parentWindow, IWebView2CreateWebViewCompletedHandler handler);
    }
}
