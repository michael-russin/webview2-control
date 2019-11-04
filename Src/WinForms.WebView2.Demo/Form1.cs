using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Russinsoft.WebView2.Interop;
using Russinsoft.WinForms;

namespace WinForms.WebView2.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            this.Load += Form1_Load;
        }

        private bool _firstLoad = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!_firstLoad)
                return;

            WebView2Loader.CreateEnvironmentWithDetails(string.Empty, string.Empty, string.Empty, (EnvironmentCreatedEventArgs environmentArgs) =>
            {
                WebView2Environment webViewEnvironment = environmentArgs.WebViewEnvironment;

                webViewEnvironment.CreateWebView(this.panel1.Handle, (CreateWebViewCompletedEventArgs args) =>
                {
                    int result2 = args.Result;
                    WebView2WebView webView = args.WebView;

                    if (webView != null)
                    {
                        _webviewWindow = webView;
                    }

                    // Add a few settings for the webview
                    // this is a redundant demo step as they are the default settings values
                    WebView2Settings settings = _webviewWindow.Settings;
                    settings.IsScriptEnabled = true;
                    settings.AreDefaultScriptDialogsEnabled = true;
                    settings.IsWebMessageEnabled = true;

                    // Resize WebView to fit the bounds of the parent window
                    _webviewWindow.Bounds = new Rectangle(new Point(0, 0), panel1.Size);

                    // Step 4 - Navigation events
                    // register an IWebView2NavigationStartingEventHandler to cancel any non-https navigation
                    long navigationToken = 0;
                    navigationToken = _webviewWindow.RegisterNavigationCompleted((c) => {
                        bool success = c.IsSuccess;

                        _webviewWindow.UnregisterNavigationCompleted(navigationToken);

//                        _webviewWindow.Navigate("https://www.cnn.com/");
                    });

                    // Schedule an async task to navigate to Bing
                    _webviewWindow.Navigate("https://www.bing.com/");
                });
            });
            _firstLoad = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Resize WebView to fit the bounds of the parent window
            if (_webviewWindow != null)
            {
//                Rectangle parentBounds = panel1.Bounds;
                _webviewWindow.Bounds = new Rectangle(new Point(0,0), panel1.Size);
            }
        }

        private WebView2WebView _webviewWindow;

        //// for Windows 10 version RS2 and above
        [DllImport("WebView2Loader.dll", SetLastError = true)]
        private static extern int CreateWebView2EnvironmentWithDetails(
                string browserExecutableFolder,
                string userDataFolder,
                string additionalBrowserArguments,
                IWebView2CreateWebView2EnvironmentCompletedHandler environment_created_handler);

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;

            webBrowser1.Url = new Uri(url);
            webView2Control2.Url = url;
        }

        private void webView2Control2_NavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigation Completed " + e.IsSuccess);
        }

        private void webView2Control2_NavigationStarting(object sender, NavigationStartingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigation Starting " + e.IsUserInitiated);

            WebView2HttpRequestHeaderCollection headerCollection = e.HttpHeaderCollection;
            IReadOnlyDictionary<string, string> headers = headerCollection.HeaderDictionary;
            foreach (string key in headers.Keys)
            {
                System.Diagnostics.Debug.WriteLine("Request Header " + key + "=" + headers[key]);
            }
            //e.Cancel = true;
        }
    }
}
