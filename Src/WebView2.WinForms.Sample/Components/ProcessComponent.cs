using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Winforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ProcessComponent
    {
        private WebView2Control _webView2;
        private MainForm _parent;

        public ProcessComponent(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _webView2.ProcessFailed += WebView2ProcessFailed;
        }

        public void CleanUp()
        {
            _webView2.ProcessFailed -= WebView2ProcessFailed;
            _webView2 = null;
            _parent = null;
        }

        private void WebView2ProcessFailed(object sender, Wrapper.ProcessFailedEventArgs e)
        {
            WEBVIEW2_PROCESS_FAILED_KIND failureType = e.ProcessFailedKind;
            if (failureType == WEBVIEW2_PROCESS_FAILED_KIND.WEBVIEW2_PROCESS_FAILED_KIND_BROWSER_PROCESS_EXITED)
            {
                DialogResult button = MessageBox.Show(
                    "Browser process exited unexpectedly.  Recreate webview?",
                    "Browser process exited",
                    MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    _parent.ReinitializeWebView();
                }
            }
        }

        public void ShowBrowserProcessInfo()
        {
            uint processId = _webView2.BrowserProcessId;

            string message = string.Format("Process ID: {0}", processId);
            MessageBox.Show(message, "Process Info", MessageBoxButtons.OK);
        }

        public void CrashBrowserProcess()
        {
            // Crash the browser's process on command, to test crash handlers.
            _webView2.Navigate("edge://inducebrowsercrashforrealz");
        }

    }
}
