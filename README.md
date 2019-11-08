# webview2-control
.net controls for the Edge WebView2 

|WebView2 Native|WebVeiw2Control|Tested|
|---|---|---|
|get_Settings([out, retval] IWebView2Settings** settings)|   |   |
|get_Source([out, retval] LPWSTR* uri)|   |   |
|Navigate([in] LPCWSTR uri)|   |   |
|MoveFocus([in] WEBVIEW2_MOVE_FOCUS_REASON reason)|  |  |
|NavigateToString([in] LPCWSTR htmlContent)|   |   |
|add/remove NavigationStarting|  |  |
|add/remove DocumentStateChanged|  |  |
|add/remove NavigationCompleted|  |  |
|add/remove FrameNavigationStarting|  |  |
|add/remove MoveFocusRequested|  |  |
|add/remove GotFocus|  |  |
|add/remove LostFocus|  |  |
|add/remove WebResourceRequested|  |  |
|add/remove ScriptDialogOpening|  |  |
|add/remove ZoomFactorChanged|  |  |
|add/remove PermissionRequested|  |  |
|add/remove ProcessFailed|  |  |
|AddScriptToExecuteOnDocumentCreated|  |  |
|RemoveScriptToExecuteOnDocumentCreated|  |  |
|ExecuteScript|  |  |
|CapturePreview|  |  |
|Reload|  |  |
|get_Bounds|  |  |
|put_Bounds|  |  |
|get_ZoomFactor|  |  |
|put_ZoomFactor|  |  |
|get_IsVisible|  |  |
|put_IsVisible|  |  |
|PostWebMessageAsJson|  |  |
|PostWebMessageAsString|  |  |
|add_WebMessageReceived|  |  |
|remove_WebMessageReceived|  |  |
|Close|  |  |
|CallDevToolsProtocolMethod|  |  |
|add_DevToolsProtocolEventReceived|  |  |
|remove_DevToolsProtocolEventReceived|  |  |
|get_BrowserProcessId|  |  |
|get_CanGoBack|  |  |
|get_CanGoForward|  |  |
|GoBack|  |  |
|GoForward|  |  |
