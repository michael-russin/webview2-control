using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.WinForms.Sample.Forms;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ScriptComponent
    {
        private WebView2Control _webView2;
        private MainForm _parent;
        private string _lastInitializeScriptId;
        private IDictionary<string, long> _devToolsProtocolEventReceivedTokenMap = new Dictionary<string, long>();

        public ScriptComponent(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _webView2.DevToolsProtocolEventReceived += WebView2DevToolsProtocolEventReceived;
        }

        public void CleanUp()
        {
            _webView2.DevToolsProtocolEventReceived -= WebView2DevToolsProtocolEventReceived;
            _webView2 = null;
            _parent = null;
        }

        private void WebView2DevToolsProtocolEventReceived(object sender, DevToolsProtocolEventReceivedEventArgs e)
        {
            string parameterObjectAsJson = e.ParameterObjectAsJson;
            string eventName = e.EventName;

            MessageBox.Show(parameterObjectAsJson, ("CDP Event Fired: " + eventName), MessageBoxButtons.OK);
        }

        /// <summary>
        /// Prompt the user for some script and then execute it.
        /// </summary>
        public void InjectScript()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Inject Script",
                "Enter script code:",
                "Enter the JavaScript code to run in the webview.",
                "window.getComputedStyle(document.body).backgroundColor",
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _webView2.ExecuteScript(dialog.Input, (args) =>
                {
                    if (args.ErrorCode != 0)
                    {
                        CommonDialogs.ShowFailure(args.ErrorCode, "ExecuteScript failed");
                    }
                    MessageBox.Show(args.ResultAsJson, "ExecuteScript Result", MessageBoxButtons.OK);
                });
            }
        }

        /// <summary>
        /// Prompt the user for some script and register it to execute whenever a new page loads.
        /// </summary>
        public void AddInitializeScript()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Add Initialize Script",
                "Initialization Script:",
                "Enter the JavaScript code to run as the initialization script that " +
                    "runs before any script in the HTML document.",
                // This example script stops child frames from opening new windows.  Because
                // the initialization script runs before any script in the HTML document, we
                // can trust the results of our checks on window.parent and window.top.
                "if (window.parent !== window.top) {\r\n" +
                  "    delete window.open;\r\n" +
                  "}",
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _webView2.AddScriptToExecuteOnDocumentCreated(
                    dialog.Input,
                    (args) =>
                    {
                        _lastInitializeScriptId = args.Id;
                        MessageBox.Show(_lastInitializeScriptId, "AddScriptToExecuteOnDocumentCreated Id", MessageBoxButtons.OK);
                    });
            }
        }

        /// <summary>
        /// Prompt the user for an initialization script ID and deregister that script.
        /// </summary>
        public void RemoveInitializeScript()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Remove Initialize Script",
                "Script ID:",
                "Enter the ID created from Add Initialize Script.",
                _lastInitializeScriptId,
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _webView2.RemoveScriptToExecuteOnDocumentCreated(dialog.Input);
            }
        }

        /// <summary>
        /// Prompt the user for a string and then post it as a web message.
        /// </summary>
        public void SendStringWebMessage()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Post Web Message String",
                "Web message string:",
                "Enter the web message as a string.",
                string.Empty, 
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _webView2.PostWebMessageAsString(dialog.Input);
            }
        }

        /// <summary>
        /// Prompt the user for some JSON and then post it as a web message.
        /// </summary>
        public void SendJsonWebMessage()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Post Web Message JSON",
                "Web message JSON:",
                "Enter the web message as JSON.",
                "{\"SetColor\":\"blue\"}",
                false);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _webView2.PostWebMessageAsJson(dialog.Input);
            }
        }

        /// <summary>
        /// Prompt the user to name a CDP event, and then subscribe to that event.
        /// </summary>
        public void SubscribeToCdpEvent()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Subscribe to CDP Event",
                "CDP event name:",
                "Enter the name of the CDP event to subscribe to.\r\n" +
                    "You may also have to call the \"enable\" method of the\r\n" +
                    "event's domain to receive events (for example \"Log.enable\").\r\n",
                "Log.entryAdded",
                false);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string eventName = dialog.Input;

                // If we are already subscribed to this event, unsubscribe first.
                if (!string.IsNullOrEmpty(eventName))
                {
                    _webView2.StopListeningDevToolsProtocolEvent(eventName);
                }

                _webView2.StartListeningDevToolsProtocolEvent(eventName);
            }
        }

        /// <summary>
        /// Prompt the user for the name and parameters of a CDP method, then call it.
        /// </summary>
        public void CallCdpMethod()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Call CDP Method",
                "CDP method name:",
                "Enter the CDP method name to call, followed by a space,\r\n" +
                    "followed by the parameters in JSON format.",
                "Runtime.evaluate {\"expression\":\"alert(\\\"test\\\")\"}",
                false);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int delimiterPos = dialog.Input.IndexOf(' ');
                string methodName = dialog.Input.Substring(0, delimiterPos);
                string methodParams =
                    (delimiterPos < dialog.Input.Length
                        ? dialog.Input.Substring(delimiterPos + 1)
                        : "{}");

                _webView2.CallDevToolsProtocolMethod(
                    methodName,
                    methodParams,
                    (args) => {
                        MessageBox.Show(args.ReturnObjectAsJson, "CDP Method Result", MessageBoxButtons.OK);
                    });
            }
        }

        private object _remoteObject;

        public void AddComObject()
        {
            TextInputDialog dialog = new TextInputDialog(
                "Add COM object",
                "CLSID or ProgID of COM object:",
                "Enter the CLSID (eg '{0002DF01-0000-0000-C000-000000000046}')\r\n" +
                "or ProgID (eg 'InternetExplorer.Application') of the COM object to create and\r\n" +
                "provide to the WebView as `window.chrome.remoteObjects.example`.",
                "InternetExplorer.Application",
                false);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Type type = Type.GetTypeFromProgID(dialog.Input, false);
                if (type == null)
                {
                    try
                    {
                        Guid guid = new Guid(dialog.Input);

                        type = Type.GetTypeFromCLSID(guid, false);
                    }
                    catch (Exception) { }
                }
                if (type != null)
                {
                    _remoteObject = Activator.CreateInstance(type);
                    _webView2.AddRemoteObject("example", ref _remoteObject);
                }
                else
                {
                    CommonDialogs.ShowError("Coudn't create COM object.");
                }
            }
        }

        public void OpenDevToolsWindow()
        {
            _webView2.OpenDevToolsWindow();
        }

    }
}
