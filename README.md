# webview2-control
.net controls for the Edge WebView2 

|WebView2 Native|WebVeiw2Control|Tested|
|---|---|---|
|get_Settings|properties expoesed on control   |   |
|get_Source([out, retval] LPWSTR* uri)|Source property   |   |
|Navigate([in] LPCWSTR uri)|Url property and Navigate method   |   |
|MoveFocus([in] WEBVIEW2_MOVE_FOCUS_REASON reason)|  |  |
|NavigateToString([in] LPCWSTR htmlContent)| NavigateToString method  |   |
|add/remove NavigationStarting| NavigationStarting event  |  |
|add/remove DocumentStateChanged| DocumentStateChanged event |  |
|add/remove NavigationCompleted| NavigationCompleted completed |  |
|add/remove FrameNavigationStarting|FrameNavigationStarting  |  |
|add/remove MoveFocusRequested| MoveFocusRequested |  |
|add/remove GotFocus| Control.GotFocus event |  |
|add/remove LostFocus| Control.LostFocus  |  |
|add/remove WebResourceRequested|  |  |
|add/remove ScriptDialogOpening| ScriptDialogOpening event   |  |
|add/remove ZoomFactorChanged| ZoomFactorChanged event  |  |
|add/remove PermissionRequested| PermissionRequested event|  |
|add/remove ProcessFailed| ProcessFailed |  |
|AddScriptToExecuteOnDocumentCreated|  |  |
|RemoveScriptToExecuteOnDocumentCreated|  |  |
|ExecuteScript|  |  |
|CapturePreview|  |  |
|Reload|  |  |
|get_Bounds| Control.Bounds property |  |
|put_Bounds| Control.Bounds property |  |
|get_ZoomFactor| ZoomFactor property  |  |
|put_ZoomFactor|  ZoomFactor property |  |
|get_IsVisible| Control.Visible  |  |
|put_IsVisible| Control.Visible |  |
|PostWebMessageAsJson| PostWebMessageAsJson |  |
|PostWebMessageAsString| PostWebMessageAsString  |  |
|add_WebMessageReceived|  |  |
|remove_WebMessageReceived|  |  |
|Close|  |  |
|CallDevToolsProtocolMethod|  |  |
|add_DevToolsProtocolEventReceived|  |  |
|remove_DevToolsProtocolEventReceived|  |  |
|get_BrowserProcessId|  |  |
|get_CanGoBack| CanGoBack property |  |
|get_CanGoForward| CanGoForward property  |  |
|GoBack| GoBack method  |  |
|GoForward| GoForward method |  |
