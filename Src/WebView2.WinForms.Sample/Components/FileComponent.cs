using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Winforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class FileComponent
    {
        private WebView2Control _webView2;
        private MainForm _parent;

        public FileComponent(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _webView2.DocumentTitleChanged += WebView2DocumentTitleChanged;
        }

        public void CleanUp()
        {
            _webView2.DocumentTitleChanged -= WebView2DocumentTitleChanged;
            _webView2 = null;
            _parent = null;
        }

        private void WebView2DocumentTitleChanged(object sender, Wrapper.DocumentTitleChangedEventArgs e)
        {
            string title = _webView2.DocumentTitle;
            _parent.Text = title;
        }

        public void SaveScreenshot()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "WebView2_Screenshot.png";
            saveFileDialog.Filter = "PNG File | *.png";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    _webView2.CapturePreview(WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT.WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT_PNG, file, (e) =>
                    {
                        MessageBox.Show("Preview Captured", "Preview Captured", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
            }
        }

        public void ShowTitle()
        {
            string title = _webView2.DocumentTitle;
            MessageBox.Show(title, "Document Title", MessageBoxButtons.OK);
        }
    }
}
