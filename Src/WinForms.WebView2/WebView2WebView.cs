#region License
// Copyright (c) 2019 Michael T. Russin
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using MtrDev.WebView2.Interop;
using MtrDev.WinForms.Handlers;

namespace MtrDev.WinForms
{
    public class WebView2WebView 
    {
        private IWebView2WebView4 _webview;

        internal WebView2WebView(IWebView2WebView webview)
        {
            _webview = (IWebView2WebView4)webview;
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


        /// <summary>
        /// The title for the current top level document.
        /// If the document has no explicit title or is otherwise empty, then the URI
        /// of the top level document is used.
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                string documentTitle;
                _webview.DocumentTitle(out documentTitle);
                return documentTitle;
            }
        }
        #endregion

        #region IWebView2WebView4
        // HRESULT AddRemoteObject([in] LPCWSTR name, [in] VARIANT* object);

        /// Remove the host object specified by the name so that it is no longer
        /// accessible from JavaScript code in the WebView.
        /// While new access attempts will be denied, if the object is already
        /// obtained by JavaScript code in the WebView, the JavaScript code will
        /// continue to have access to that object.
        /// Calling this method for a name that is already removed or never added will
        /// fail.
        //HRESULT RemoveRemoteObject([in] LPCWSTR name);

        /// Opens the DevTools window for the current document in the WebView.
        /// Does nothing if called when the DevTools window is already open
        public void OpenDevToolsWindow()
        {
            _webview.OpenDevToolsWindow();
        }


  /// Add an event handler for the AcceleratorKeyPressed event.
  /// AcceleratorKeyPressed fires when an accelerator key or key combo is
  /// pressed or released while the WebView is focused. A key is considered an
  /// accelerator if either:
  ///   1. Ctrl or Alt is currently being held, or
  ///   2. the pressed key does not map to a character.
  /// A few specific keys are never considered accelerators, such as Shift.
  /// The Escape key is always considered an accelerator.
  ///
  /// Autorepeated key events caused by holding the key down will also fire this
  /// event.  You can filter these out by checking the event args'
  /// KeyEventLParam or PhysicalKeyStatus.
  ///
  /// In windowed mode, this event handler is called synchronously. Until you
  /// call Handle() on the event args or the event handler returns, the browser
  /// process will be blocked and outgoing cross-process COM calls will fail
  /// with RPC_E_CANTCALLOUT_ININPUTSYNCCALL. All WebView2 API methods will
  /// work, however.
  ///
  /// In windowless mode, the event handler is called asynchronously.  Further
  /// input will not reach the browser until the event handler returns or
  /// Handle() is called, but the browser process itself will not be blocked,
  /// and outgoing COM calls will work normally.
  ///
  /// It is recommended to call Handle(TRUE) as early as you can know that you want
  /// to handle the accelerator key.
  ///
  /// \snippet AppWindow.cpp AcceleratorKeyPressed
  //  HRESULT add_AcceleratorKeyPressed([in] IWebView2AcceleratorKeyPressedEventHandler* eventHandler, [out] EventRegistrationToken* token);

        /// Remove an event handler previously added with add_AcceleratorKeyPressed.
  //      HRESULT remove_AcceleratorKeyPressed([in] EventRegistrationToken token);
        #endregion
    }
}
