using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using WinForms=System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Input;
using System.IO;

namespace MtrDev.WebView2.Wpf
{
    public class WebView2Control : HwndHost 
    {
        BrowserParentWindow _webView2Host;

        private WebView2Environment _webViewEnvironment;
        private WebView2WebView _webView2WebView;
        private IntPtr _parentWindow;
        private string _internalUrl;
        private double _initialZoomFactor = 1.0;
        private bool _initialAreDevToolsEnabled = true;
        private bool _initialAreDefaultScriptDialogsEnabled = true;
        private bool _initialIsScriptEnabled = true;
        private bool _initialIsWebMessageEnabled = true;
        private bool _initialIsStatusBarEnabled = true;
        private bool _initialAreDefaultContextMenusEnabled = true;
        private bool _hasFocus;

        public WebView2Control()
        {
        }

        public WebView2Control(WebView2Environment webViewEnvironment)
        {
            _webViewEnvironment = webViewEnvironment;
        }

        #region Public Properties

        /// <summary>
        ///     The DependencyProperty for the TabIndex property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      1
        /// </summary>
        internal static readonly DependencyProperty TabIndexProperty
            = Control.TabIndexProperty.AddOwner(typeof(WebView2Control));

        /// <summary>
        ///     TabIndex property change the order of Tab navigation between Controls.
        ///     Control with lower TabIndex will get focus before the Control with higher index
        /// </summary>
        public int TabIndex
        {
            get { return (int)GetValue(TabIndexProperty); }
            set { SetValue(TabIndexProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty for the IsTabStop property.
        /// </summary>
        public static readonly DependencyProperty IsTabStopProperty
                = KeyboardNavigation.IsTabStopProperty.AddOwner(typeof(WebView2Control));

        /// <summary>
        ///     Determine is the Control should be considered during Tab navigation.
        ///     If IsTabStop is false then it is excluded from Tab navigation
        /// </summary>
        [Bindable(true), Category("Behavior")]
        public bool IsTabStop
        {
            get { return (bool)GetValue(IsTabStopProperty); }
            set { SetValue(IsTabStopProperty, value); }
        }


        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WebView2Environment WebView2Environment
        {
            get { return _webViewEnvironment; }
        }

        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IWebView2WebView InnerWebView2WebView
        {
            get { return _webView2WebView.InnerWebView2WebView; }
        }

        [
            Bindable(true),
            DefaultValue(null)
        ]
        public string Url
        {
            get
            {
                return InternalUrl;
            }
            set
            {
                if (value != null && value.ToString() == "")
                {
                    value = null;
                }
                InternalNavigate(value);
            }
        }

        /// <summary>
        /// Can navigate the webview to the previous page in the navigation history.
        /// CanGoBack change value with the <see cref="DocumentStateChanged"/> event.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanGoBack
        {
            get
            {
                if (_webView2WebView == null)
                    return false;
                return _webView2WebView.CanGoBack;
            }
        }

        /// <summary>
        /// Can navigate the webview to the next page in the navigation history.
        /// CanGoForward change value with the <see cref="DocumentStateChanged"/>  event.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanGoForward
        {
            get
            {
                if (_webView2WebView == null)
                    return false;
                return _webView2WebView.CanGoForward;
            }
        }

        /// <summary>
        /// The URI of the current top level document. This value potentially
        /// changes as a part of the <see cref="DocumentStateChanged"/> event firing for some cases
        /// such as navigating to a different site or fragment navigations. It will
        /// remain the same for other types of navigations such as page reloads or
        /// history.pushState with the same URL as the current page.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Source
        {
            get
            {
                if (_webView2WebView != null)
                {
                    return _webView2WebView.Source;
                }
                return string.Empty;
            }
        }

        [DefaultValue(true)]
        public bool AreDevToolsEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialAreDevToolsEnabled;
                return _webView2WebView.Settings.AreDevToolsEnabled;
            }
            set
            {
                _initialAreDevToolsEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.AreDevToolsEnabled = value;
                }
            }
        }

        [DefaultValue(true)]
        public bool AreDefaultScriptDialogsEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialAreDefaultScriptDialogsEnabled;
                return _webView2WebView.Settings.AreDefaultScriptDialogsEnabled;
            }
            set
            {
                _initialAreDefaultScriptDialogsEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.AreDefaultScriptDialogsEnabled = value;
                }
            }
        }

        [DefaultValue(true)]
        public bool IsScriptEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialIsScriptEnabled;
                return _webView2WebView.Settings.IsScriptEnabled;
            }
            set
            {
                _initialIsScriptEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.IsScriptEnabled = value;
                }
            }
        }

        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ContainsFullScreenElement
        {
            get
            {
                if (_webView2WebView == null)
                    return false;
                return _webView2WebView.ContainsFullScreenElement;
            }
        }

        [DefaultValue(true)]
        public bool IsWebMessageEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialIsWebMessageEnabled;
                return _webView2WebView.Settings.IsWebMessageEnabled;
            }
            set
            {
                _initialIsWebMessageEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.IsWebMessageEnabled = value;
                }
            }
        }

        [DefaultValue(true)]
        public bool IsStatusBarEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialIsStatusBarEnabled;
                return _webView2WebView.Settings.IsStatusBarEnabled;
            }
            set
            {
                _initialIsStatusBarEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.IsStatusBarEnabled = value;
                }
            }
        }

        [DefaultValue(true)]
        public bool AreDefaultContextMenusEnabled
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialAreDefaultContextMenusEnabled;
                return _webView2WebView.Settings.AreDefaultContextMenusEnabled;
            }
            set
            {
                _initialAreDefaultContextMenusEnabled = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.AreDefaultContextMenusEnabled = value;
                }
            }
        }

        /// <summary>
        /// The zoom factor for the current page in the WebView.
        /// The zoom factor is persisted per site.
        /// Note that changing zoom factor could cause `window.innerWidth/innerHeight`
        /// and page layout to change.
        /// When WebView navigates to a page from a different site,
        /// the zoom factor set for the previous page will not be applied.
        /// If the app wants to set the zoom factor for a certain page, the earliest
        /// place to do it is in the DocumentStateChanged event handler. Note that if it
        /// does that, it might receive a ZoomFactorChanged event for the persisted
        /// zoom factor before receiving the ZoomFactorChanged event for the specified
        /// zoom factor.
        /// Specifying a zoomFactor less than or equal to 0 is not allowed.
        /// WebView also has an internal supported zoom factor range. When a specified
        /// zoom factor is out of that range, it will be normalized to be within the
        /// range, and a ZoomFactorChanged event will be fired for the real
        /// applied zoom factor. When this range normalization happens, the
        /// ZoomFactor property will report the zoom factor specified during the
        /// previous modification of the ZoomFactor property until the
        /// ZoomFactorChanged event is received after webview applies the normalized
        /// zoom factor.
        /// </summary>
        [Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ZoomFactor
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialZoomFactor;
                return _webView2WebView.ZoomFactor;
            }
            set
            {
                _initialZoomFactor = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.ZoomFactor = value;
                }
            }
        }

        [Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Version
        {
            get
            {
                if (_webViewEnvironment == null)
                    return string.Empty;
                return _webViewEnvironment.BrowserVersionInfo;
            }
        }

        /// <summary>
        /// The process id of the browser process that hosts the WebView.
        /// </summary>
        [Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public uint BrowserProcessId
        {
            get
            {
                if (_webView2WebView != null)
                    return _webView2WebView.BrowserProcessId;
                return 0;
            }
        }

        /// <summary>
        /// The title for the current top level document.
        /// If the document has no explicit title or is otherwise empty,
        /// a default that may or may not match the URI of the document will be used.
        /// </summary>
        [Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DocumentTitle
        {
            get
            {
                if (_webView2WebView != null)
                    return _webView2WebView.DocumentTitle;
                return string.Empty;
            }
        }
        #endregion


        #region Public Methods

        /// <summary>
        /// Cause a navigation of the top level document to the specified URI. See
        /// the navigation events for more information. Note that this starts a
        /// navigation and the corresponding <see cref="NavigationStarting"/> event will fire
        /// sometime after this Navigate call completes.
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            InternalNavigate(url);
        }

        /// <summary>
        /// Initiates a navigation to htmlContent as source HTML of a new
        /// document. The htmlContent parameter may not be larger than 2 MB of
        /// characters. The origin of the new page will be about:blank.
        /// </summary>
        /// <param name="htmlContent"></param>
        public void NavigateToString(string htmlContent)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.NavigateToString(htmlContent);
        }

        /// <summary>
        /// Navigates the webview to the next page in the navigation history.
        /// </summary>
        public void GoForward()
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.GoForward();
            }
        }

        /// <summary>
        /// Navigates the webview to the previous page in the navigation history.
        /// </summary>
        public void GoBack()
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.GoBack();
            }
        }

        /// <summary>
        /// Reload the current page. This is similar to navigating to the URI of
        /// current top level document including all navigation events firing and
        /// respecting any entries in the HTTP cache. But, the back/forward history
        /// will not be modified.
        /// </summary>
        public void Reload()
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.Reload();
            }
        }

        /// <summary>
        /// This is a helper for posting a message that is a simple string
        /// rather than a JSON string representation of a JavaScript object. This
        /// behaves in exactly the same manner as PostWebMessageAsJson but the
        /// `window.chrome.webview` message event arg's data property will be a string
        /// with the same value as webMessageAsString. Use this instead of
        /// PostWebMessageAsJson if you want to communicate via simple strings rather
        /// than JSON objects.
        /// </summary>
        /// <param name="json"></param>
        public void PostWebMessageAsString(string json)
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.PostWebMessageAsString(json);
            }
        }

        /// <summary>
        /// Post the specified webMessage to the top level document in this
        /// IWebView2WebView. The top level document's window.chrome.webview's message
        /// event fires. JavaScript in that document may subscribe and unsubscribe to
        /// the event via the following:
        /// ```
        ///    window.chrome.webview.addEventListener('message', handler)
        ///    window.chrome.webview.removeEventListener('message', handler)
        /// ```
        /// The event args is an instance of `MessageEvent`.
        /// The IWebView2Settings::IsWebMessageEnabled setting must be true or this method
        /// will fail with E_INVALIDARG.
        /// The event arg's data property is the webMessage string parameter parsed
        /// as a JSON string into a JavaScript object.
        /// The event arg's source property is a reference to the
        /// `window.chrome.webview` object.
        /// See SetWebMessageReceivedEventHandler for information on sending messages
        /// from the HTML document in the webview to the host.
        /// This message is sent asynchronously. If a navigation occurs before the
        /// message is posted to the page, then the message will not be sent.
        /// </summary>
        /// <param name="json"></param>
        public void PostWebMessageAsJson(string json)
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.PostWebMessageAsJson(json);
            }
        }

        /// <summary>
        /// Call an asynchronous DevToolsProtocol method. See the
        /// [DevTools Protocol Viewer](https://aka.ms/DevToolsProtocolDocs)
        /// for a list and description of available methods.
        /// The methodName parameter is the full name of the method in the format
        /// `{domain}.{method}`.
        /// The parametersAsJson parameter is a JSON formatted string containing
        /// the parameters for the corresponding method.
        /// The handler's Invoke method will be called when the method asynchronously
        /// completes. Invoke will be called with the method's return object as a
        /// JSON string.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parametersAsJson"></param>
        /// <param name="callback">Callback handler or null</param>
        public void CallDevToolsProtocolMethod(string methodName, string parametersAsJson, Action<CallDevToolsProtocolMethodCompletedEventArgs> callback)
        {
            _webView2WebView.CallDevToolsProtocolMethod(methodName, parametersAsJson, callback);
        }

        /// <summary>
        /// Subscribe to a DevToolsProtocol event. See the
        /// [DevTools Protocol Viewer](https://aka.ms/DevToolsProtocolDocs)
        /// for a list and description of available events.
        /// The eventName parameter is the full name of the event in the format
        /// `{domain}.{event}`.
        /// The handler's Invoke method will be called whenever the corresponding
        /// DevToolsProtocol event fires. Invoke will be called with the
        /// an event args object containing the CDP event's parameter object as a JSON
        /// string.
        /// </summary>
        /// <param name="eventName"></param>
        public void StartListeningDevToolsProtocolEvent(string eventName)
        {
            if (_devToolsProtocolEventTokenDictionary.ContainsKey(eventName))
            {
                return;
            }

            long token = _webView2WebView.RegisterDevToolsProtocolEventReceived(eventName, OnDevToolsProtocolEventReceived);
            _devToolsProtocolEventTokenDictionary.Add(eventName, token);
        }

        /// <summary>
        /// Stop listening to a DevToolsProtocol event
        /// </summary>
        /// <param name="eventName"></param>
        public void StopListeningDevToolsProtocolEvent(string eventName)
        {
            if (!_devToolsProtocolEventTokenDictionary.ContainsKey(eventName))
            {
                return;
            }
            long token = _devToolsProtocolEventTokenDictionary[eventName];
            _devToolsProtocolEventTokenDictionary.Remove(eventName);
            _webView2WebView.UnregisterDevToolsProtocolEventReceived(token);
        }

        /// <summary>
        /// Execute JavaScript code from the javascript parameter in the
        /// current top level document rendered in the WebView. This will execute
        /// asynchronously and when complete, if a handler is provided in the
        /// ExecuteScriptCompletedHandler parameter, its Invoke method will be
        /// called with the result of evaluating the provided JavaScript. The result
        /// value is a JSON encoded string.
        /// If the result is undefined, contains a reference cycle, or otherwise
        /// cannot be encoded into JSON, the JSON null value will be returned as the
        /// string 'null'. Note that a function that has no explicit return value
        /// returns undefined.
        /// If the executed script throws an unhandled exception, then the result is
        /// also 'null'.
        /// This method is applied asynchronously. If the call is made while the
        /// webview is on one document, and a navigation occurs after the call is
        /// made but before the JavaScript is executed, then the script will not be
        /// executed and the handler will be called with E_FAIL for its errorCode
        /// parameter.
        /// ExecuteScript will work even if IsScriptEnabled is set to FALSE.
        /// </summary>
        /// <param name="javaScript"></param>
        /// <param name="callback"></param>
        public void ExecuteScript(string javaScript, Action<ExecuteScriptCompletedEventArgs> callback)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.ExecuteScript(javaScript, callback);
        }

        new public bool Focus()
        {
            return MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PROGRAMMATIC);
            //return base.Focus();
        }

        /// <summary>
        /// Add the provided JavaScript to a list of scripts
        /// that should be executed after the global object has been created, but
        /// before the HTML document has been parsed and before any other script
        /// included by the HTML document is executed. The
        /// injected script will apply to all future top level document and child
        /// frame navigations until removed with RemoveScriptToExecuteOnDocumentCreated.
        /// This is applied asynchronously and you must wait for the completion
        /// handler to run before you can be sure that the script is ready to
        /// execute on future navigations.
        ///
        /// Note that if an HTML document has sandboxing of some kind via [sandbox](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/iframe#attr-sandbox)
        /// properties or the [Content-Security-Policy HTTP header](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy)
        /// this will affect the script run
        /// here. So, for example, if the 'allow-modals' keyword is not set then calls
        /// to the `alert` function will be ignored.
        /// </summary>
        /// <param name="javaScript"></param>
        /// <param name="handler"></param>
        public void AddScriptToExecuteOnDocumentCreated(string javaScript, Action<AddScriptToExecuteOnDocumentCreatedCompletedEventArgs> callback)
        {
            if (_webView2WebView == null)
                return;
            if (string.IsNullOrEmpty(javaScript))
                throw new ArgumentNullException("javaScript");

            _webView2WebView.AddScriptToExecuteOnDocumentCreated(javaScript, callback);
        }

        /// <summary>
        /// Remove the corresponding JavaScript added via AddScriptToExecuteOnDocumentCreated.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveScriptToExecuteOnDocumentCreated(string id)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.RemoveScriptToExecuteOnDocumentCreated(id);
        }

        /// <summary>
        /// Closes the webview and cleans up the underlying browser instance.
        /// Cleaning up the browser instace will release the resources powering the webview.
        /// The browser instance will be shut down if there are no other webviews using it.
        ///
        /// After calling Close, all method calls will fail and event handlers
        /// will stop firing. Specifically, the WebView will release its references
        /// to its event handlers when Close is called.
        /// </summary>
        public void Close()
        {
            if (_webView2WebView == null)
                return;
            UnregisterHandlers();
            _webView2WebView.Close();
        }

        /// <summary>
        /// Stop all navigations and pending resource fetches
        /// </summary>
        public void Stop()
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.Stop();
        }

        /// <summary>
        /// Opens the DevTools window for the current document in the WebView.
        /// Does nothing if called when the DevTools window is already open
        /// </summary>
        public void OpenDevToolsWindow()
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.OpenDevToolsWindow(); ;
        }

        public void AddWebResourceRequestedFilter(string uri, WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContext)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.AddWebResourceRequestedFilter(uri, resourceContext);
        }

        public void RemoveWebResourceRequestedFilter(string uri, WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContext)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.RemoveWebResourceRequestedFilter(uri, resourceContext);
        }
        public void CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat, Stream imageStream, Action<CapturePreviewCompletedArgs> callback)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.CapturePreview(imageFormat, imageStream, callback);
        }

        public void AddRemoteObject(string name, ref object remoteObject)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.AddRemoteObject(name, ref remoteObject);
        }

        public void RemoveRemoteObject(string name)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.RemoveRemoteObject(name);
        }
        #endregion

        #region Public Events
        /// <summary>
        /// Occurs before the environment is created for the web browser.  Provides ability to change
        /// cache directory, browser version and command line parameters.
        /// </summary>
        public event EventHandler<BeforeEnvironmentCreatedEventArgs> BeforeEnvironmentCreated;

        /// <summary>
        /// Occurs after the evironment is created, either returns the environment or an error code
        /// why the creation failed.
        /// </summary>
        public event EventHandler<EnvironmentCreatedEventArgs> EnvironmentCreated;

        /// <summary>
        /// Occurs after the inner WebView2 is created and attached to the parent window.
        /// </summary>
        public event EventHandler<EventArgs> BrowserCreated;

        /// <summary>
        /// Fires when the WebView main frame is requesting permission to navigate to a 
        /// different URI. This will fire for redirects as well.
        /// </summary>
        public event EventHandler<NavigationStartingEventArgs> NavigationStarting;

        /// <summary>
        /// fires when a child frame in the WebView
        /// requesting permission to navigate to a different URI. This will fire for
        /// redirects as well.
        /// </summary>
        public event EventHandler<NavigationStartingEventArgs> FrameNavigationStarting;

        /// <summary>
        /// DocumentStateChanged fires when new content has started loading
        /// on the webview's main frame or if a same page navigation occurs (such as
        /// through fragment navigations or history.pushState navigations).
        /// This follows the NavigationStarting event and precedes the
        /// NavigationCompleted event.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<DocumentStateChangedEventArgs> DocumentStateChanged;

        /// <summary>
        /// NavigationCompleted event fires when the WebView has completely loaded
        /// (body.onload has fired) or loading stopped with error.
        /// </summary>
        public event EventHandler<NavigationCompletedEventArgs> NavigationCompleted;

        /// <summary>
        /// The event fires when the ZoomFactor property of the WebView changes.
        /// The event could fire because the caller modified the ZoomFactor property,
        /// or due to the user manually modifying the zoom. When it is modified by the
        /// caller via the ZoomFactor property, the internal zoom factor is updated
        /// immediately and there will be no ZoomFactorChanged event.
        /// WebView associates the last used zoom factor for each site. Therefore, it
        /// is possible for the zoom factor to change when navigating to a different
        /// page. When the zoom factor changes due to this, the ZoomFactorChanged
        /// event fires right after the DocumentStateChanged event.
        /// </summary>
        public event EventHandler<ZoomFactorCompletedEventArgs> ZoomFactorChanged;

        /// <summary>
        /// This event fires when the IsWebMessageEnabled setting is set and the top
        /// level document of the webview calls `window.chrome.webview.postMessage`.
        /// The postMessage function is `void postMessage(object)` where
        /// object is any object supported by JSON conversion.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<WebMessageReceivedEventArgs> WebMessageRecieved;

        /// <summary>
        /// MoveFocusRequested fires when user tries to tab out of the WebView.
        /// The WebView's focus has not changed when this event is fired.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<MoveFocusRequestedEventArgs> MoveFocusRequested;

        /// <summary>
        /// The event fires when a JavaScript dialog (alert, confirm, or prompt) will
        /// show for the webview. This event only fires if the
        /// <see cref="AreDefaultContextMenusEnabled"/> property is set to false.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<ScriptDialogOpeningEventArgs> ScriptDialogOpening;

        /// <summary>
        /// Fires when content in a WebView requests permission to access some
        /// privileged resources.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<PermissionRequestedEventArgs> PermissionRequested;

        /// <summary>
        /// Fires when a WebView process terminated unexpectedly or
        /// become unresponsive.
        /// </summary>
        [Browsable(false)]
        public event EventHandler<ProcessFailedEventArgs> ProcessFailed;

        /// <summary>
        /// The event fires when the DocumentTitle property of the WebView changes
        /// and may fire before or after the NavigationCompleted event.
        /// </summary>
        public event EventHandler<DocumentTitleChangedEventArgs> DocumentTitleChanged;

        /// <summary>
        /// Fires when content inside the WebView requested to open a new window,
        /// such as through window.open. The app can pass a target
        /// webview that will be considered the opened window.
        /// </summary>
        public event EventHandler<NewWindowRequestedEventArgs> NewWindowRequested;

        /// <summary>
        /// AcceleratorKeyPressed fires when an accelerator key or key combo is
        /// pressed or released while the WebView is focused. A key is considered an
        /// accelerator if either:
        ///   1. Ctrl or Alt is currently being held, or
        ///   2. the pressed key does not map to a character.
        /// A few specific keys are never considered accelerators, such as Shift.
        /// The Escape key is always considered an accelerator.
        /// </summary>
        public event EventHandler<AcceleratorKeyPressedEventArgs> AcceleratorKeyPressed;

        /// <summary>
        /// Notifies when the ContainsFullScreenElement property changes. This means
        /// that an HTML element inside the WebView is entering fullscreen to the size
        /// of the WebView or leaving fullscreen.
        /// This event is useful when, for example, a video element requests to go
        /// fullscreen. The listener of ContainsFullScreenElementChanged can then
        /// resize the WebView in response.
        /// </summary>
        public event EventHandler<ContainsFullScreenElementChangedEventArgs> ContainsFullScreenElementChanged;

        /// Fires when the WebView has performs any HTTP request.
        /// Use urlFilter to pass in a list with size filterLength of urls to listen
        /// for. Each url entry also supports wildcards: '*' matches zero or more
        /// characters, and '?' matches exactly one character. For each urlFilter
        /// entry, provide a matching resourceContextFilter representing the types of
        /// resources for which WebResourceRequested should fire.
        /// If filterLength is 0, the event will fire for all network requests.
        /// The supported resource contexts are:
        /// Document, Stylesheet, Image, Media, Font, Script, XHR, Fetch.
        /// </summary>
        public event EventHandler<WebResourceRequestedEventArgs> WebResourceRequested;

        /// <summary>
        /// See the
        /// [DevTools Protocol Viewer](https://aka.ms/DevToolsProtocolDocs)
        /// for a list and description of available events.
        /// The eventName parameter is the full name of the event in the format
        /// `{domain}.{event}`.
        /// The handler's Invoke method will be called whenever the corresponding
        /// DevToolsProtocol event fires. Invoke will be called with the
        /// an event args object containing the CDP event's parameter object as a JSON
        /// string.
        ///
        /// </summary>
        public event EventHandler<DevToolsProtocolEventReceivedEventArgs> DevToolsProtocolEventReceived;
        #endregion

        #region Public Overrides
        #endregion

        #region Protected Overrides

        //        protected override void OnResize(EventArgs e)
        //        {
        //            base.OnResize(e);
        //            ResizeWebView();
        //        }

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    if (_webView2WebView != null)
        //    {
        //        _webView2WebView.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PROGRAMMATIC);
        //    }
        //    base.OnGotFocus(e);
        //}
        protected override void OnWindowPositionChanged(Rect rcBoundingBox)
        {
            base.OnWindowPositionChanged(rcBoundingBox);
            if (_webView2WebView != null)
            {
                _webView2WebView.Bounds = new System.Drawing.Rectangle((int)0,
                                                (int)0,
                                                (int)rcBoundingBox.Width,
                                                (int)rcBoundingBox.Height);
            }
        }

        protected override bool TabIntoCore(TraversalRequest request)
        {
            if (_webView2WebView != null && 
                (request.FocusNavigationDirection == FocusNavigationDirection.First ||
                 request.FocusNavigationDirection == FocusNavigationDirection.Last))
            {
                _webView2WebView.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PROGRAMMATIC);
                return true;
            }
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(request);
            }
            return false;
//            bool iWantTheTab =  base.TabIntoCore(request);
//            return iWantTheTab;
        }

        #endregion

        #region Protected Virtuals

        protected virtual void OnBeforeEnvironmentCreated(BeforeEnvironmentCreatedEventArgs e)
        {
            if (BeforeEnvironmentCreated != null)
            {
                BeforeEnvironmentCreated(this, e);
            }
        }

        protected virtual void OnEnvironmentCreated(EnvironmentCreatedEventArgs e)
        {
            if (EnvironmentCreated != null)
            {
                EnvironmentCreated(this, e);
            }
        }

        protected virtual void OnBrowserCreated(EventArgs e)
        {
            if (BrowserCreated != null)
            {
                BrowserCreated(this, e);
            }
        }

        protected virtual void OnNavigationStarting(NavigationStartingEventArgs e)
        {
            if (NavigationStarting != null)
            {
                NavigationStarting(this, e);
            }
        }



        protected virtual void OnNavigationCompleted(NavigationCompletedEventArgs e)
        {
            if (NavigationCompleted != null)
            {
                NavigationCompleted(this, e);
            }
        }

        protected virtual void OnZoomFactorChanged(ZoomFactorCompletedEventArgs e)
        {
            if (ZoomFactorChanged != null)
            {
                ZoomFactorChanged(this, e);
            }
        }

        protected virtual void OnWebMessageRecieved(WebMessageReceivedEventArgs e)
        {
            if (WebMessageRecieved != null)
            {
                WebMessageRecieved(this, e);
            }
        }

        protected virtual void OnDocumentStateChanged(DocumentStateChangedEventArgs e)
        {
            if (DocumentStateChanged != null)
            {
                DocumentStateChanged(this, e);
            }
        }

        protected virtual void OnDevToolsProtocolEventReceived(DevToolsProtocolEventReceivedEventArgs e)
        {
            if (DevToolsProtocolEventReceived != null)
            {
                DevToolsProtocolEventReceived(this, e);
            }
        }

        protected virtual void OnFrameNavigationStarting(NavigationStartingEventArgs e)
        {
            if (FrameNavigationStarting != null)
            {
                FrameNavigationStarting(this, e);
            }
        }

        protected virtual void OnMoveFocusRequested(MoveFocusRequestedEventArgs e)
        {
            if (MoveFocusRequested != null)
            {
                MoveFocusRequested(this, e);
            }

        }

        protected virtual void OnWebResourceRequested(WebResourceRequestedEventArgs e)
        {
            if (WebResourceRequested != null)
            {
                WebResourceRequested(this, e);
            }
        }

        protected virtual void OnScriptDialogOpening(ScriptDialogOpeningEventArgs e)
        {
            if (ScriptDialogOpening != null)
            {
                ScriptDialogOpening(this, e);
            }
        }

        protected virtual void OnPermissionRequested(PermissionRequestedEventArgs e)
        {
            if (PermissionRequested != null)
            {
                PermissionRequested(this, e);
            }
        }

        /// <summary>
        /// Raises the 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProcessFailed(ProcessFailedEventArgs e)
        {
            // Communication with the underlying webView2 is broken
            _webView2WebView = null;
            _handlersRegistered = false;

            if (ProcessFailed != null)
            {
                ProcessFailed(this, e);
            }
        }

        protected virtual void OnDocumentTitleChanged(DocumentTitleChangedEventArgs e)
        {
            if (DocumentTitleChanged != null)
            {
                DocumentTitleChanged(this, e);
            }
        }

        protected virtual void OnNewWindowRequested(NewWindowRequestedEventArgs e)
        {
            if (NewWindowRequested != null)
            {
                NewWindowRequested(this, e);
            }
        }

        protected virtual void OnAcceleratorKeyPressed(AcceleratorKeyPressedEventArgs e)
        {
            if (AcceleratorKeyPressed != null)
            {
                AcceleratorKeyPressed(this, e);
            }
        }

        protected virtual void OnContainsFullScreenElementChanged(ContainsFullScreenElementChangedEventArgs e)
        {
            if (ContainsFullScreenElementChanged != null)
            {
                ContainsFullScreenElementChanged(this, e);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Raises the <see cref="WebView2EnvironmentCreated"/ event.
        /// </summary>
        /// <param name="args"></param>
        private void OnWebView2EnvironmentCreated(EnvironmentCreatedEventArgs args)
        {
            _webViewEnvironment = args.WebViewEnvironment;

            OnEnvironmentCreated(args);

            _webViewEnvironment.CreateWebView(_parentWindow, OnWebViewCreated);
        }

        private void OnWebViewCreated(CreateWebViewCompletedEventArgs args)
        {
            int result = args.Result;
            WebView2WebView webView = args.WebView;

            if (webView != null)
            {
                _webView2WebView = webView;

                RegisterHandlers();

                AreDevToolsEnabled = _initialAreDevToolsEnabled;
                ZoomFactor = _initialZoomFactor;

                UpdateWindowPos();

                OnBrowserCreated(EventArgs.Empty);

                if (!string.IsNullOrEmpty(_internalUrl))
                {
                    _webView2WebView.Navigate(_internalUrl);
                }
            }
        }

        private IDictionary<HandlerType, long> _handlerTokenDictionary = new Dictionary<HandlerType, long>();
        private IDictionary<string, long> _devToolsProtocolEventTokenDictionary = new Dictionary<string, long>();
        private bool _handlersRegistered = false;

        private void RegisterHandlers()
        {
            _handlerTokenDictionary.Add(HandlerType.NavigationComplete, _webView2WebView.RegisterNavigationCompleted(OnNavigationCompleted));
            _handlerTokenDictionary.Add(HandlerType.NavigationStarting, _webView2WebView.RegisterNavigationStarting(OnNavigationStarting));
            _handlerTokenDictionary.Add(HandlerType.ZoomFactorChanged, _webView2WebView.RegisterZoomFactorChanged(OnZoomFactorChanged));
            _handlerTokenDictionary.Add(HandlerType.WebMessageReceived, _webView2WebView.RegisterWebMessageReceived(OnWebMessageRecieved));
            _handlerTokenDictionary.Add(HandlerType.DocumentStateChanged, _webView2WebView.RegisterDocumentStateChanged(OnDocumentStateChanged));
            _handlerTokenDictionary.Add(HandlerType.LostFocus, _webView2WebView.RegisterLostFocus(OnBrowserLostFocus));
            _handlerTokenDictionary.Add(HandlerType.FrameNavigationStarting, _webView2WebView.RegisterFrameNavigationStarting(OnFrameNavigationStarting));
            _handlerTokenDictionary.Add(HandlerType.MoveFocusRequested, _webView2WebView.RegisterMoveFocusRequested(OnMoveFocusRequested));
            _handlerTokenDictionary.Add(HandlerType.GotFocus, _webView2WebView.RegisterGotFocus(OnBrowserGotFocus));
            _handlerTokenDictionary.Add(HandlerType.WebResourceRequested, _webView2WebView.RegisterWebResourceRequested(OnWebResourceRequested));
            _handlerTokenDictionary.Add(HandlerType.ScriptDialogOpening, _webView2WebView.RegisterScriptDialogOpening(OnScriptDialogOpening));
            _handlerTokenDictionary.Add(HandlerType.PermissionRequested, _webView2WebView.RegisterPermissionRequested(OnPermissionRequested));
            _handlerTokenDictionary.Add(HandlerType.ProcessFailed, _webView2WebView.RegisterProcessFailed(OnProcessFailed));
            _handlerTokenDictionary.Add(HandlerType.TitleChanged, _webView2WebView.RegisterDocumentTitledChanged(OnDocumentTitleChanged));
            _handlerTokenDictionary.Add(HandlerType.NewWindow, _webView2WebView.RegisterNewWindowRequested(OnNewWindowRequested));
            _handlerTokenDictionary.Add(HandlerType.AcceleratorKeyPressed, _webView2WebView.RegisterAcceleratorKeyPressed(OnAcceleratorKeyPressed));
            _handlerTokenDictionary.Add(HandlerType.FullScreenElement, _webView2WebView.RegisterContainsFullScreenElementChanged(OnContainsFullScreenElementChanged));
            _handlersRegistered = true;
        }

        private void UnregisterHandlers()
        {
            if (!_handlersRegistered)
                return;
            _handlersRegistered = false;
            foreach (long token in _devToolsProtocolEventTokenDictionary.Values)
            {
                _webView2WebView.UnregisterDevToolsProtocolEventReceived(token);
            }
            _webView2WebView.UnregisterNavigationCompleted(_handlerTokenDictionary[HandlerType.NavigationComplete]);
            _webView2WebView.UnregisterNavigationStarting(_handlerTokenDictionary[HandlerType.NavigationStarting]);
            _webView2WebView.UnregisterZoomFactorChanged(_handlerTokenDictionary[HandlerType.ZoomFactorChanged]);
            _webView2WebView.UnregisterWebMessageReceived(_handlerTokenDictionary[HandlerType.WebMessageReceived]);
            _webView2WebView.UnregisterDocumentStateChanged(_handlerTokenDictionary[HandlerType.DocumentStateChanged]);
            _webView2WebView.UnregisterLostFocus(_handlerTokenDictionary[HandlerType.LostFocus]);
            _webView2WebView.UnregisterFrameNavigationStarting(_handlerTokenDictionary[HandlerType.FrameNavigationStarting]);
            _webView2WebView.UnregisterMoveFocusRequested(_handlerTokenDictionary[HandlerType.MoveFocusRequested]);
            _webView2WebView.UnregisterGotFocus(_handlerTokenDictionary[HandlerType.GotFocus]);
            _webView2WebView.UnregisterWebResourceRequested(_handlerTokenDictionary[HandlerType.WebResourceRequested]);
            _webView2WebView.UnregisterScriptDialogOpening(_handlerTokenDictionary[HandlerType.ScriptDialogOpening]);
            _webView2WebView.UnregisterPermissionRequested(_handlerTokenDictionary[HandlerType.PermissionRequested]);
            _webView2WebView.UnregisterProcessFailed(_handlerTokenDictionary[HandlerType.ProcessFailed]);
            _webView2WebView.UnregisterDocumentTitledChanged(_handlerTokenDictionary[HandlerType.TitleChanged]);
            _webView2WebView.UnregisterNewWindowRequested(_handlerTokenDictionary[HandlerType.NewWindow]);
            _webView2WebView.UnregisterAcceleratorKeyPressed(_handlerTokenDictionary[HandlerType.AcceleratorKeyPressed]);
            _webView2WebView.UnRegisterContainsFullScreenElementChanged(_handlerTokenDictionary[HandlerType.FullScreenElement]);
        }


        private void OnBrowserLostFocus(FocusChangedEventEventArgs e)
        {
            _hasFocus = false;
            //RoutedEventArgs args = new RoutedEventArgs();
            //base.OnLostFocus(args);
            RaiseEvent(new RoutedEventArgs(LostFocusEvent, this));
        }

        private void OnBrowserGotFocus(FocusChangedEventEventArgs e)
        {
            _hasFocus = true;
        }

        private void InternalNavigate(string url)
        {
            _internalUrl = url;
            if (_webView2WebView == null) return;
            _webView2WebView.Navigate(url);
        }

        private string InternalUrl
        {
            get
            {
                if (_webView2WebView == null)
                    return _internalUrl;
                return _webView2WebView.Source;
            }
        }

        public bool MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON reason)
        {
            if (_webView2WebView == null)
                return false;
            _webView2WebView.MoveFocus(reason);
            return true;
        }

        #endregion

        private bool _isVisible;

        protected void AsyncBuildWindowCore(IntPtr hwndParent)
        {
            _parentWindow = hwndParent;

            bool isInDesignMode = DesignerProperties.GetIsInDesignMode(this);

            if (!isInDesignMode)
            {
                if (_webViewEnvironment == null)
                {
                    BeforeEnvironmentCreatedEventArgs eventArgs = new BeforeEnvironmentCreatedEventArgs();
                    OnBeforeEnvironmentCreated(eventArgs);

                    string browserExecutableFolder = eventArgs.BrowserExecutableFolder ?? string.Empty;
                    string userDataFolder = eventArgs.UserDataFolder ?? string.Empty;
                    string browserArguments = eventArgs.BrowserArguments ?? string.Empty;
                    WebView2Loader.CreateEnvironmentWithDetails(browserExecutableFolder, userDataFolder, browserArguments, OnWebView2EnvironmentCreated);
                }
                else
                {
                    _webViewEnvironment.CreateWebView(hwndParent, OnWebViewCreated);
                }
            }
        }

        protected override bool HasFocusWithinCore()
        {
            return _hasFocus;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Focus();
            base.OnGotFocus(e);
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            _webView2Host = new BrowserParentWindow(hwndParent.Handle);

            AsyncBuildWindowCore(_webView2Host.Handle);

            return new HandleRef(this, _webView2Host.Handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            UnregisterHandlers();
            try
            {
                if (_webView2WebView != null)
                {
                    _webView2WebView.IsVisible = false;
                    _webView2WebView.Close();
                    _webView2WebView = null;
                }
            }
            catch(Exception)
            {
            }

            try
            {
                if (_webView2Host != null)
                {
                    _webView2Host.DestroyHandle();
                    _webView2Host = null;
                }
            }
            catch (Exception)
            {
            }
        }
    }

    /// <summary>
    /// HwndHost wants a HWND when getting created but the WebView2 doesn't expose one.   So use this window as the parent
    /// for the WebView2 that we can return to HwndHost
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class BrowserParentWindow : WinForms.NativeWindow
    {
        // Constant values were found in the "windows.h" header file.
        private const int WS_CHILD = 0x40000000,
                          WS_VISIBLE = 0x10000000,
                          WM_ACTIVATEAPP = 0x001C,
                          WM_SETFOCUS = 0x0007,
                          WM_KILLFOCUS = 0x0008;

        private int windowHandle;

        public BrowserParentWindow(IntPtr parent)
        {

            WinForms.CreateParams cp = new WinForms.CreateParams();

            // Fill in the CreateParams details.
            cp.Caption = "WebView2ControlHost";
            //cp.ClassName = "WebView2ControlHost";

            // Specify the form as the parent.
            cp.Parent = parent;

            // Create as a child of the specified parent
            cp.Style = WS_CHILD | WS_VISIBLE;

            // Create the actual window
            this.CreateHandle(cp);
        }

        // Listen to when the handle changes to keep the variable in sync
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void OnHandleChange()
        {
            windowHandle = (int)this.Handle;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref WinForms.Message m)
        {
            // Listen for messages that are sent to the button window. Some messages are sent
            // to the parent window instead of the button's window.

            switch (m.Msg)
            {
            }
            base.WndProc(ref m);
        }
    }
}
