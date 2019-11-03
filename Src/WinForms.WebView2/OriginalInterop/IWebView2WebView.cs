using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    [Guid("76711B9E-8D56-4806-8485-35250BB2384F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWebView2WebView
    {
        [DispId(1610678306)]
        tagRECT Bounds
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            set;
        }

        [DispId(1610678320)]
        uint BrowserProcessId
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(1610678321)]
        int CanGoBack
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(1610678322)]
        int CanGoForward
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(1610678310)]
        int IsVisible
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            set;
        }

        [DispId(1610678272)]
        IWebView2Settings Settings
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(1610678273)]
        string Source
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(1610678308)]
        double ZoomFactor
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            set;
        }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_DevToolsProtocolEventReceived([In] string eventName, [In] IWebView2DevToolsProtocolEventReceivedEventHandler handler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_DocumentStateChanged([In] IWebView2DocumentStateChangedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_FrameNavigationStarting([In] IWebView2NavigationStartingEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_GotFocus([In] IWebView2FocusChangedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_LostFocus([In] IWebView2FocusChangedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_MoveFocusRequested([In] IWebView2MoveFocusRequestedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_NavigationCompleted([In] IWebView2NavigationCompletedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_NavigationStarting([In] IWebView2NavigationStartingEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_PermissionRequested([In] IWebView2PermissionRequestedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_ProcessFailed([In] IWebView2ProcessFailedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_ScriptDialogOpening([In] IWebView2ScriptDialogOpeningEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_WebMessageReceived([In] IWebView2WebMessageReceivedEventHandler handler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_WebResourceRequested([In] ref string urlFilter, [In] ref WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContextFilter, [In][ComAliasName("WebView2.Interop.ULONG_PTR")] ulong filterLength, [In] IWebView2WebResourceRequestedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void add_ZoomFactorChanged([In] IWebView2ZoomFactorChangedEventHandler eventHandler, out EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddScriptToExecuteOnDocumentCreated([In] string javaScript, [In] IWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler handler);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CallDevToolsProtocolMethod([In] string methodName, [In] string parametersAsJson, [In] IWebView2CallDevToolsProtocolMethodCompletedHandler handler);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CapturePreview([In] WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat, [In] IStream imageStream, [In] IWebView2CapturePreviewCompletedHandler handler);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Close();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ExecuteScript([In] string javaScript, [In] IWebView2ExecuteScriptCompletedHandler handler);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GoBack();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GoForward();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void MoveFocus([In] WEBVIEW2_MOVE_FOCUS_REASON reason);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Navigate([In] string uri);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void NavigateToString([In] string htmlContent);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void PostWebMessageAsJson([In] string webMessageAsJson);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void PostWebMessageAsString([In] string webMessageAsString);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reload();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_DevToolsProtocolEventReceived([In] string eventName, [In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_DocumentStateChanged([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_FrameNavigationStarting([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_GotFocus([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_LostFocus([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_MoveFocusRequested([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_NavigationCompleted([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_NavigationStarting([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_PermissionRequested([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_ProcessFailed([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_ScriptDialogOpening([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_WebMessageReceived([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_WebResourceRequested([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void remove_ZoomFactorChanged([In] EventRegistrationToken token);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveScriptToExecuteOnDocumentCreated([In] string id);
    }
}
