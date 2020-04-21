using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.WinForms.Sample.Forms;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class SettingsComponent
    {
        private WebView2Environment _webView2Environment;
        private WebView2Control _webView2;
        private SettingsComponent _settingsComponent;
        private bool _blockImages = false;
        private bool _deferScriptDialogs = false;
        private bool _changeUserAgent = false;
        private bool _isScriptEnabled = true;
        private bool _blockedSitesSet = false;
        private bool _fullScreenAllowed = true;
        private IList<string> _blockedSites = new List<string>();
        private string _overridingUserAgent;

        public SettingsComponent(WebView2Environment webView2Environment, WebView2Control webView2) :
               this(webView2Environment, webView2, null)
        {
        }

        public SettingsComponent(WebView2Environment webView2Environment,  WebView2Control webView2, SettingsComponent settingsComponent)
        {
            _webView2Environment = webView2Environment;
            _webView2 = webView2;
            if (settingsComponent != null)
            {
                _webView2.IsScriptEnabled = settingsComponent._webView2.IsScriptEnabled;
                _webView2.IsWebMessageEnabled = settingsComponent._webView2.IsWebMessageEnabled;
                _webView2.AreDefaultScriptDialogsEnabled = settingsComponent._webView2.AreDefaultScriptDialogsEnabled;
                _webView2.IsStatusBarEnabled = settingsComponent._webView2.IsStatusBarEnabled;
                _webView2.AreDevToolsEnabled = settingsComponent._webView2.AreDevToolsEnabled;
                SetBlockImages(settingsComponent._blockImages);
                SetUserAgent(settingsComponent._overridingUserAgent);
                _deferScriptDialogs = settingsComponent._deferScriptDialogs;
                _isScriptEnabled = settingsComponent._isScriptEnabled;
                _blockedSitesSet = settingsComponent._blockedSitesSet;
                foreach (string site in settingsComponent._blockedSites)
                {
                    _blockedSites.Add(site);
                }
                _overridingUserAgent = settingsComponent._overridingUserAgent;
            }

            _webView2.NavigationStarting += WebView2NavigationStarting;
            _webView2.FrameNavigationStarting += WebView2FrameNavigationStarting;
            _webView2.ScriptDialogOpening += WebView2ScriptDialogOpening;
            _webView2.PermissionRequested += WebView2PermissionRequested;
        }

        public void CleanUp()
        {
            _webView2.NavigationStarting -= WebView2NavigationStarting;
            _webView2.FrameNavigationStarting -= WebView2FrameNavigationStarting;
            _webView2.ScriptDialogOpening -= WebView2ScriptDialogOpening;
            _webView2.PermissionRequested -= WebView2PermissionRequested;
            if (_blockImages)
            {
                _webView2.WebResourceRequested -= WebView2WebResourceRequested;
            }
            if (!string.IsNullOrEmpty(_overridingUserAgent))
            {
                _webView2.WebResourceRequested -= WebView2WebResourceRequestedUserAgent;
            }
        }

        public bool FullScreenAllowed { get => _fullScreenAllowed; set => _fullScreenAllowed = value; }

        /// <summary>
        /// Register a handler for the NavigationStarting event.
        /// This handler will check the domain being navigated to, and if the domain
        /// matches a list of blocked sites, it will cancel the navigation and
        /// possibly display a warning page.  It will also disable JavaScript on
        /// selected websites.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2NavigationStarting(object sender, NavigationStartingEventArgs e)
        {
            string uri = e.Uri;

            if (ShouldBlockUri(uri))
            {
                e.Cancel = true;

                // If the user clicked a link to navigate, show a warning page.
                bool userInitiated = e.IsUserInitiated;

                //! [NavigateToString]
                string htmlContent =
                    "<h1>Domain Blocked</h1>" +
                    "<p>You've attempted to navigate to a domain in the blocked " +
                    "sites list. Press back to return to the previous page.</p>";
                _webView2.NavigateToString(htmlContent);
                //! [NavigateToString]
            }
            //! [IsScriptEnabled]
            // Changes to settings will apply at the next navigation, which includes the
            // navigation after a NavigationStarting event.  We can use this to change
            // settings according to what site we're visiting.
            if (ShouldBlockScriptForUri(uri))
            {
                _webView2.IsScriptEnabled = false;
            }
            else
            {
                _webView2.IsScriptEnabled = _isScriptEnabled;
            }
            //! [IsScriptEnabled]
        }

        /// <summary>
        /// Register a handler for the FrameNavigationStarting event.
        /// This handler will prevent a frame from navigating to a blocked domain.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2FrameNavigationStarting(object sender, NavigationStartingEventArgs e)
        {
            string uri = e.Uri;

            if (ShouldBlockUri(uri))
            {
                e.Cancel = true;
            }
        }

        private TextInputDialog _textInputDialog;
        private ICoreWebView2Deferral _deferral;
        private ScriptDialogOpeningEventArgs _scriptDialogOpeningEventArgs;

        /// <summary>
        /// Register a handler for the ScriptDialogOpening event.
        /// This handler will set up a custom prompt dialog for the user,
        /// and may defer the event if the setting to defer dialogs is enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2ScriptDialogOpening(object sender, ScriptDialogOpeningEventArgs e)
        {
            string uri = e.Uri;
            WEBVIEW2_SCRIPT_DIALOG_KIND type = e.Kind;
            string message = e.Message;
            string defaultText = e.DefaultText;

            string promptString = "The page at '" + uri + "' says:";

            _textInputDialog = new TextInputDialog(
                                "Script Dialog",
                                promptString,
                                message,
                                defaultText,
                                /* readonly */ type != WEBVIEW2_SCRIPT_DIALOG_KIND.WEBVIEW2_SCRIPT_DIALOG_KIND_PROMPT);

            if (_deferScriptDialogs)
            {
                _scriptDialogOpeningEventArgs = e;
                _deferral = e.GetDeferral();
            }
            else
            {
                if (_textInputDialog.ShowDialog() == DialogResult.OK)
                {
                    e.ResultText = _textInputDialog.Input;
                    e.Accept();
                }
                _textInputDialog = null;
            }
        }

        /// <summary>
        /// Register a handler for the PermissionRequested event.
        /// This handler prompts the user to allow or deny the request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2PermissionRequested(object sender, PermissionRequestedEventArgs e)
        {
            string uri = e.Uri;
            WEBVIEW2_PERMISSION_TYPE type = e.PermissionType;
            bool userInitiated = e.IsUserInitiated;

            string message = "Do you want to grant permission for ";
            message += NameOfPermissionType(type);
            message += " to the website at ";
            message += uri;
            message += "?\n\n";
            message += (userInitiated
                ? "This request came from a user gesture."
                : "This request did not come from a user gesture.");

            DialogResult response = MessageBox.Show(message, "Permission Request", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            WEBVIEW2_PERMISSION_STATE state =
                  response == DialogResult.Yes ? WEBVIEW2_PERMISSION_STATE.WEBVIEW2_PERMISSION_STATE_ALLOW
                : response == DialogResult.No ? WEBVIEW2_PERMISSION_STATE.WEBVIEW2_PERMISSION_STATE_DENY
                : WEBVIEW2_PERMISSION_STATE.WEBVIEW2_PERMISSION_STATE_DEFAULT;
            e.State = state;
        }

        /// <summary>
        /// Prompt the user for a list of blocked domains
        /// </summary>
        public void ChangeBlockedSites()
        {
            string blockedSitesString = string.Empty;

            if (_blockedSitesSet)
            {
                foreach (string site in _blockedSites)
                {
                    if (!string.IsNullOrEmpty(blockedSitesString))
                    {
                        blockedSitesString += ";";
                    }
                    blockedSitesString += site;
                }
            }
            else
            {
                blockedSitesString = "foo.com;bar.org";
            }

            TextInputDialog dialog = new TextInputDialog("Blocked Sites",
                                                     "Sites:",
                                                     "Enter hostnames to block, separated by semicolons.",
                                                     blockedSitesString,
                                                     false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _blockedSitesSet = true;
                _blockedSites.Clear();

                string input = dialog.Input;
                string[] sites = input.Split(';');
                _blockedSites = new List<string>(sites);
            }
        }

        /// <summary>
        /// Check the URI's domain against the blocked sites list
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool ShouldBlockUri(string uri)
        {
            Uri ur = new Uri(uri);
            string domain = ur.Host;

            foreach(string site in _blockedSites)
            {
                if (string.Compare(site, domain, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Decide whether a website should have script disabled.  Since we're only using this
        /// for sample code and we don't actually want to break any websites, just return false.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool ShouldBlockScriptForUri(string uri)
        {
            return false;
        }

        /// <summary>
        /// Turn on or off image blocking by adding or removing a WebResourceRequested handler
        /// which selectively intercepts requests for images.
        /// </summary>
        /// <param name="blockImages"></param>
        private void SetBlockImages(bool blockImages)
        {
            if (blockImages != _blockImages)
            {
                _blockImages = blockImages;

                //! [WebResourceRequested]
                if (_blockImages)
                {
                    _webView2.AddWebResourceRequestedFilter("*", WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_IMAGE);
                    _webView2.WebResourceRequested += WebView2WebResourceRequested;
                }
                else
                {
                    _webView2.WebResourceRequested -= WebView2WebResourceRequested;
                }
                //! [WebResourceRequested]
            }
        }

        private void WebView2WebResourceRequested(object sender, WebResourceRequestedEventArgs e)
        {
            WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContext = e.ResourceContext;

            // Ensure that the type is image
            if (resourceContext != WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_IMAGE)
            {
                return;
            }

            // Override the response with an empty one to block the image.
            // If put_Response is not called, the request will continue as normal.
            WebView2WebResourceResponse response = _webView2Environment.CreateWebResourceResponse(null, 403, "Blocked", "");
            e.SetResponse(response);
        }

        /// <summary>
        /// Prompt the user for a new User Agent string
        /// </summary>
        public void ChangeUserAgent()
        {
            TextInputDialog dialog = new TextInputDialog(
                "User Agent",
                "User agent:",
                "Enter user agent, or leave blank to restore default.",
                _changeUserAgent
                    ? _overridingUserAgent
                    : "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3818.0 Safari/537.36 Edg/77.0.188.0",
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SetUserAgent(dialog.Input);
            }
        }

        /// <summary>
        /// Register a WebResourceRequested handler which adds a custom User-Agent
        /// HTTP header to all requests.
        /// </summary>
        private void SetUserAgent(string userAgent)
        {
            if (_changeUserAgent)
            {
                _webView2.WebResourceRequested -= WebView2WebResourceRequestedUserAgent;
            }

            _overridingUserAgent = userAgent;

            if (string.IsNullOrEmpty(_overridingUserAgent))
            {
                _changeUserAgent = false;
            }
            else
            {
                _changeUserAgent = true;

                // Register a handler for the WebResourceRequested event.
                // This handler adds a User-Agent HTTP header to the request,
                // then lets the request continue normally.
                _webView2.AddWebResourceRequestedFilter("*", WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_ALL);
                _webView2.WebResourceRequested += WebView2WebResourceRequestedUserAgent;
            }
        }

        private void WebView2WebResourceRequestedUserAgent(object sender, WebResourceRequestedEventArgs e)
        {
            WebView2WebResourceRequest request = e.Request;
            WebView2HttpRequestHeaderCollection requestHeaders = request.Headers;
            requestHeaders.SetHeader("User-Agent", _overridingUserAgent);
        }

        private string NameOfPermissionType(WEBVIEW2_PERMISSION_TYPE type)
        {
            switch (type)
            {
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_MICROPHONE:
                    return "Microphone";
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_CAMERA:
                    return "Camera";
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_GEOLOCATION:
                    return "Geolocation";
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_NOTIFICATIONS:
                    return "Notifications";
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_OTHER_SENSORS:
                    return "Generic Sensors";
                case WEBVIEW2_PERMISSION_TYPE.WEBVIEW2_PERMISSION_TYPE_CLIPBOARD_READ:
                    return "Clipboard Read";
                default:
                    return "Unknown resources";
            }
        }

        public void ToggleJavascript()
        {
            _isScriptEnabled = !_isScriptEnabled;
            _webView2.IsScriptEnabled = _isScriptEnabled;
            MessageBox.Show("JavaScript will be "
                           + (_isScriptEnabled ? "enabled" : "disabled")
                           + " after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
        }

        public void ToggleWebMessages()
        {
            bool isWebMessageEnabled = _webView2.IsWebMessageEnabled;
            _webView2.IsWebMessageEnabled = !isWebMessageEnabled;
            MessageBox.Show("Web Messaging will be "
                           + (!isWebMessageEnabled ? "enabled" : "disabled")
                           + " after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
        }

        public void ToggleStatusBar()
        {
            bool isStatusBarEnabled = _webView2.IsStatusBarEnabled;
            _webView2.IsStatusBarEnabled = !isStatusBarEnabled;
            MessageBox.Show("Status bar will be " +
                           (!isStatusBarEnabled ? "enabled" : "disabled")
                           + " after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
        }

        public void ToggleDevToolsEnabled()
        {
            bool areDevToolsEnabled = _webView2.AreDevToolsEnabled;
            _webView2.AreDevToolsEnabled = !areDevToolsEnabled;
            MessageBox.Show("Dev tools will be "
                           + (!areDevToolsEnabled ? "enabled" : "disabled")
                           + " after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
        }

        public void ToggleBlockedImages()
        {
            SetBlockImages(!_blockImages);
            MessageBox.Show("Image blocking has been " +
                (_blockImages ? "enabled." : "disabled."),
                       "Settings change", MessageBoxButtons.OK);
        }

        public void UseDefaultScriptDialogs()
        {
            bool defaultCurrentlyEnabled = _webView2.AreDefaultScriptDialogsEnabled;
            if (!defaultCurrentlyEnabled)
            {
                _webView2.AreDefaultScriptDialogsEnabled = true;
                MessageBox.Show("Default script dialogs will be used after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
            }
        }

        public void UseCustomScriptDialogs()
        {
            bool defaultCurrentlyEnabled = _webView2.AreDefaultScriptDialogsEnabled;
            if (defaultCurrentlyEnabled)
            {
                _webView2.AreDefaultScriptDialogsEnabled = false;
                _deferScriptDialogs = false;
                MessageBox.Show("Custom script dialogs without deferral will be used after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
            }
            else if (_deferScriptDialogs)
            {
                _deferScriptDialogs = false;
                MessageBox.Show("Custom script dialogs without deferral will be used now.",
                           "Settings change", MessageBoxButtons.OK);
            }
        }

        public void UseDeferredScriptDialogs()
        {
            bool defaultCurrentlyEnabled = _webView2.AreDefaultScriptDialogsEnabled;
            if (defaultCurrentlyEnabled)
            {
                _webView2.AreDefaultScriptDialogsEnabled = false;
                _deferScriptDialogs = true;
                MessageBox.Show("Custom script dialogs with deferral will be used after the next navigation.",
                           "Settings change", MessageBoxButtons.OK);
            }
            else if (!_deferScriptDialogs)
            {
                _deferScriptDialogs = true;
                MessageBox.Show("Custom script dialogs with deferral will be used now.",
                           "Settings change", MessageBoxButtons.OK);
            }
        }

        public void CompleteScriptDialogDeferral()
        {
            if (_deferral != null)
            {
                if (_textInputDialog.ShowDialog() == DialogResult.OK)
                {
                    _scriptDialogOpeningEventArgs.ResultText = _textInputDialog.Input;
                    _scriptDialogOpeningEventArgs.Accept();
                }

                _deferral.Complete();
                _deferral = null;
                _scriptDialogOpeningEventArgs = null;
                _textInputDialog = null;
            }
        }

        public void ToggleContextMenuEnagled()
        {
            bool allowContextMenus = _webView2.AreDefaultContextMenusEnabled;
            if (allowContextMenus)
            {
                _webView2.AreDefaultContextMenusEnabled = false;
                MessageBox.Show("Context menus will be disabled after the next navigation.",
                    "Settings change", MessageBoxButtons.OK);
            }
            else
            {
                _webView2.AreDefaultContextMenusEnabled = true;
                MessageBox.Show("Context menus will be enabled after the next navigation.",
                    "Settings change", MessageBoxButtons.OK);
            }
        }

        public void ToggleFullScreenAllowed()
        {
            FullScreenAllowed = !FullScreenAllowed;
            MessageBox.Show("Fullscreen is now " +
                           (_fullScreenAllowed ? "allowed" : "disallowed"),
                            "", MessageBoxButtons.OK);
        }

        public void ToggleRemoteObjects()
        {
            bool allowRemoteObjects = _webView2.AreRemoteObjectsAllowed;
            if (allowRemoteObjects)
            {
                _webView2.AreRemoteObjectsAllowed = false;
                MessageBox.Show(
                    "Access to remote objects will be denied after the next navigation.",
                    "Settings change", MessageBoxButtons.OK);
            }
            else
            {
                _webView2.AreRemoteObjectsAllowed = true;
                MessageBox.Show(
                    "Access to remote objects will be allowed after the next navigation.",
                    "Settings change", MessageBoxButtons.OK);
            }
        }

        public void ToggleZoomEnabled()
        {
            bool zoomControlEnabled = _webView2.IsZoomControlEnabled;
            if (zoomControlEnabled)
            {
                _webView2.IsZoomControlEnabled = false;
                MessageBox.Show(
                    "Zoom control is disabled after the next navigation.", "Settings change",
                    MessageBoxButtons.OK);
            }
            else
            {
                _webView2.IsZoomControlEnabled = true;
                MessageBox.Show(
                    "Zoom control is enabled after the next navigation.", "Settings change",
                    MessageBoxButtons.OK);
            }

        }
    }
}
