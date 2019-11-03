using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Russinsoft.WebView2.Interop;
using WebView2Sharp.Events;

namespace WebView2Sharp
{
    public class WebView2ControlBase : Control
    {
        private WebViewEnvironment _webViewEnvironment;
        private WebView2WebView _webView2WebView;

        public WebView2ControlBase()
        {
        }

        public WebView2ControlBase(WebViewEnvironment webViewEnvironment)
        {
            _webViewEnvironment = webViewEnvironment;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            if (!DesignMode)
            {
                if (_webViewEnvironment == null)
                {
                    BeforeEnvironmentCreatedEventArgs eventArgs = new BeforeEnvironmentCreatedEventArgs();
                    OnBeforeEnvironmentCreated(eventArgs);

                    string browserExecutableFolder = eventArgs.BrowserExecutableFolder ?? string.Empty;
                    string userDataFolder = eventArgs.UserDataFolder ?? string.Empty;
                    string browserArguments = eventArgs.BrowserArguments ?? string.Empty;

                    //WebView2Loader.CreateEnvironmentWithDetails(string.Empty, string.Empty, string.Empty, OnWebView2EnvironmentCreated);
                    WebView2Loader.CreateEnvironmentWithDetails(browserExecutableFolder, userDataFolder, browserArguments, OnWebView2EnvironmentCreated);
                }
                else
                {
                    _webViewEnvironment.CreateWebView(Handle, OnWebViewCreated);
                }
            }
        }

        private void OnWebView2EnvironmentCreated(EnvironmentCreatedEventArgs args)
        {
            _webViewEnvironment = args.WebViewEnvironment;

            OnEnvironmentCreated(args);

            _webViewEnvironment.CreateWebView(Handle, OnWebViewCreated);
        }

        private void OnWebViewCreated(CreateWebViewCompletedEventArgs args)
        {
            int result = args.Result;
            WebView2WebView webView = args.WebView;

            if (webView != null)
            {
                _webView2WebView = webView;
            }

            RegisterHandlers();

            // Resize WebView to fit the bounds of the parent window
            _webView2WebView.Bounds = new Rectangle(new Point(0, 0), Size);

            // Set any properties that we have cached because the browser wasn't created yet
            AreDevToolsEnabled = _initialAreDevToolsEnabled;
            IsFullscreenAllowed = _initialIsFullscreenAllowed;
            ZoomFactor = _initialZoomFactor;

            OnBrowserCreated(EventArgs.Empty);

            if (!string.IsNullOrEmpty(_internalUrl))
            {
                // Schedule an async task to navigate to Bing
                _webView2WebView.Navigate(_internalUrl);
            }

        }

        protected override void DestroyHandle()
        {
            UnregisterHandlers();
            base.DestroyHandle();
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
            _handlerTokenDictionary.Add(HandlerType.GotFocus, _webView2WebView.RegisterGotFocusdEvent(OnGotFocus));
            _handlerTokenDictionary.Add(HandlerType.ScriptDialogOpening, _webView2WebView.RegisterScriptDialogOpening(OnScriptDialogOpening));
            _handlerTokenDictionary.Add(HandlerType.PermissionRequested, _webView2WebView.RegisterPermissionRequested(OnPermissionRequested));
            _handlerTokenDictionary.Add(HandlerType.ProcessFailed, _webView2WebView.RegisterProcessFailed(OnProcessFailed));
            _handlersRegistered = true;
        }

        private void UnregisterHandlers()
        {
            if (!_handlersRegistered)
                return;
            foreach(long token in _devToolsProtocolEventTokenDictionary.Values)
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
            _webView2WebView.UnregisterGotFocusEvent(_handlerTokenDictionary[HandlerType.GotFocus]);
            _webView2WebView.UnregisterWebResourceRequested(_handlerTokenDictionary[HandlerType.WebResourceRequested]);
            _webView2WebView.UnregisterScriptDialogOpening(_handlerTokenDictionary[HandlerType.ScriptDialogOpening]);
            _webView2WebView.UnregisterPermissionRequested(_handlerTokenDictionary[HandlerType.PermissionRequested]);
            _webView2WebView.UnregisterProcessFailed(_handlerTokenDictionary[HandlerType.ProcessFailed]);
        }

        protected virtual void OnBeforeEnvironmentCreated(BeforeEnvironmentCreatedEventArgs e)
        {
        }

        protected virtual void OnEnvironmentCreated(EnvironmentCreatedEventArgs e)
        {
        }

        protected virtual void OnBrowserCreated(EventArgs e)
        {
        }

        protected virtual void OnNavigationCompleted(NavigationCompletedEventArgs e)
        {
        }

        protected virtual void OnNavigationStarting(NavigationStartingEventArgs e)
        {
        }

        protected virtual void OnZoomFactorChanged(ZoomFactorCompletedEventArgs e)
        {
        }

        protected virtual void OnWebMessageRecieved(WebMessageReceivedEventArgs e)
        {
        }

        protected virtual void OnDevToolsProtocolEventReceived(DevToolsProtocolEventReceivedEventArgs e)
        {
        }

        protected virtual void OnDocumentStateChanged(DocumentStateChangedEventArgs e)
        {
        }

        private void OnBrowserLostFocus(FocusChangedEventEventArgs e)
        {
            base.OnLostFocus(EventArgs.Empty);
        }

        protected virtual void OnFrameNavigationStarting(NavigationStartingEventArgs e)
        {

        }

        protected virtual void OnMoveFocusRequested(MoveFocusRequestedEventArgs e)
        {

        }

        protected virtual void OnWebResourceRequested(WebResourceRequestedEventArgs e)
        {

        }

        protected virtual void OnScriptDialogOpening(ScriptDialogOpeningEventArgs e)
        {
        }

        protected virtual void OnPermissionRequested(PermissionRequestedEventArgs e)
        {
        }

        protected virtual void OnProcessFailed(ProcessFailedEventArgs e)
        {
        }

        protected void ResizeWebView()
        {
            if (_webView2WebView == null) return;

            // Resize WebView to fit the bounds of the parent window
            _webView2WebView.Bounds = new Rectangle(new Point(0, 0), Size);
        }

        private string _internalUrl;

        protected string InternalUrl
        {
            get
            {
                if (_webView2WebView == null)
                    return _internalUrl;
                return _webView2WebView.Source;
            }
        }

        public void Navigate(string url)
        {
            InternalNavigate(url);
        }

        public void GoForward()
        {
            if (_webView2WebView != null)
            {
                _webView2WebView.GoForward();
            }
        }

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

        public void CallDevToolsProtocolMethod(string methodName, string parametersAsJson)
        {
            _webView2WebView.CallDevToolsProtocolMethod(methodName, parametersAsJson, null);
        }

        public void CallDevToolsProtocolMethod(string methodName, string parametersAsJson, Action<CallDevToolsProtocolMethodCompletedEventArgs> callback)
        {
            _webView2WebView.CallDevToolsProtocolMethod(methodName, parametersAsJson, callback);
        }

        public void StartListeningDevToolsProtocolEvent(string eventName)
        {
            if (_devToolsProtocolEventTokenDictionary.ContainsKey(eventName))
            {
                return;
            }

            long token = _webView2WebView.RegisterDevToolsProtocolEventReceived(eventName, OnDevToolsProtocolEventReceived);
            _devToolsProtocolEventTokenDictionary.Add(eventName, token);
        }

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

        public void ExecuteScript(string javaScript, Action<ExecuteScriptCompletedEventArgs> callback)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.ExecuteScript(javaScript, callback);
        }

        public void MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON reason)
        {
            if (_webView2WebView == null)
                return;
            _webView2WebView.MoveFocus(reason);
        }


        protected void InternalNavigate(string url)
        {
            _internalUrl = url;
            if (_webView2WebView == null) return;
            _webView2WebView.Navigate(url);
        }

        #region Control Properties
        [Browsable(false), 
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanGoBack
        {
            get
            {
                return _webView2WebView.CanGoBack;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanGoForward
        {
            get
            {
                return _webView2WebView.CanGoForward;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        private bool _initialAreDevToolsEnabled = true;

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
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.AreDevToolsEnabled = value;
                }
            }
        }

        private bool _initialIsFullscreenAllowed = true;

        public bool IsFullscreenAllowed
        {
            get
            {
                if (_webView2WebView == null)
                    return _initialIsFullscreenAllowed;
                return _webView2WebView.Settings.IsFullscreenAllowed;
            }
            set
            {
                _initialIsFullscreenAllowed = value;
                if (_webView2WebView != null)
                {
                    _webView2WebView.Settings.IsFullscreenAllowed = value;
                }
            }
        }

        private double _initialZoomFactor = 1.0;

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

        #endregion
    }
}
