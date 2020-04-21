using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtrDev.WebView2.WinForms.Sample.Scenarios
{
    public class ScenarioWebMessage : ComponentBase
    {
        private MainForm _parent;
        private WebView2Control _webView2;

        string _samplePath = "Scenarios\\ScenarioWebMessage.html";
        string _sampleUri;

        public ScenarioWebMessage(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _sampleUri = FileUtil.GetLocalUri(_samplePath);

            _webView2.IsWebMessageEnabled = true;

            _webView2.WebMessageRecieved += WebView2WebMessageRecieved;
            _webView2.ContentLoading += WebView2ContentLoading;

            // Changes to IWebView2Settings::IsWebMessageEnabled apply to the next document
            // to which we navigate.
            _webView2.Navigate(_sampleUri);
        }

        public override void CleanUp()
        {
            _webView2.WebMessageRecieved -= WebView2WebMessageRecieved;
            _webView2.ContentLoading -= WebView2ContentLoading;

            _webView2 = null;
            _parent = null;
        }

        /// <summary>
        /// Turn off this scenario if we navigate away from the sample page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2ContentLoading(object sender, Wrapper.ContentLoadingEventArgs e)
        {
            string uri = _webView2.Source;
            if (uri != _sampleUri)
            {
                _parent.DeleteComponent(this);
            }
        }

        private void WebView2WebMessageRecieved(object sender, Wrapper.WebMessageReceivedEventArgs e)
        {
            string url = _webView2.Source;

            // Always validate that the origin of the message is what you expect.
            if (url != _sampleUri)
            {
                return;
            }
            string message = e.WebMessageAsString;

            if (message.StartsWith("SetTitleText "))
            {
                _parent.Text = message.Substring(13);
            }
            else if (message.StartsWith("GetWindowBounds"))
            {
                Rectangle bounds = _webView2.Bounds;
                string reply =
                    "{\"WindowBounds\":\"Left:" + bounds.Left.ToString()
                    + "\\nTop:" + bounds.Top.ToString()
                    + "\\nRight:" + bounds.Right.ToString()
                    + "\\nBottom:" + bounds.Bottom.ToString()
                    + "\"}";
                _webView2.PostWebMessageAsJson(reply);
            }
        }
    }
}
