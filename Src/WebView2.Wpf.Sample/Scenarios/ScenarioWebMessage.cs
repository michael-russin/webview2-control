using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Utils;
using MtrDev.WebView2.Wpf;
using System;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Scenarios
{
    public class ScenarioWebMessage : ComponentBase
    {
        private MainWindow _parent;
        private WebView2Control _webView2;

        string _samplePath = "Scenarios\\ScenarioWebMessage.html";
        string _sampleUri;

        public ScenarioWebMessage(MainWindow parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _sampleUri = FileUtil.GetLocalUri(_samplePath);

            _webView2.IsWebMessageEnabled = true;

            _webView2.WebMessageRecieved += WebView2WebMessageRecieved;

            // Changes to IWebView2Settings::IsWebMessageEnabled apply to the next document
            // to which we navigate.
            _webView2.Navigate(_sampleUri);
        }

        public override void CleanUp()
        {
            _webView2.WebMessageRecieved -= WebView2WebMessageRecieved;

            _webView2 = null;
            _parent = null;
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            throw new NotImplementedException();
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
                _parent.Title = message.Substring(13);
            }
            else if (message.StartsWith("GetWindowBounds"))
            {
                string reply =
                    "{\"WindowBounds\":\"Left:" + "0"
                    + "\\nTop:" + "0"
                    + "\\nRight:" + _webView2.ActualWidth
                    + "\\nBottom:" + _webView2.ActualHeight
                    + "\"}";
                _webView2.PostWebMessageAsJson(reply);
            }
        }
    }
}
