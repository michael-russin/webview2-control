using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wpf.Sample;
using System.Windows;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ProcessComponent : ComponentBase
    {
        private WebView2Control _webView2;
        private MainWindow _parent;

        public ProcessComponent(MainWindow parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _webView2.ProcessFailed += WebView2ProcessFailed;
        }

        public override void CleanUp()
        {
            _webView2.ProcessFailed -= WebView2ProcessFailed;
            _webView2 = null;
            _parent = null;
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            if (command == ProcessCommands.BrowserProcessInfo)
            {
                ShowBrowserProcessInfo();
            }
            else if (command == ProcessCommands.CrashBrowserProcess)
            {
                CrashBrowserProcess();
            }
        }

        private void WebView2ProcessFailed(object sender, Wrapper.ProcessFailedEventArgs e)
        {
            WEBVIEW2_PROCESS_FAILED_KIND failureType = e.ProcessFailedKind;
            if (failureType == WEBVIEW2_PROCESS_FAILED_KIND.WEBVIEW2_PROCESS_FAILED_KIND_BROWSER_PROCESS_EXITED)
            {
                MessageBoxResult button = MessageBox.Show(
                    "Browser process exited unexpectedly.  Recreate webview?",
                    "Browser process exited",
                    MessageBoxButton.YesNo);
                if (button == MessageBoxResult.Yes)
                {
                    _parent.ReinitializeWebView();
                }
            }
        }

        public void ShowBrowserProcessInfo()
        {
            uint processId = _webView2.BrowserProcessId;

            string message = string.Format("Process ID: {0}", processId);
            MessageBox.Show(message, "Process Info", MessageBoxButton.OK);
        }

        public void CrashBrowserProcess()
        {
            // Crash the browser's process on command, to test crash handlers.
            _webView2.Navigate("edge://inducebrowsercrashforrealz");
        }

    }
}
