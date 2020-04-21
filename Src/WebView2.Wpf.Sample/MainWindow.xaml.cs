using MtrDev.WebView2.Interop;
using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Forms;
using MtrDev.WebView2.WinForms.Sample.Scenarios;
using MtrDev.WebView2.WinForms.Sample.Utils;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wpf.Sample;
using MtrDev.WebView2.Wpf.Sample.Forms;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebView2.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private long _newVersionToken;
        private bool _containsFullscreenElement = false;
        private WebView2Environment _environment;
        private WebView2Control _webView2Control;
        private NewWindowRequestedEventArgs _newWindowRequestedEventArgs;
        private ICoreWebView2Deferral _newWindowDeferral;
        private string _initialUrl;
        private Action _onWebViewFirstInitialized;

        private SettingsComponent _settingsComponent;
        private FileComponent _fileComponent;
        private ProcessComponent _processComponent;
        private ScriptComponent _scriptComponent;
        private ControlComponent _controlComponent;
        private ViewComponent _viewComponent;

        public MainWindow() :
            this("https://www.bing.com/", null)
        {
        }

        public MainWindow(string initialUrl, Action createdCallback)
        {
            InitializeComponent();

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

        public MainWindow(NewWindowRequestedEventArgs args, ICoreWebView2Deferral deferral) : 
            this(string.Empty, null)
        {
            _newWindowRequestedEventArgs = args;
            _newWindowDeferral = deferral;
        }

        public WebView2Control WebView { get { return _webView2Control; } }

        public bool AutoTabHandle { get; set; }

        public WebView2Environment WebView2Environment { get => _environment; }

        private void CloseWebView(bool cleanupUserDataFolder)
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

                mainGrid.Children.Remove(_webView2Control);
                _webView2Control.Dispose();
                _webView2Control = null;
            }

            if (cleanupUserDataFolder)
            {
                // For non-UWP apps, the default user data folder {Executable File Name}.WebView2
                // is in the same directory next to the app executable. If end
                // developers specify userDataFolder during WebView environment
                // creation, they would need to pass in that explicit value here.
                // For more information about userDataFolder:
                // https://docs.microsoft.com/microsoft-edge/hosting/webview2/reference/webview2.idl#createwebview2environmentwithdetails
                string userDataFolder = Environment.CurrentDirectory;
                // Obtain the absolute path for relative paths that include "./" or "../"
                userDataFolder = System.IO.Path.Combine(userDataFolder, "MtrDev.WebView2.WinForms.Sample.exe.WebView2");
                string userDataFolderPath = userDataFolder;

                string message = "Are you sure you want to clean up the user data folder at\n";
                message += userDataFolderPath;
                message += "\n?\nWarning: This action is not reversible.\n\n";
                message += "Click No if there are other open WebView instnaces.\n";

                if (MessageBox.Show(this, message, "Cleanup User Data Folder", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    try
                    {
                        Directory.Delete(userDataFolderPath, true);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void InitializeWebView()
        {
            CloseWebView(false);

            _webView2Control = new WebView2Control();

            Grid.SetRow(_webView2Control, 2);
            _webView2Control.Name = "webView2Control1";
            _webView2Control.IsTabStop = true;
            _webView2Control.TabIndex = 3;
            _webView2Control.EnvironmentCreated += webView2Control1_EnvironmentCreated;
            _webView2Control.BrowserCreated += _webView2Control_BrowserCreated;
            _webView2Control.ContainsFullScreenElementChanged += _webView2Control_ContainsFullScreenElementChanged;
            _webView2Control.NewWindowRequested += _webView2Control_NewWindowRequested;

            mainGrid.Children.Add(_webView2Control);
        }

        private void webView2Control1_EnvironmentCreated(object sender, EnvironmentCreatedEventArgs e)
        {
            if (e.Result != 0)
            {
                if (e.Result == 2)
                {
                    MessageBox.Show("Couldn't find Edge installation. Do you have a version installed that's compatible with this WebView2 SDK version?",
                        "Error", MessageBoxButton.OK);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => CommonDialogs.ShowFailure(e.Result, "Failed to create webview environment")));
                }
                return;
            }
            _environment = e.WebViewEnvironment;
            _newVersionToken = _environment.RegisterNewVersionAvailable(OnNewVersionAvailable);
        }


        // Decide what to do when an accelerator key is pressed. Instead of immediately performing
        // the action, we hand it to the caller so they can decide whether to run it right away
        // or running it asynchronously. Will return nullptr if there is no action for the key.
        public Action GetAcceleratorKeyFunction(uint key)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                switch (key)
                {
                    case 'N':
                        return () =>
                        {
                            new MainWindow();
                        };
                    case 'Q':
                        return () => { Close(); };
                    case 'S':
                        return () => { _fileComponent.SaveScreenshot(); };
                    case 'T':
                        return () => { /*CreateNewThread();*/ };
                    case 'W':

                        return () => { CloseWebView(false); };
                }
            }
            return null;
        }

        public void SelectNextControl(WebView2Control webView)
        {
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Right);
            request.Wrapped = true;
            webView.MoveFocus(request);
        }

        public void SelectPreviousControl(WebView2Control webView)
        {
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Left);
            request.Wrapped = true;
            webView.MoveFocus(request);
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
            CloseWebView(false);

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
            MessageBoxResult response = MessageBox.Show(message, "New available version",
                (_webView2Control != null) ? MessageBoxButton.YesNoCancel : MessageBoxButton.OK);

            if (response == MessageBoxResult.OK)
            {
                RestartApp();
            }
            else if (response == MessageBoxResult.No)
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
            CloseWebView(false);

            // Make sure the browser process inside webview is closed
            ProcessUtil.EnsureProcessIsClosed(webviewProcessId, 2000);

            // Some restart
        }

        private void _webView2Control_NewWindowRequested(object sender, NewWindowRequestedEventArgs e)
        {
            ICoreWebView2Deferral deferral = e.GetDeferral();
            MainWindow newWindow = new MainWindow(e, deferral);
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
            WindowState = WindowState.Maximized;
            navigationToolBar.Visibility = Visibility.Collapsed;
            menuStrip1.Visibility = Visibility.Collapsed;
        }

        private void ExitFullScreen()
        {
            WindowState = WindowState.Normal;
            navigationToolBar.Visibility = Visibility.Visible;
            menuStrip1.Visibility = Visibility.Visible;
        }

        private void _webView2Control_BrowserCreated(object sender, EventArgs e)
        {
            _settingsComponent = new SettingsComponent(_environment, _webView2Control);
            _fileComponent = new FileComponent(this, _webView2Control);
            _processComponent = new ProcessComponent(this, _webView2Control);
            _scriptComponent = new ScriptComponent(this, _webView2Control);
            _controlComponent = new ControlComponent(this, navigationToolBar, _webView2Control);
            _viewComponent = new ViewComponent(this, _webView2Control);

            if (_onWebViewFirstInitialized != null)
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

        #region Menu Command Handlers
        private void Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion Menu Command Handlers


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
        private void FileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _fileComponent.RunCommand(e.Command, e);
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        #endregion File Menu Items

        #region Script Menu Item

        private void ScriptCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _scriptComponent.RunCommand(e.Command, e);
        }

        #endregion Script Menu Item

        #region Window Menu Item 
        private void WindowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == WindowCommands.CloseWebView)
            {
                CloseWebView(false);
            }
            else if (e.Command == WindowCommands.CloseWebViewAndClear)
            {
                CloseWebView(true);
            }
            else if (e.Command == WindowCommands.CreateWebView)
            {
                InitializeWebView();
            }
            else if (e.Command == WindowCommands.CreateNewWindow)
            {
                MainWindow f = new MainWindow();
                f.Show();

            }
            else if (e.Command == WindowCommands.CreateNewThread)
            {
            }
        }

        #endregion Window Menu Item 

        #region Settings Menu Item
        private void SettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _settingsComponent.RunCommand(e.Command, e);
        }

        #endregion Settings Menu Item

        #region Processor Menu Item
        private void ProcessCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _processComponent.RunCommand(e.Command, e);
        }

        #endregion Processor Menu Item

        #region View Menu Items
        private void ViewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewComponent.RunCommand(e.Command, e);
        }
        #endregion View Menu Items

        #region Scenrio Menu Items

        private void ScenerioCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ScenerioCommands.WebMessaging)
            {
                ScenarioWebMessage scenerio = new ScenarioWebMessage(this, _webView2Control);
                _components.Add(scenerio);

            }
            else if (e.Command == ScenerioCommands.RemoteObjects)
            {
                ScenarioAddRemoteObject scenerio = new ScenarioAddRemoteObject(this, _webView2Control);
                _components.Add(scenerio);
            }
            else if (e.Command == ScenerioCommands.EventMonitor)
            {
                ScenarioWebViewEventMonitor scenerio = new ScenarioWebViewEventMonitor(this, _webView2Control);
                _components.Add(scenerio);
            }
        }
        #endregion

        #region Help Menu Items

        private void HelpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == HelpCommands.About)
            {
                AboutWindow form = new AboutWindow();
                form.Owner = this;
                form.ShowDialog();
            }
        }

        #endregion Help Menu Items

        #region  Component Management
        private IList<ComponentBase> _components = new List<ComponentBase>();

        public void DeleteComponent(ComponentBase component)
        {
            _components.Remove(component);
            component.CleanUp();
        }
        #endregion


    }
}
