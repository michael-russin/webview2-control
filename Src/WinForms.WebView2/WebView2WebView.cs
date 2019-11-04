using System;
using System.Collections.Generic;
using System.Drawing;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms.Handlers;

namespace Russinsoft.WinForms
{
    public class WebView2WebView : IWebView2WebView3
    {
        private IWebView2WebView3 _webview;

        internal WebView2WebView(IWebView2WebView webview)
        {
            _webview = (IWebView2WebView3)webview;
        }

        internal IWebView2WebView InternalWebView
        {
            get { return _webview; }
        }

        #region IWebView2WebView
        public WebView2Settings Settings
        {
            get
            {
                return new WebView2Settings(_webview.Settings);
            }
        }

        public string Source
        {
            get
            {
                return _webview.Source;
            }
        }


        public void Navigate(string uri)
        {
            _webview.Navigate(uri);
        }


        public void MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON reason)
        {
            _webview.MoveFocus(reason);
        }


        public void NavigateToString(string htmlContent)
        {
            _webview.NavigateToString(htmlContent);
        }

        #endregion

        public Rectangle Bounds
        {
            get
            {
                tagRECT rect = _webview.Bounds;
                return new Rectangle(rect.left, rect.top, (rect.right - rect.left), (rect.bottom - rect.top));
            }
            set
            {
                tagRECT rect = new tagRECT();
                rect.top = value.Top;
                rect.left = value.Left;
                rect.right = value.Right;
                rect.bottom = value.Bottom;
                _webview.Bounds = rect;
            }
        }

        public uint BrowserProcessId
        {
            get
            {
                return _webview.BrowserProcessId;
            }
        }

        public bool CanGoBack
        {
            get => _webview.CanGoBack;
        }

        public bool CanGoForward
        {
            get => _webview.CanGoForward;
        }

        public bool IsVisible
        {
            get => _webview.IsVisible;
            set => _webview.IsVisible = value;
        }



        public double ZoomFactor
        {
            get
            {
                return _webview.ZoomFactor;
            }
            set
            {
                _webview.ZoomFactor = value;
            }
        }

        public void AddScriptToExecuteOnDocumentCreated(string javaScript, Action<AddScriptToExecuteOnDocumentCreatedCompletedEventArgs> callback)
        {
            AddScriptToExecuteOnDocumentCreatedCompletedHandler completedHandler = new AddScriptToExecuteOnDocumentCreatedCompletedHandler(callback);

            _webview.AddScriptToExecuteOnDocumentCreated(javaScript, completedHandler);
        }
        public void RemoveScriptToExecuteOnDocumentCreated(string id)
        {
            _webview.RemoveScriptToExecuteOnDocumentCreated(id);
        }

        private IDictionary<long, DevToolProtocol> _devToolsProtocolCallbacks =
            new Dictionary<long, DevToolProtocol>();

        public long RegisterDevToolsProtocolEventReceived(string eventName, Action<DevToolsProtocolEventReceivedEventArgs> callback)
        {
            DevToolsProtocolEventReceivedEventHandler completedHandler = new DevToolsProtocolEventReceivedEventHandler(eventName, callback);

            EventRegistrationToken token;
            _webview.add_DevToolsProtocolEventReceived(eventName, completedHandler, out token);
            DevToolProtocol devToolProtocol = new DevToolProtocol(eventName, callback);
            _devToolsProtocolCallbacks.Add(token.value, devToolProtocol);
            return token.value;
        }

        public void UnregisterDevToolsProtocolEventReceived(long token)
        {
            DevToolProtocol devToolProtocol = _devToolsProtocolCallbacks[token];
            _devToolsProtocolCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_DevToolsProtocolEventReceived(devToolProtocol.EventName, registrationToken);
        }

        private IDictionary<long, Action<DocumentStateChangedEventArgs>> _documentStateChangedCallbacks =
            new Dictionary<long, Action<DocumentStateChangedEventArgs>>();

        public long RegisterDocumentStateChanged(Action<DocumentStateChangedEventArgs> callback)
        {
            WebView2DocumentStateChangedEventHandler completedHandler = new WebView2DocumentStateChangedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_DocumentStateChanged(completedHandler, out token);
            _documentStateChangedCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterDocumentStateChanged(long token)
        {
            _devToolsProtocolCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_DocumentStateChanged(registrationToken);
        }

        private IDictionary<long, Action<NavigationStartingEventArgs>> _frameNavigationStartingCallbacks =
            new Dictionary<long, Action<NavigationStartingEventArgs>>();

        public long RegisterFrameNavigationStarting(Action<NavigationStartingEventArgs> callback)
        {
            NavigationStartingEventHandler completedHandler = new NavigationStartingEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_FrameNavigationStarting(completedHandler, out token);
            _frameNavigationStartingCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterFrameNavigationStarting(long token)
        {
            _frameNavigationStartingCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_FrameNavigationStarting(registrationToken);
        }

        private IDictionary<long, Action<FocusChangedEventEventArgs>> _gotFocusCallbacks =
            new Dictionary<long, Action<FocusChangedEventEventArgs>>();

        public long RegisterGotFocusdEvent(Action<FocusChangedEventEventArgs> callback)
        {
            WebView2FocusChangedEventHandler completedHandler = new WebView2FocusChangedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_GotFocus(completedHandler, out token);
            _gotFocusCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterGotFocusEvent(long token)
        {
            _frameNavigationStartingCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_GotFocus(registrationToken);
        }

        private IDictionary<long, Action<FocusChangedEventEventArgs>> _lostFocusEventDictionary =
            new Dictionary<long, Action<FocusChangedEventEventArgs>>();

        public long RegisterLostFocus(Action<FocusChangedEventEventArgs> callback)
        {
            WebView2FocusChangedEventHandler completedHandler = new WebView2FocusChangedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_LostFocus(completedHandler, out token);
            _lostFocusEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterLostFocus(long token)
        {
            if (_lostFocusEventDictionary.ContainsKey(token))
            {
                _lostFocusEventDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_LostFocus(registrationToken);
        }

        private IDictionary<long, Action<MoveFocusRequestedEventArgs>> _moveFocusRequestedDictionary =
            new Dictionary<long, Action<MoveFocusRequestedEventArgs>>();

        public long RegisterMoveFocusRequested(Action<MoveFocusRequestedEventArgs> callback)
        {
            MoveFocusRequestedEventHandler completedHandler = new MoveFocusRequestedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_MoveFocusRequested(completedHandler, out token);
            _moveFocusRequestedDictionary.Add(token.value, callback);
            return token.value;
        }
        public void UnregisterMoveFocusRequested(long token)
        {
            if (_moveFocusRequestedDictionary.ContainsKey(token))
            {
                _moveFocusRequestedDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_MoveFocusRequested(registrationToken);
        }

        private IDictionary<long, Action<NavigationCompletedEventArgs>> _navigationCompletedEventDictionary = 
            new Dictionary<long, Action<NavigationCompletedEventArgs>>();

        public long RegisterNavigationCompleted(Action<NavigationCompletedEventArgs> callback)
        {
            WebView2NavigationCompletedEventHandler completedHandler = new WebView2NavigationCompletedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_NavigationCompleted(completedHandler, out token);
            _navigationCompletedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterNavigationCompleted(long token)
        {
            if (_navigationCompletedEventDictionary.ContainsKey(token))
            {
                _navigationCompletedEventDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_NavigationCompleted(registrationToken);
        }

        private IDictionary<long, Action<NavigationStartingEventArgs>> _navigationStartingEventDictionary =
            new Dictionary<long, Action<NavigationStartingEventArgs>>();

        public long RegisterNavigationStarting(Action<NavigationStartingEventArgs> callback)
        {
            NavigationStartingEventHandler completedHandler = new NavigationStartingEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_NavigationStarting(completedHandler, out token);
            _navigationStartingEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterNavigationStarting(long token)
        {
            if (_navigationStartingEventDictionary.ContainsKey(token))
            {
                _navigationStartingEventDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_NavigationStarting(registrationToken);
        }

        private IDictionary<long, Action<PermissionRequestedEventArgs>> _permissionRequestedCallbacks =
            new Dictionary<long, Action<PermissionRequestedEventArgs>>();

        public long RegisterPermissionRequested(Action<PermissionRequestedEventArgs> callback)
        {
            PermissionRequestedEventHandler completedHandler = new PermissionRequestedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_PermissionRequested(completedHandler, out token);
            _permissionRequestedCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterPermissionRequested(long token)
        {
            _permissionRequestedCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_PermissionRequested(registrationToken);
        }

        private IDictionary<long, Action<ProcessFailedEventArgs>> _processFailedCallbacks =
            new Dictionary<long, Action<ProcessFailedEventArgs>>();

        public long RegisterProcessFailed(Action<ProcessFailedEventArgs> callback)
        {
            ProcessFailedEventHandler completedHandler = new ProcessFailedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_ProcessFailed(completedHandler, out token);
            _processFailedCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterProcessFailed(long token)
        {
            _processFailedCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_ProcessFailed(registrationToken);
        }

        private IDictionary<long, Action<ScriptDialogOpeningEventArgs>> _scriptDialogOpeningCallbacks =
            new Dictionary<long, Action<ScriptDialogOpeningEventArgs>>();

        public long RegisterScriptDialogOpening(Action<ScriptDialogOpeningEventArgs> callback)
        {
            ScriptDialogOpeningEventHandler completedHandler = new ScriptDialogOpeningEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_ScriptDialogOpening(completedHandler, out token);
            _scriptDialogOpeningCallbacks.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterScriptDialogOpening(long token)
        {
            _scriptDialogOpeningCallbacks.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_ScriptDialogOpening(registrationToken);
        }

        private IDictionary<long, Action<WebMessageReceivedEventArgs>> _webMessageReceivedEventDictionary =
            new Dictionary<long, Action<WebMessageReceivedEventArgs>>();

        public long RegisterWebMessageReceived(Action<WebMessageReceivedEventArgs> callback)
        {
            WebMessageReceivedEventHandler completedHandler = new WebMessageReceivedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_WebMessageReceived(completedHandler, out token);
            _webMessageReceivedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterWebMessageReceived(long token)
        {
            if (_webMessageReceivedEventDictionary.ContainsKey(token))
            {
                _webMessageReceivedEventDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_WebMessageReceived(registrationToken);
        }

        private IDictionary<long, Action<WebResourceRequestedEventArgs>> _webResourceRequestedEventDictionary =
            new Dictionary<long, Action<WebResourceRequestedEventArgs>>();

        public long RegisterWebResourceRequested(ref string urlFilter, ref WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContextFilter, Action<WebResourceRequestedEventArgs> callback)
        {
            WebResourceRequestedEventHandler completedHandler = new WebResourceRequestedEventHandler(callback);

            EventRegistrationToken token;
            uint filterLength = (uint)urlFilter.Length;
            _webview.add_WebResourceRequested(ref urlFilter, ref resourceContextFilter, filterLength, completedHandler, out token);
            _webResourceRequestedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterWebResourceRequested(long token)
        {
            _webResourceRequestedEventDictionary.Remove(token);
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_WebResourceRequested(registrationToken);
        }

        private IDictionary<long, Action<ZoomFactorCompletedEventArgs>> _zoomFactorChangedEventDictionary =
            new Dictionary<long, Action<ZoomFactorCompletedEventArgs>>();

        public long RegisterZoomFactorChanged(Action<ZoomFactorCompletedEventArgs> callback)
        {
            ZoomFactorChangedEventHandler completedHandler = new ZoomFactorChangedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_ZoomFactorChanged(completedHandler, out token);
            _zoomFactorChangedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterZoomFactorChanged(long token)
        {
            if (_zoomFactorChangedEventDictionary.ContainsKey(token))
            {
                _zoomFactorChangedEventDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;
            _webview.remove_ZoomFactorChanged(registrationToken);
        }

        public void CallDevToolsProtocolMethod(string methodName, string parametersAsJson)
        {
            _webview.CallDevToolsProtocolMethod(methodName, parametersAsJson, null);
        }

        public void CallDevToolsProtocolMethod(string methodName, string parametersAsJson, Action<CallDevToolsProtocolMethodCompletedEventArgs> callback)
        {
            CallDevToolsProtocolMethodCompletedHandler callbackHandler = null;
            if (callback != null)
            {
                callbackHandler = new CallDevToolsProtocolMethodCompletedHandler(callback);
            }
            _webview.CallDevToolsProtocolMethod(methodName, parametersAsJson, callbackHandler);
        }

        void CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat, IStream imageStream, IWebView2CapturePreviewCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            _webview.Close();
        }

        public void ExecuteScript(string javaScript, Action<ExecuteScriptCompletedEventArgs> callback)
        {
            ExecuteScriptCompletedHandler callbackHandler = null;
            if (callback != null)
            {
                callbackHandler = new ExecuteScriptCompletedHandler(callback);
            }
            _webview.ExecuteScript(javaScript, callbackHandler);
        }

        public void GoBack()
        {
            _webview.GoBack();
        }

        public void GoForward()
        {
            _webview.GoForward();
        }

        public void PostWebMessageAsJson(string webMessageAsJson)
        {
            _webview.PostWebMessageAsJson(webMessageAsJson);
        }

        public void PostWebMessageAsString(string webMessageAsString)
        {
            _webview.PostWebMessageAsString(webMessageAsString);
        }

        public void Reload()
        {
            _webview.Reload();
        }

        #region IWebView2WebView2
        public void Stop()
        {
            _webview.Stop();
        }
        #endregion

        #region IWebView2WebView3

        private IDictionary<long, Action<NewWindowRequestedEventArgs>> _newWindowRequestedEventDictionary =
            new Dictionary<long, Action<NewWindowRequestedEventArgs>>();

        /// <summary>
        /// Add an event handler for the NewWindowRequested event.
        /// Fires when content inside the WebView requested to open a new window,
        /// such as through window.open. The app can pass a target
        /// webview that will be considered the opened window. 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public long RegisterNewWindowRequested(Action<NewWindowRequestedEventArgs> callback)
        {
            NewWindowRequestedEventHandler completedHandler = new NewWindowRequestedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_NewWindowRequested(completedHandler, out token);
            _newWindowRequestedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterNewWindowRequested(long token)
        {
            _newWindowRequestedEventDictionary.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_NewWindowRequested(registrationToken);
        }

        private IDictionary<long, Action<DocumentTitleChangedEventArgs>> _titleChangedEventDictionary =
            new Dictionary<long, Action<DocumentTitleChangedEventArgs>>();

        public long RegisterDocumentTitledChanged(Action<DocumentTitleChangedEventArgs> callback)
        {
            DocumentTitleChangedEventHandler completedHandler = new DocumentTitleChangedEventHandler(callback);

            EventRegistrationToken token;
            _webview.add_DocumentTitleChanged(completedHandler, out token);
            _titleChangedEventDictionary.Add(token.value, callback);
            return token.value;
        }

        public void UnregisterDocumentTitledChanged(long token)
        {
            _titleChangedEventDictionary.Remove(token);

            EventRegistrationToken registrationToken = new EventRegistrationToken()
            {
                value = token
            };
            _webview.remove_DocumentTitleChanged(registrationToken);
        }


        /// The title for the current top level document.
        /// If the document has no explicit title or is otherwise empty, then the URI
        /// of the top level document is used.
        public string DocumentTitle
        {
            get
            {
                string documentTitle;
                _webview.DocumentTitle(out documentTitle);
                return documentTitle;
            }
        }

        public void add_NavigationStarting(IWebView2NavigationStartingEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_NavigationStarting(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_DocumentStateChanged(IWebView2DocumentStateChangedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_DocumentStateChanged(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_NavigationCompleted(IWebView2NavigationCompletedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_NavigationCompleted(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_FrameNavigationStarting(IWebView2NavigationStartingEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_FrameNavigationStarting(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_MoveFocusRequested(IWebView2MoveFocusRequestedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_MoveFocusRequested(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_GotFocus(IWebView2FocusChangedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_GotFocus(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_LostFocus(IWebView2FocusChangedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_LostFocus(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_WebResourceRequested(ref string urlFilter, ref WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContextFilter, ulong filterLength, IWebView2WebResourceRequestedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_WebResourceRequested(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_ScriptDialogOpening(IWebView2ScriptDialogOpeningEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_ScriptDialogOpening(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_ZoomFactorChanged(IWebView2ZoomFactorChangedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_ZoomFactorChanged(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_PermissionRequested(IWebView2PermissionRequestedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_PermissionRequested(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_ProcessFailed(IWebView2ProcessFailedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_ProcessFailed(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        void IWebView2WebView3.AddScriptToExecuteOnDocumentCreated(string javaScript, IWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        public void ExecuteScript(string javaScript, IWebView2ExecuteScriptCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        void IWebView2WebView3.CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat, IStream imageStream, IWebView2CapturePreviewCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        public void add_WebMessageReceived(IWebView2WebMessageReceivedEventHandler handler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_WebMessageReceived(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_DevToolsProtocolEventReceived(string eventName, IWebView2DevToolsProtocolEventReceivedEventHandler handler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_DevToolsProtocolEventReceived(string eventName, EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_NewWindowRequested(IWebView2NewWindowRequestedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_NewWindowRequested(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void add_DocumentTitleChanged(IWebView2DocumentTitleChangedEventHandler eventHandler, out EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        public void remove_DocumentTitleChanged(EventRegistrationToken token)
        {
            throw new NotImplementedException();
        }

        void IWebView2WebView3.DocumentTitle(out string title)
        {
            throw new NotImplementedException();
        }

        public void AddRemoteObject(string name, ref object @object)
        {
            throw new NotImplementedException();
        }

        public void RemoveRemoteObject(string name)
        {
            throw new NotImplementedException();
        }

        public void OpenDevToolsWindow()
        {
            throw new NotImplementedException();
        }

        IWebView2Settings IWebView2WebView3.Settings => throw new NotImplementedException();

        tagRECT IWebView2WebView3.Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IWebView2WebView.AddScriptToExecuteOnDocumentCreated(string javaScript, IWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        void IWebView2WebView.CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat, IStream imageStream, IWebView2CapturePreviewCompletedHandler handler)
        {
            throw new NotImplementedException();
        }

        IWebView2Settings IWebView2WebView.Settings => throw new NotImplementedException();

        tagRECT IWebView2WebView.Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IWebView2WebView.CallDevToolsProtocolMethod(string methodName, string parametersAsJson, IWebView2CallDevToolsProtocolMethodCompletedHandler handler)
        {
        }
        void IWebView2WebView3.CallDevToolsProtocolMethod(string methodName, string parametersAsJson, IWebView2CallDevToolsProtocolMethodCompletedHandler handler)
        {
        }
        #endregion
    }
}
