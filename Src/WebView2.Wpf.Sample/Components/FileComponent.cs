using Microsoft.Win32;
using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wpf.Sample;
using MtrDev.WebView2.Wrapper;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class FileComponent : ComponentBase
    {
        private WebView2Control _webView2;
        private MainWindow _parent;

        public FileComponent(MainWindow parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _webView2.DocumentTitleChanged += WebView2DocumentTitleChanged;
        }

        public override void CleanUp()
        {
            _webView2.DocumentTitleChanged -= WebView2DocumentTitleChanged;
            _webView2 = null;
            _parent = null;
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            if (command == FileCommands.SaveScreenshot)
            {
                SaveScreenshot();
            }
            else if (command == FileCommands.GetDocumentTitle)
            {
                ShowTitle();
            }
            else if (command == FileCommands.VersionBeforeCreation)
            {
                string versionInfo;
                Globals.GetCoreWebView2BrowserVersionInfo(null, out versionInfo);
                MessageBox.Show(versionInfo, "Browser Version Info Before WebView Creation", MessageBoxButton.OK);
            }
            else if (command == FileCommands.VersionAfterCreation)
            {
                string version = _parent.WebView2Environment.BrowserVersionInfo;
                MessageBox.Show(version, "Browser Version Info After WebView Creation", MessageBoxButton.OK);
            }
        }

        private void WebView2DocumentTitleChanged(object sender, Wrapper.DocumentTitleChangedEventArgs e)
        {
            string title = _webView2.DocumentTitle;
            _parent.Title = title;
        }

        public void SaveScreenshot()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "WebView2_Screenshot.png";
            saveFileDialog.Filter = "PNG File | *.png";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream file = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    _webView2.CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT.WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT_PNG, file, (e) =>
                    {
                        MessageBox.Show("Preview Captured", "Preview Captured", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }
            }
        }

        public void ShowTitle()
        {
            string title = _webView2.DocumentTitle;
            MessageBox.Show(title, "Document Title", MessageBoxButton.OK);
        }
    }
}
