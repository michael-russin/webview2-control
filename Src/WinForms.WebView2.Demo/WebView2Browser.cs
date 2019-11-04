using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms;

namespace WinForms.WebView2.Demo
{
    public partial class WebView2Browser : Form
    {
        public WebView2Browser()
        {
            InitializeComponent();
            Load += WebView2Browser_Load;
        }
        const int DEFAULT_DPI = 96;
        const int INVALID_TAB_ID = 0;
        const int UIBarHeight = 70;
        const int OptionsDropdownHeight = 108;
        const int OptionsDropdownWidth = 200;


        private WebView2Control _optionsWebView;
        private int _activeTabId;
        private IDictionary<int, Tab> _tabDictionary = new Dictionary<int, Tab>();

        private void WebView2Browser_Load(object sender, EventArgs e)
        {
            
        }

        private void _optionsWebView_LostFocus(object sender, EventArgs e)
        {
            JObject jObject = new JObject();
            jObject.Add("message", JToken.FromObject(Messages.MG_OPTIONS_LOST_FOCUS));
            jObject.Add("args", JToken.FromObject("{}"));

            string json = jObject.ToString();

            controlsWebView2.PostWebMessageAsJson(json);
        }

        private void _optionsWebView_WebMessageRecieved(object sender, WebMessageReceivedEventArgs e)
        {
            OnWebMessageRecieved(e);
        }

        private void _optionsWebView_ZoomFactorChanged(object sender, ZoomFactorCompletedEventArgs e)
        {
            _optionsWebView.ZoomFactor = 1.0;
        }

        string GetAppDataDirectory(string path)
        {
            string dataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!string.IsNullOrEmpty(dataDirectory))
            {
                dataDirectory = Path.Combine(dataDirectory, "Microsoft");
            }
            else
            {
                dataDirectory = ".";
            }

            dataDirectory = Path.Combine(dataDirectory, path);
            return dataDirectory;
        }

        string GetFullPathFor(string relativePath)
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            string fulllName = new FileInfo(location.AbsolutePath).Directory.FullName;

            return Path.Combine(fulllName, relativePath);
        }

        string GetFilePathAsURI(string fullPath)
        {
            string fileURI;
            Uri uri = new Uri(fullPath);
            //            DWORD uriFlags = Uri_CREATE_ALLOW_IMPLICIT_FILE_SCHEME;
            //            HRESULT hr = CreateUri(fullPath.c_str(), uriFlags, 0, &uri);

            return uri.AbsoluteUri;

            //if (SUCCEEDED(hr))
            //{
            //    wil::unique_bstr absoluteUri;
            //    uri->GetAbsoluteUri(&absoluteUri);
            //    fileURI = std::wstring(absoluteUri.get());
            //}

            //return fileURI;
        }

        private void InitUIWebViews()
        {
            // Get data directory for browser UI data
            //string browserDataDirectory = GetAppDataDirectory("Browser Data");

            //// Create WebView environment for browser UI. A separate data directory is
            //// used to isolate the browser UI from web content requested by the user.
            //WebView2Loader.CreateEnvironmentWithDetails(string.Empty, browserDataDirectory,
            //    string.Empty, (environment) => {
            //        // Environment is ready, create the WebView
            //        //_uiEnvironment = environment.WebViewEnvironment;

            //        CreateBrowserControlsWebView();
            //        CreateBrowserOptionsWebView();
            //    });
        }

        public void OnWebMessageRecieved(WebMessageReceivedEventArgs args)
        {
            string source = args.Source;
            string json = args.WebMessageAsJson;
            string str = args.WebMessageAsString;

            JObject jsonObj = JObject.Parse(json);

            if (!jsonObj.ContainsKey("message"))
            {
                return;
            }

            if (!jsonObj.ContainsKey("args"))
            {
                return;
            }


            Messages m;
            if (!Enum.TryParse<Messages>(jsonObj["message"].Value<string>(), out m))
            {
                return;
            }
            JToken msgArgs = jsonObj["args"];

            switch (m)
            {
                case Messages.MG_CREATE_TAB:
                    {
                        int tabId = msgArgs["tabId"].Value<int>();
                        bool shouldBeActive = msgArgs["active"].Value<bool>();

                        Tab newTab = Tab.CreateNewTab(this, _contentEnvironment, tabId, shouldBeActive);

                        if (!_tabDictionary.ContainsKey(tabId))
                        {
                            newTab.Dock = DockStyle.Fill;
                            tableLayoutPanel1.Controls.Add(newTab, 0, 1);

                            _tabDictionary.Add(tabId, newTab);
                        }
                        else
                        {
//                            _tabDictionary[tabId].close();
                        }
                        //            std::map<size_t, std::unique_ptr<Tab>>::iterator it = m_tabs.find(id);
                        //            if (it == m_tabs.end())
                        //            {
                        //                m_tabs.insert(std::pair<size_t, std::unique_ptr<Tab>>(id, std::move(newTab)));
                        //            }
                        //            else
                        //            {
                        //                m_tabs.at(id)->m_contentWebView->Close();
                        //                it->second = std::move(newTab);
                        //            }
                    }
                    break;
                case Messages.MG_NAVIGATE:
                    {
                        string uri = msgArgs["uri"].Value<string>();
                        string browserScheme = "browser://";

                        if (uri.Contains(browserScheme))
                        {
                            string path = uri.Substring(browserScheme.Length);
                            if (path == "favorites" ||
                                path == "settings" ||
                                path == "history")
                            {
                                string filePath = "Content\\content_ui\\";
                                filePath = Path.Combine(filePath, path);
                                filePath += ".html";
                                string fullPath = GetFullPathFor(filePath);
                                _tabDictionary[_activeTabId].Navigate(fullPath);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Requested unknown browser page");
                            }
                        }
                        else
                        {
                            _tabDictionary[_activeTabId].Navigate(uri);

                            // If this fails navigate to the search URL
                            //string encodedSearchURI = (string)jsonObj.SelectToken("args['encodedSearchURI']");
                            //_tabDictionary[_activeTabId].Navigate(encodedSearchURI);
                        }
                    }
                    break;
                case Messages.MG_GO_FORWARD:
                    {
                        _tabDictionary[_activeTabId].GoForward();
                    }
                    break;
                case Messages.MG_GO_BACK:
                    {
                        _tabDictionary[_activeTabId].GoBack();
                    }
                    break;
                case Messages.MG_RELOAD:
                    {
                        _tabDictionary[_activeTabId].Reload();
                    }
                    break;
                case Messages.MG_CANCEL:
                    {
                        _tabDictionary[_activeTabId].CallDevToolsProtocolMethod("Page.stopLoading", "{}");
                    }
                    break;
                case Messages.MG_SWITCH_TAB:
                    {
                        int tabId = msgArgs["tabId"].Value<int>();

                        SwitchToTab(tabId);
                    }
                    break;
                case Messages.MG_CLOSE_TAB:
                    {
                        int tabId = msgArgs["tabId"].Value<int>();
                        tableLayoutPanel1.Controls.Remove(_tabDictionary[tabId]);
                        _tabDictionary.Remove(tabId);
                    }
                    break;
                case Messages.MG_CLOSE_WINDOW:
                    {
                        //DestroyWindow(m_hWnd);
                    }
                    break;
                case Messages.MG_SHOW_OPTIONS:
                    {
                        _optionsWebView.Visible = true;
                        ResizeUIWebViews();
                        _optionsWebView.BringToFront();
                        _optionsWebView.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PROGRAMMATIC);
                    }
                    break;
                case Messages.MG_HIDE_OPTIONS:
                    {
                        _optionsWebView.Visible = false;
                    }
                    break;
                case Messages.MG_OPTION_SELECTED:
                    {
                        _tabDictionary[_activeTabId].MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PROGRAMMATIC);
                    }
                    break;
                case Messages.MG_GET_FAVORITES:
                case Messages.MG_GET_SETTINGS:
                case Messages.MG_GET_HISTORY:
                    {
                        // Forward back to requesting tab
                        JToken tabIdToken = msgArgs["tabId"];
                        int tabId = tabIdToken.Value<int>();

                        RemoveFields(msgArgs, new string[] { "tabId" });

                        PostJsonToWebView(jsonObj, _tabDictionary[tabId]);
                    }
                    break;
            //    default:
            //        {
            //            OutputDebugString(L"Unexpected message\n");
            //        }
            //        break;
            }

            //return S_OK;
        }

        private void RemoveFields(JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                RemoveFields(el, fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }
        }

        private void CreateBrowserControlsWebView()
        {
            string controlsPath = GetFullPathFor("Content\\controls_ui\\default.html");
            controlsWebView2.Navigate(controlsPath);

            //_uiEnvironment.CreateWebView(panelControls.Handle, (createViewArgs) => {

            //    int result = createViewArgs.Result;
            //    WebView2WebView webView = createViewArgs.WebView;

            //    if (result != 0)
            //    {
            //        return;
            //    }

            //    _controlsWebView = webView;

            //    WebView2Settings settings = _controlsWebView.Settings;
            //    settings.AreDevToolsEnabled = false;
            //    settings.IsFullscreenAllowed = false;

            //    _zoomEventToken = _controlsWebView.RegisterZoomFactorChanged((args) => {
            //        _controlsWebView.ZoomFactor = 1.0;
            //    });

            //    _webMessageEventToken = _controlsWebView.RegisterWebMessageReceived(OnWebMessageRecieved);

            //    ResizeUIWebViews();

            //    string controlsPath = GetFullPathFor("Content\\controls_ui\\default.html");
            //    _controlsWebView.Navigate(controlsPath);
            //});
        }

        private void CreateBrowserOptionsWebView()
        {
            _optionsWebView = new WebView2Control(_controlEnvironment);
            _optionsWebView.Visible = false;
            _optionsWebView.AreDevToolsEnabled = false;
            _optionsWebView.IsFullscreenAllowed = false;
            _optionsWebView.ZoomFactorChanged += _optionsWebView_ZoomFactorChanged;
            _optionsWebView.WebMessageRecieved += _optionsWebView_WebMessageRecieved;
            _optionsWebView.LostFocus += _optionsWebView_LostFocus;
            _optionsWebView.BrowserCreated += (s, e) => {
                ResizeUIWebViews();
            };

            Controls.Add(_optionsWebView);

            string optionsPath = GetFullPathFor("Content\\controls_ui\\options.html");
            _optionsWebView.Navigate(optionsPath);

            

            //_uiEnvironment.CreateWebView(panelUI.Handle, (createViewArgs) =>
            //{
            //    int result = createViewArgs.Result;
            //    WebView2WebView webview = createViewArgs.WebView;

            //    if (result != 0)
            //    {
            //        return;
            //    }

            //    // WebView created
            //    _optionsWebView = webview;

            //    WebView2Settings settings = _controlsWebView.Settings;
            //    settings.AreDevToolsEnabled = false;
            //    settings.IsFullscreenAllowed = false;

            //    _optionsZoomEventToken = _controlsWebView.RegisterZoomFactorChanged((args) =>
            //    {
            //        _controlsWebView.ZoomFactor = 1.0;
            //    });

            //    // Hide by default
            //    _optionsWebView.IsVisible = false;
            //    _optionsUIMessageBrokerToken = _optionsWebView.RegisterWebMessageReceived(OnWebMessageRecieved);

            //    // Hide menu when focus is lost
            //    _lostOptionsFocus = _optionsWebView.RegisterLostFocus((args) =>
            //    {
            //        JObject jObject = new JObject();
            //        jObject.Add("message", JToken.FromObject(Messages.MG_OPTIONS_LOST_FOCUS));
            //        jObject.Add("args", JToken.FromObject("{}"));

            //        PostJsonToWebView(jObject, _controlsWebView);
            //    });

            //    ResizeUIWebViews();

            //    string optionsPath = GetFullPathFor("Content\\controls_ui\\options.html");
            //    _optionsWebView.Navigate(optionsPath);
            //});
        }

        private void ResizeUIWebViews()
        {
            if (_optionsWebView != null)
            {
                int dpiDropdownWidth = (int)GetDPIAwareBound(OptionsDropdownWidth);
                int dpiBarHeight = (int)GetDPIAwareBound(UIBarHeight);
                int dpiDropdownHeight = (int)GetDPIAwareBound(OptionsDropdownHeight);
                int x = (int)Bounds.Right - dpiDropdownWidth;

                Rectangle bounds = new Rectangle(new Point(x, dpiBarHeight),
                    new Size(dpiDropdownWidth, dpiDropdownHeight));
            //            bounds.top = GetDPIAwareBound(c_uiBarHeight);
            //            bounds.Height = bounds.top + GetDPIAwareBound(c_optionsDropdownHeight);
            //            bounds.left = bounds.right - GetDPIAwareBound(c_optionsDropdownWidth);
                _optionsWebView.Bounds = bounds;
            }


            //if (_controlsWebView != null)
            //{
            //    _controlsWebView.Bounds = new Rectangle(new Point(0, 0), panelControls.Size);
            //}

            //if (_optionsWebView != null)
            //{
            //    _optionsWebView.Bounds = new Rectangle(new Point(0, 0), panel.Size);
            //    RECT bounds;
            //    GetClientRect(m_hWnd, &bounds);
            //    bounds.top = GetDPIAwareBound(c_uiBarHeight);
            //    bounds.bottom = bounds.top + GetDPIAwareBound(c_optionsDropdownHeight);
            //    bounds.left = bounds.right - GetDPIAwareBound(c_optionsDropdownWidth);

            //    RETURN_IF_FAILED(m_optionsWebView->put_Bounds(bounds));
            //}
        }

        private void PostJsonToWebView(JObject jObjectj, WebView2Control webview)
        {
            string json = jObjectj.ToString();

            webview.PostWebMessageAsJson(json);
        }

        private void PostJsonToWebView(string json, WebView2Control webview)
        {
            webview.PostWebMessageAsJson(json);
        }

        private void controlsWebView2_BeforeEnvironmentCreated(object sender, BeforeEnvironmentCreatedEventArgs e)
        {
            e.UserDataFolder = GetAppDataDirectory("User Data");
        }

        private void webView2Control2_BeforeEnvironmentCreated(object sender, BeforeEnvironmentCreatedEventArgs e)
        {
            e.UserDataFolder = GetAppDataDirectory("Browser Data");
        }

        private void controlsWebView2_BrowserCreated(object sender, EventArgs e)
        {
            string controlsPath = GetFullPathFor("Content\\controls_ui\\default.html");
            controlsWebView2.Navigate(controlsPath);

            controlsWebView2.AreDevToolsEnabled = true;
            controlsWebView2.IsFullscreenAllowed = false;

            CreateBrowserOptionsWebView();
        }

        private void controlsWebView2_ZoomFactorChanged(object sender, ZoomFactorCompletedEventArgs e)
        {
            controlsWebView2.ZoomFactor = 1.0;
        }

        private void controlsWebView2_WebMessageRecieved(object sender, WebMessageReceivedEventArgs e)
        {
            OnWebMessageRecieved(e);
        }

        public void HandleTabCreated(int tabId, bool shouldBeActive)
        {
            if (shouldBeActive)
            {
                SwitchToTab(tabId);
            }
        }

        public void HandleTabSecurityUpdate(int tabId, DevToolsProtocolEventReceivedEventArgs eventArgs)
        {
            JObject paramJson = JObject.Parse(eventArgs.ParameterObjectAsJson);

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("message");
                writer.WriteValue(Messages.MG_SECURITY_UPDATE);
                writer.WritePropertyName("args");
                writer.WriteStartObject();
                writer.WritePropertyName("tabId");
                writer.WriteValue(tabId);
                writer.WritePropertyName("state");
                writer.WriteValue(paramJson.GetValue("securityState"));
            }
            string json = sw.ToString();

            PostJsonToWebView(json, controlsWebView2);
        }

        private void SwitchToTab(int tabId)
        {
            int previousActiveTab = _activeTabId;

            if (_tabDictionary.ContainsKey(tabId))
            {
                //                RETURN_IF_FAILED(m_tabs.at(tabId)->ResizeWebView());
                //                RETURN_IF_FAILED(m_tabs.at(tabId)->m_contentWebView->put_IsVisible(TRUE));
                _tabDictionary[tabId].Visible = true;
                _activeTabId = tabId;
                if (previousActiveTab != INVALID_TAB_ID && previousActiveTab != _activeTabId)
                {
                    _tabDictionary[previousActiveTab].Visible = false;
                    //m_tabs.at(previousActiveTab)->m_contentWebView->put_IsVisible(FALSE));
                }

            }
        }

        public void HandleTabURIUpdate(int tabId, Tab webview)
        {
            if (!_tabDictionary.ContainsKey(tabId))
                return;

            string source = webview.Source;

            JObject jObject = new JObject();
            jObject.Add("message", JToken.FromObject(Messages.MG_UPDATE_URI));
            JObject args = new JObject();
            args.Add("tabId", JToken.FromObject(tabId));
            args.Add("uri", JToken.FromObject(source));
            jObject.Add("args", args);

            string uri = source;
            string favoritesURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\favorites.html"));
            string settingsURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\settings.html"));
            string historyURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\history.html"));

            if (uri == favoritesURI)
            {
                args.Add("uriToShow", JToken.FromObject("browser://favorites"));
            }
            else if (uri == settingsURI)
            {
                args.Add("uriToShow", JToken.FromObject("browser://settings"));
            }
            else if (uri == historyURI)
            {
                args.Add("uriToShow", JToken.FromObject("browser://history"));
            }

            bool canGoForward = webview.CanGoForward;
            args.Add("canGoForward", JToken.FromObject(canGoForward));

            bool canGoBack = webview.CanGoBack;
            args.Add("canGoBack", JToken.FromObject(canGoBack));

            PostJsonToWebView(jObject, controlsWebView2);
        }

        public void HandleTabNavStarting(int tabId, Tab webview)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("message");
                writer.WriteValue(Messages.MG_NAV_STARTING);
                writer.WritePropertyName("args");
                writer.WriteStartObject();
                writer.WritePropertyName("tabId");
                writer.WriteValue(tabId);
            }
            string json = sw.ToString();

            PostJsonToWebView(json, controlsWebView2);
        }

        public void HandleTabNavCompleted(int tabId, Tab webview, bool isSuccess)
        {
            string getTitleScript =
                // Look for a title tag
                "(() => {" +
                "    const titleTag = document.getElementsByTagName('title')[0];" +
                "    if (titleTag) {" +
                "        return titleTag.innerHTML;" +
                "    }" +
                // No title tag, look for the file name
                "    pathname = window.location.pathname;" +
                "    var filename = pathname.split('/').pop();" +
                "    if (filename) {" +
                "        return filename;" +
                "    }" +
                // No file name, look for the hostname
                "    const hostname =  window.location.hostname;" +
                "    if (hostname) {" +
                "        return hostname;" +
                "    }" +
                // Fallback: let the UI use a generic title
                "    return '';" +
                "})();";

            string getFaviconURI =
                "(() => {" +
                // Let the UI use a fallback favicon
                "    let faviconURI = '';" +
                "    let links = document.getElementsByTagName('link');" +
                // Test each link for a favicon
                "    Array.from(links).map(element => {" +
                "        let rel = element.rel;" +
                // Favicon is declared, try to get the href
                "        if (rel && (rel == 'shortcut icon' || rel == 'icon')) {" +
                "            if (!element.href) {" +
                "                return;" +
                "            }" +
                // href to icon found, check it's full URI
                "            try {" +
                "                let urlParser = new URL(element.href);" +
                "                faviconURI = urlParser.href;" +
                "            } catch(e) {" +
                // Try prepending origin
                "                let origin = window.location.origin;" +
                "                let faviconLocation = `${origin}/${element.href}`;" +
                "                try {" +
                "                    urlParser = new URL(faviconLocation);" +
                "                    faviconURI = urlParser.href;" +
                "                } catch (e2) {" +
                "                    return;" +
                "                }" +
                "            }" +
                "        }" +
                "    });" +
                "    return faviconURI;" +
                "})();";

            webview.ExecuteScript(getTitleScript, (ExecuteScriptCompletedEventArgs args) =>
            {
                if (args.ErrorCode != 0)
                    return;

                StringBuilder titleSb = new StringBuilder();
                StringWriter titleSw = new StringWriter(titleSb);

                using (JsonWriter writer = new JsonTextWriter(titleSw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("message");
                    writer.WriteValue(Messages.MG_UPDATE_TAB);
                    writer.WritePropertyName("args");
                    writer.WriteStartObject();
                    writer.WritePropertyName("title");
                    writer.WriteValue(args.ResultAsJson.Trim('"'));
                    writer.WritePropertyName("tabId");
                    writer.WriteValue(tabId);
                }
                string titleJson = titleSb.ToString();

                PostJsonToWebView(titleJson, controlsWebView2);
            });


            webview.ExecuteScript(getFaviconURI, (ExecuteScriptCompletedEventArgs args) =>
            {
                if (args.ErrorCode != 0)
                    return;
                StringBuilder iconSb = new StringBuilder();
                StringWriter iconSw = new StringWriter(iconSb);

                using (JsonWriter writer = new JsonTextWriter(iconSw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("message");
                    writer.WriteValue(Messages.MG_UPDATE_FAVICON);
                    writer.WritePropertyName("args");
                    writer.WriteStartObject();
                    writer.WritePropertyName("uri");
                    writer.WriteValue(args.ResultAsJson);
                    writer.WritePropertyName("tabId");
                    writer.WriteValue(tabId);
                }
                string iconJson = iconSw.ToString();

                PostJsonToWebView(iconJson, controlsWebView2);
            });


            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("message");
                writer.WriteValue(Messages.MG_NAV_COMPLETED);
                writer.WritePropertyName("args");
                writer.WriteStartObject();
                writer.WritePropertyName("tabId");
                writer.WriteValue(tabId);
                writer.WritePropertyName("isError");
                writer.WriteValue(!isSuccess);
            }

            string json = sw.ToString();

            PostJsonToWebView(json, controlsWebView2);
        }

        public void HandleTabMessageReceived(int tabId, Tab webview, WebMessageReceivedEventArgs eventArgs)
        {
            string jsonString = eventArgs.WebMessageAsJson;

            JObject jsonObj = JObject.Parse(jsonString);

            string uri = webview.Source;

            Messages message;
            if (!Enum.TryParse<Messages>(jsonObj["message"].Value<string>(), out message))
            {
                return;
            }

            JToken args = jsonObj["args"];

            switch (message)
            {
                case Messages.MG_GET_FAVORITES:
                case Messages.MG_REMOVE_FAVORITE:
                    {
                        string fileURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\favorites.html"));
                        // Only the favorites UI can request favorites
                        if (fileURI == uri)
                        {
                            args["tabId"] = tabId;
                            PostJsonToWebView(jsonObj, controlsWebView2);
                        }
                    }
                    break;
                case Messages.MG_GET_SETTINGS:
                    {
                        string fileURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\settings.html"));
                        // Only the settings UI can request settings
                        if (fileURI == uri)
                        {
                            args["tabId"] = tabId;
                            PostJsonToWebView(jsonObj, controlsWebView2);
                        }
                    }
                    break;
                case Messages.MG_CLEAR_CACHE:
                    {
                        string fileURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\settings.html"));
                        // Only the settings UI can request cache clearing
                        if (fileURI == uri)
                        {
                            args["content"] = false;
                            args["controls"] = false;

                            if (ClearContentCache())
                            {
                                args["content"] = true;
                            }

                            if (ClearControlsCache())
                            {
                                args["controls"] = true;
                            }

                            PostJsonToWebView(jsonObj, _tabDictionary[tabId]);
                        }
                    }
                    break;
                case Messages.MG_CLEAR_COOKIES:
                    {
                        string fileURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\settings.html"));
                        // Only the settings UI can request cookies clearing
                        if (fileURI == uri)
                        {
                            args["content"] = false;
                            args["controls"] = false;

                            if (ClearContentCookies())
                            {
                                args["content"] = true;
                            }
                            
                            if (ClearControlsCookies())
                            {
                                args["controls"] = true;
                            }

                            PostJsonToWebView(jsonObj, _tabDictionary[tabId]);
                        }
                    }
                    break;
                case Messages.MG_GET_HISTORY:
                case Messages.MG_REMOVE_HISTORY_ITEM:
                case Messages.MG_CLEAR_HISTORY:
                    {
                        string fileURI = GetFilePathAsURI(GetFullPathFor("Content\\content_ui\\history.html"));
                        // Only the history UI can request history
                        if (fileURI == uri)
                        {
                            args["tabId"] = tabId;
                            PostJsonToWebView(jsonObj, controlsWebView2);
                        }
                    }
                    break;
                default:
                    {
                        //OutputDebugString(L"Unexpected message\n");
                    }
                    break;
            }
        }

        private WebView2Environment _contentEnvironment;
        private WebView2Environment _controlEnvironment;

        private void webView2Control2_EnvironmentCreated(object sender, EnvironmentCreatedEventArgs e)
        {
            _contentEnvironment = e.WebViewEnvironment;
        }

        protected override void OnResize(EventArgs e)
        {
            ResizeUIWebViews();

            base.OnResize(e);
        }

        private void controlsWebView2_EnvironmentCreated(object sender, EnvironmentCreatedEventArgs e)
        {
            _controlEnvironment = e.WebViewEnvironment;
        }

        private bool ClearContentCache()
        {
            _tabDictionary[_activeTabId].CallDevToolsProtocolMethod("Network.clearBrowserCache", "{}");
            return true;
        }

        private bool ClearControlsCache()
        {
            _tabDictionary[_activeTabId].CallDevToolsProtocolMethod("Network.clearBrowserCache", "{}");
            return true;
        }

        private bool ClearContentCookies()
        {
            _tabDictionary[_activeTabId].CallDevToolsProtocolMethod("Network.clearBrowserCookies", "{}");
            return true;
        }

        private bool ClearControlsCookies()
        {
            controlsWebView2.CallDevToolsProtocolMethod("Network.clearBrowserCookies", "{}");
            return true;
        }

        // This method is not available until Windows 8.1
        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        static extern uint GetDpiForWindow(IntPtr hWnd);

        int GetDPIAwareBound(int bound)
        {
            // Remove the GetDpiForWindow call when using Windows 7 or any version
            // below 1607 (Windows 10). You will also have to make sure the build
            // directory is clean before building again.
            int dpiForWindow = (int)GetDpiForWindow(Handle);
            return (bound * dpiForWindow / DEFAULT_DPI);
        }
    }
}
