using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Forms;
using MtrDev.WebView2.WinForms.Sample.Scenarios;
using MtrDev.WebView2.WinForms.Sample.Utils;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MtrDev.WebView2.WinForms.Sample
{
    public partial class MainForm : Form
    {
        private long _newVersionToken;
        private bool _containsFullscreenElement = false;
        private WebView2Environment _environment;
        private WebView2Control _webView2Control;
        private NewWindowRequestedEventArgs _newWindowRequestedEventArgs;
        private IWebView2Deferral _newWindowDeferral;
        private string _initialUrl;
        private Action _onWebViewFirstInitialized;

        private SettingsComponent _settingsComponent;
        private FileComponent _fileComponent;
        private ProcessComponent _processComponent;
        private ScriptComponent _scriptComponent;
        private ControlComponent _controlComponent;
        private ViewComponent _viewComponent;

        public MainForm():
            this("https://www.bing.com/", null)
        {
        }

        public MainForm(string initialUrl, Action createdCallback)
        {
            _initialUrl = initialUrl;
            _onWebViewFirstInitialized = createdCallback;

            AutoTabHandle = true;
            InitializeComponent();
            navigationToolBar.GoClicked += NavigationToolBarGoClicked;
            navigationToolBar.BackClicked += NavigationToolBarBackClicked;
            navigationToolBar.CancelClicked += NavigationToolBarCancelClicked;
            navigationToolBar.ReloadClicked += NavigationToolBarReloadClicked;
            navigationToolBar.ForwardClicked += NavigationToolBarForwardClicked;
            InitializeWebView();
        }

        public MainForm(NewWindowRequestedEventArgs args, IWebView2Deferral deferral) : this(string.Empty, null)
        {
            _newWindowRequestedEventArgs = args;
            _newWindowDeferral = deferral;
        }

        public WebView2Control WebView { get { return _webView2Control; } }

        public bool AutoTabHandle { get; set; }

        protected override bool ProcessTabKey(bool forward)
        {
            return base.ProcessTabKey(forward);
        }

        // Decide what to do when an accelerator key is pressed. Instead of immediately performing
        // the action, we hand it to the caller so they can decide whether to run it right away
        // or running it asynchronously. Will return nullptr if there is no action for the key.
        public Action GetAcceleratorKeyFunction(uint key)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (key)
                {
                    case 'N':
                        return ()=>
                        {
                            new MainForm();
                        };
                    case 'Q':
                        return ()=> { Close(); } ;
                    case 'S':
                        return () => { _fileComponent.SaveScreenshot(); };
                    case 'T':
                        return ()=> { /*CreateNewThread();*/ };
                    case 'W':
                        
                        return () =>{ CloseWebView(); };
                }
            }
            return null;
        }

        private void setFocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _webView2Control.Focus();
        }

        private void toggleTabHandlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoTabHandle = !AutoTabHandle;
        }

        private void tabInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _webView2Control.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_NEXT);
        }

        private void reverseTabInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _webView2Control.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PREVIOUS);
        }

        private void webView2Control1_EnvironmentCreated(object sender, EnvironmentCreatedEventArgs e)
        {
            if (e.Result != 0)
            {
                if (e.Result == 2)
                {
                    MessageBox.Show("Couldn't find Edge installation. Do you have a version installed that's compatible with this WebView2 SDK version?",
                        "Error", MessageBoxButtons.OK);
                }
                else
                {
                    CommonDialogs.ShowFailure(e.Result, "Failed to create webview environment");
                }
                return;
            }
            _environment = e.WebViewEnvironment;
            _newVersionToken = _environment.RegisterNewVersionAvailable(OnNewVersionAvailable);
        }

        private void CloseWebView()
        {
            if (_webView2Control != null)
            {
                _webView2Control.Close();
                _webView2Control.EnvironmentCreated -= webView2Control1_EnvironmentCreated;
                _webView2Control.BrowserCreated -= _webView2Control_BrowserCreated;
                _webView2Control.ContainsFullScreenElementChanged -= _webView2Control_ContainsFullScreenElementChanged;
                _webView2Control.NewWindowRequested -= _webView2Control_NewWindowRequested;

                _environment.UnregisterNewVersionAvailable(_newVersionToken);

                _settingsComponent.CleanUp();
                _settingsComponent = null;

                _fileComponent.CleanUp();
                _fileComponent = null;

                _processComponent.CleanUp();
                _processComponent = null;

                _scriptComponent.CleanUp();
                _scriptComponent = null;

                _controlComponent.CleanUp();
                _controlComponent = null;

                _viewComponent.CleanUp();
                _viewComponent = null;

                tableLayoutPanel1.Controls.Remove(_webView2Control);
                _webView2Control.Dispose();
                _webView2Control = null;
            }
        }


        private void InitializeWebView()
        {
            CloseWebView();

            _webView2Control = new WebView2Control();

            _webView2Control.Dock = DockStyle.Fill;
            _webView2Control.Location = new Point(3, 38);
            _webView2Control.Name = "webView2Control1";
            _webView2Control.Size = new Size(1050, 381);
            _webView2Control.TabIndex = 3;
            _webView2Control.EnvironmentCreated += webView2Control1_EnvironmentCreated;
            _webView2Control.BrowserCreated += _webView2Control_BrowserCreated;
            _webView2Control.ContainsFullScreenElementChanged += _webView2Control_ContainsFullScreenElementChanged;
            _webView2Control.NewWindowRequested += _webView2Control_NewWindowRequested;

            tableLayoutPanel1.SetColumnSpan(_webView2Control, 6);
            tableLayoutPanel1.Controls.Add(_webView2Control, 0, 1);
        }


        public void ReinitializeWebView()
        {
            // Save the settings component from being deleted when the WebView is closed, so we can
            // copy its properties to the next settings component.
            InitializeWebView();
        }

        private void ReinitializeWebViewWithNewBrowser()
        {
            // Use the reference to the web view before we close it
            uint webviewProcessId = _webView2Control.BrowserProcessId;

            // We need to close the current webviews and wait for the browser_process to exit
            // This is so the new webviews don't use the old browser exe
            CloseWebView();

            // Make sure the browser process inside webview is closed
            ProcessUtil.EnsureProcessIsClosed(webviewProcessId, 2000);

            InitializeWebView();
        }

        // After the environment is successfully created,
        // register a handler for the NewVersionAvailable event.
        // This handler tells when there is a new Edge version available on the machine.
        private void OnNewVersionAvailable(NewVersionAvailableEventArgs args)
        {
            // Get the version value from args
            string newVersion = args.NewVersion;
            string message = "We detected there is a new version for the browser.";
            message += "\n\nVersion number: ";
            message += newVersion;
            message += "\n\n";
            if (_webView2Control != null)
            {
                message += "Do you want to restart the app? \n\n";
                message += "Click No if you only want to re-create the webviews. \n";
                message += "Click Cancel for no action. \n";
            }
            DialogResult response = MessageBox.Show(message, "New available version",
                (_webView2Control != null)? MessageBoxButtons.YesNoCancel:MessageBoxButtons.OK);

            if (response == DialogResult.Yes)
            {
                RestartApp();
            }
            else if (response == DialogResult.No)
            {
                ReinitializeWebViewWithNewBrowser();
            }
            else
            {
                // do nothing
            }
        }

        private void RestartApp()
        {
            // Use the reference to the web view before we close the app window
            uint webviewProcessId = _webView2Control.BrowserProcessId;

            // To restart the app completely, first we close the current App Window
            CloseWebView();

            // Make sure the browser process inside webview is closed
            ProcessUtil.EnsureProcessIsClosed(webviewProcessId, 2000);

            Application.Restart();
        }

        private void _webView2Control_NewWindowRequested(object sender, NewWindowRequestedEventArgs e)
        {
            IWebView2Deferral deferral = e.GetDeferral();
            MainForm newWindow = new MainForm(e, deferral);
            newWindow.Show();
        }

        private void _webView2Control_ContainsFullScreenElementChanged(object sender, ContainsFullScreenElementChangedEventArgs e)
        {
            if (_settingsComponent.FullScreenAllowed)
            {
                _containsFullscreenElement = _webView2Control.ContainsFullScreenElement;
                if (_containsFullscreenElement)
                {
                    EnterFullScreen();
                }
                else
                {
                    ExitFullScreen();
                }
            }
        }

        private void EnterFullScreen()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            navigationToolBar.Visible = false;
            menuStrip1.Visible = false;
        }

        private void ExitFullScreen()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            navigationToolBar.Visible = true;
            menuStrip1.Visible = true;
        }

        private void _webView2Control_BrowserCreated(object sender, EventArgs e)
        {
            _settingsComponent = new SettingsComponent(_environment, _webView2Control);
            _fileComponent = new FileComponent(this, _webView2Control);
            _processComponent = new ProcessComponent(this, _webView2Control);
            _scriptComponent = new ScriptComponent(this, _webView2Control);
            _controlComponent = new ControlComponent(this, navigationToolBar, _webView2Control);
            _viewComponent = new ViewComponent(this, _webView2Control);

            if (_onWebViewFirstInitialized  != null)
            {
                _onWebViewFirstInitialized.Invoke();
                _onWebViewFirstInitialized = null;
            }

            if (!string.IsNullOrEmpty(_initialUrl))
            {
                _webView2Control.Navigate(_initialUrl);
            }

            if (_newWindowRequestedEventArgs != null)
            {
                _newWindowRequestedEventArgs.NewWindow = _webView2Control.InnerWebView2WebView;
                _newWindowRequestedEventArgs.Handled = true;
                _newWindowDeferral.Complete();
                _newWindowRequestedEventArgs = null;
                _newWindowDeferral = null;
            }
        }

        #region Tool Bar event handlers
        private void NavigationToolBarGoClicked(object sender, EventArgs e)
        {
            _controlComponent.NavigateToAddressBar();
        }

        private void NavigationToolBarForwardClicked(object sender, EventArgs e)
        {
            _webView2Control.GoForward();
        }

        private void NavigationToolBarReloadClicked(object sender, EventArgs e)
        {
            _webView2Control.Reload();
        }

        private void NavigationToolBarCancelClicked(object sender, EventArgs e)
        {
            _webView2Control.Stop();
        }

        private void NavigationToolBarBackClicked(object sender, EventArgs e)
        {
            _webView2Control.GoBack();
        }
        #endregion Tool Bar event handlers

        #region File Menu Items
        private void saveScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileComponent.SaveScreenshot();
        }

        private void getDocumentTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileComponent.ShowTitle();
        }

        private void getBrowserVersionAfterCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string version = _environment.BrowserVersionInfo;
            MessageBox.Show(version, "Browser Version Info After WebView Creation", MessageBoxButtons.OK);
        }

        private void getBrowserVersionBeforeCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string versionInfo;
            Globals.GetWebView2BrowserVersionInfo(null, out versionInfo);
            MessageBox.Show(versionInfo, "Browser Version Info Before WebView Creation", MessageBoxButtons.OK);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion File Menu Items

        #region Script Menu Item
        private void injectScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.InjectScript();
        }

        private void addInitializeScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.AddInitializeScript();
        }

        private void removeInitializeScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.RemoveInitializeScript();
        }

        private void postMessageStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.SendStringWebMessage();
        }

        private void postMessageJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.SendJsonWebMessage();
        }
        private void subscribeToCDPEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.SubscribeToCdpEvent();
        }

        private void callCDPMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.CallCdpMethod();
        }
        private void addCOMObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.AddComObject();
        }

        private void openDevToolsWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scriptComponent.OpenDevToolsWindow();
        }
        #endregion Script Menu Item

        #region Window Menu Item 
        private void closeWebViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseWebView();
        }

        private void createWebViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeWebView();
        }

        private void createNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm();
            f.Show();
        }
        private void createNewThreadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion Window Menu Item 

        #region Settings Menu Item
        private void blockedDomainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ChangeBlockedSites();
        }

        private void setUserAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ChangeUserAgent();
        }

        private void toggleJavaScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleJavascript();
        }

        private void toggleWebMessagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleWebMessages();
        }

        private void toggleFullscreenAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleFullScreenAllowed();
        }

        private void toggleStatusBarEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleStatusBar();
        }

        private void toggleDevToolsEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleDevToolsEnabled();
        }

        private void toggleBlockImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleBlockedImages();
        }

        private void useDefaultScriptDialogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.UseDefaultScriptDialogs();
        }

        private void useCustomScriptDialogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.UseCustomScriptDialogs();
        }

        private void useDeferredScriptDialogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.UseDeferredScriptDialogs();
        }

        private void completeDeferredScriptDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.CompleteScriptDialogDeferral();
        }

        private void toggleContextMenusEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsComponent.ToggleContextMenuEnagled();
        }
        #endregion Settings Menu Item

        #region Processor Menu Item

        private void browserProcessInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processComponent.ShowBrowserProcessInfo();
        }

        private void crashBrowserProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processComponent.CrashBrowserProcess();
        }

        #endregion Processor Menu Item

        #region View Menu Items
        private void toggleVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewComponent.ToggleVisibility();
        }

        private void getWebViewBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewComponent.ShowWebViewBounds();
        }

        private void menuItemZoomhalf_Click(object sender, EventArgs e)
        {
            _viewComponent.SetZoomFactor(.5f);
        }

        private void menuItemZoomOne_Click(object sender, EventArgs e)
        {
            _viewComponent.SetZoomFactor(1f);
        }

        private void menuItemZoomTwo_Click(object sender, EventArgs e)
        {
            _viewComponent.SetZoomFactor(2f);
        }

        private void menuItemViewArea25_Click(object sender, EventArgs e)
        {

        }

        private void menuItemViewArea50_Click(object sender, EventArgs e)
        {

        }

        private void menuItemViewArea75_Click(object sender, EventArgs e)
        {

        }

        private void menuItemViewArea100_Click(object sender, EventArgs e)
        {

        }

        #endregion View Menu Items

        #region Scenrio Menu Items

        private void webMessagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScenarioWebMessage scenerio = new ScenarioWebMessage(this, _webView2Control);
            _components.Add(scenerio);
        }

        private void remoteObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScenarioAddRemoteObject scenerio = new ScenarioAddRemoteObject(this, _webView2Control);
            _components.Add(scenerio);
        }

        private void eventMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScenarioWebViewEventMonitor scenerio = new ScenarioWebViewEventMonitor(this, _webView2Control);
            _components.Add(scenerio);

        }
        #endregion

        #region  Component Management
        private IList<ComponentBase> _components = new List<ComponentBase>();

        public void DeleteComponent(ComponentBase component)
        {
            _components.Remove(component);
            component.CleanUp();
        }
        #endregion

        #region Help Menu Items
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Owner = this;
            form.ShowDialog();
        }
        #endregion Help Menu Items
    }
}
    