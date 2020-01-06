using MtrDev.WebView2.Interop;
using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Utils;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Scenarios
{
    public class ScenarioWebViewEventMonitor : ComponentBase
    {
        private MainWindow _parent;
        private WebView2Control _eventSourceWebView2;
        private WebView2Control _webviewEventView;
        private MainWindow _appWindowEventView;

        private string _samplePath = "Scenarios\\ScenarioWebViewEventMonitor.html";
        private string _sampleUri;
        private bool _webResourceAttached;

        public ScenarioWebViewEventMonitor(MainWindow parent, WebView2Control eventSourceWebView2)
        {
            _parent = parent;
            _eventSourceWebView2 = eventSourceWebView2;

            _sampleUri = FileUtil.GetLocalUri(_samplePath);

            _appWindowEventView = new MainWindow(_sampleUri, ()=> {
                InitializeEventView(_appWindowEventView.WebView);
            });
            _appWindowEventView.Show();

            //InitializeEventView();
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void InitializeEventView(WebView2Control webView)
        {
            _webviewEventView = webView;

            _eventSourceWebView2.NavigationStarting += WebView2NavigationStarting;
            _eventSourceWebView2.DocumentStateChanged += WebView2DocumentStateChanged;
            _eventSourceWebView2.NavigationCompleted += WebView2NavigationCompleted;
            _eventSourceWebView2.DocumentTitleChanged += WebView2DocumentTitleChanged;
            _eventSourceWebView2.WebMessageRecieved += WebView2WebMessageRecieved;
            _eventSourceWebView2.NewWindowRequested += WebView2NewWindowRequested;
            _eventSourceWebView2.FrameNavigationStarting += WebView2FrameNavigationStarting;

            _webviewEventView.WebMessageRecieved += EventViewWebMessageRecieved;
        }

        public override void CleanUp()
        {
            _eventSourceWebView2.NavigationStarting -= WebView2NavigationStarting;
            _eventSourceWebView2.DocumentStateChanged -= WebView2DocumentStateChanged;
            _eventSourceWebView2.NavigationCompleted -= WebView2NavigationCompleted;
            _eventSourceWebView2.DocumentTitleChanged -= WebView2DocumentTitleChanged;
            _eventSourceWebView2.WebMessageRecieved -= WebView2WebMessageRecieved;
            _eventSourceWebView2.NewWindowRequested -= WebView2NewWindowRequested;
            _eventSourceWebView2.FrameNavigationStarting -= WebView2FrameNavigationStarting;

            EnableWebResourceRequestedEvent(false);

            _webviewEventView.WebMessageRecieved -= EventViewWebMessageRecieved;
        }

        private void WebView2DocumentTitleChanged(object sender, DocumentTitleChangedEventArgs e)
        {
            string message =
                "{ \"kind\": \"event\", \"name\": \"DocumentTitleChanged\", \"args\": {" +
                            "}" +
                    WebViewPropertiesToJsonString(_eventSourceWebView2) +
                    "}";
            PostEventMessage(message);
        }

        private void WebView2NavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            bool isSuccess = e.IsSuccess;
            WEBVIEW2_WEB_ERROR_STATUS webErrorStatus = e.WebErrorStatus;

            string message =
                "{ \"kind\": \"event\", \"name\": \"NavigationCompleted\", \"args\": {";
            message +=
                "\"isSuccess\": " + BoolToString(isSuccess) + ", " +
                           "\"webErrorStatus\": " + EncodeQuote(WebErrorStatusToString(webErrorStatus)) + " " +
                           "}" +
                    WebViewPropertiesToJsonString(_eventSourceWebView2) +
                    "}";
            PostEventMessage(message);
        }

        private void WebView2DocumentStateChanged(object sender, DocumentStateChangedEventArgs e)
        {
            bool isErrorPage = e.IsErrorPage;
            bool isNewDocument = e.IsNewDocument;

            string message =
                "{ \"kind\": \"event\", \"name\": \"DocumentStateChanged\", \"args\": {";
            message +=
                "\"isErrorPage\": " + BoolToString(isErrorPage) + ", " +
                           "\"isNewDocument\": " + BoolToString(isNewDocument) +
                    "}" +
                    WebViewPropertiesToJsonString(_eventSourceWebView2) +
                    "}";
            PostEventMessage(message);
        }

        private void WebView2FrameNavigationStarting(object sender, NavigationStartingEventArgs e)
        {
            bool cancel = e.Cancel;
            bool isRedirected = e.IsRedirected;
            bool isUserInitiated = e.IsUserInitiated;
            WebView2HttpRequestHeaderCollection requestHeaders = e.HttpHeaderCollection;
            string uri = e.Uri;

            string message =
                "{ \"kind\": \"event\", \"name\": " +
                           "\"FrameNavigationStarting\", \"args\": {" +
                           "\"cancel\": " + BoolToString(cancel) + ", " +
                           "\"isRedirected\": " + BoolToString(isRedirected) + ", " +
                           "\"isUserInitiated\": " + BoolToString(isUserInitiated) + ", " +
                           "\"requestHeaders\": " + RequestHeadersToJsonString(requestHeaders) + ", " +
                           "\"uri\": " + EncodeQuote(uri) + " " +
                           "}" +
                    WebViewPropertiesToJsonString(_eventSourceWebView2) +
                    "}";

            PostEventMessage(message);
        }

        private void WebView2NavigationStarting(object sender, Wrapper.NavigationStartingEventArgs e)
        {
            bool cancel = e.Cancel;
            bool isRedirected = e.IsRedirected;
            bool isUserInitiated = e.IsUserInitiated;
            WebView2HttpRequestHeaderCollection requestHeaders = e.HttpHeaderCollection;
            string uri = e.Uri;

            string message =
                "{ \"kind\": \"event\", \"name\": \"NavigationStarting\", \"args\": {";
            message += "\"cancel\": " + BoolToString(cancel) + ", " +
                "\"isRedirected\": " + BoolToString(isRedirected) + ", " +
                "\"isUserInitiated\": " + BoolToString(isUserInitiated) + ", " +
                "\"requestHeaders\": " + RequestHeadersToJsonString(requestHeaders) + ", " +
                "\"uri\": " + EncodeQuote(uri) + " " +
                "}" +
                WebViewPropertiesToJsonString(_eventSourceWebView2) +
                "}";
            PostEventMessage(message);
        }

        private void WebView2NewWindowRequested(object sender, Wrapper.NewWindowRequestedEventArgs e)
        {
            bool handled = e.Handled;
            bool isUserInitiated = e.IsUserInitiated;
            string uri = e.Uri;

            string message =
                "{ \"kind\": \"event\", \"name\": \"NewWindowRequested\", \"args\": {" +
                           "\"handled\": " + BoolToString(handled) + ", " +
                           "\"isUserInitiated\": " + BoolToString(isUserInitiated) + ", " +
                           "\"uri\": " + EncodeQuote(uri) + ", " +
                           "\"newWindow\": null" +
                           "}"
                    + WebViewPropertiesToJsonString(_eventSourceWebView2)
                    + "}";
            PostEventMessage(message);
        }

        private void EventViewWebMessageRecieved(object sender, WebMessageReceivedEventArgs e)
        {
            string source = _webviewEventView.Source;

            string webMessageAsString = e.WebMessageAsString;

            if (webMessageAsString == _sampleUri)
            {
                if (webMessageAsString.StartsWith("webResourceRequested,on"))
                {
                    EnableWebResourceRequestedEvent(true);
                }
                else if (webMessageAsString.StartsWith("webResourceRequested,off"))
                {
                    EnableWebResourceRequestedEvent(false);
                }
            }
        }

        private void WebView2WebMessageRecieved(object sender, Wrapper.WebMessageReceivedEventArgs e)
        {
            string source = _eventSourceWebView2.Source;

            string webMessageAsString = e.WebMessageAsString;
            string webMessageAsJson = e.WebMessageAsJson;

            string message =
                "{ \"kind\": \"event\", \"name\": \"WebMessageReceived\", \"args\": {" +
                           "\"source\": " + EncodeQuote(source) + ", ";

            if (!string.IsNullOrEmpty(webMessageAsString))
            {
                message += "\"webMessageAsString\": " + EncodeQuote(webMessageAsString) + ", ";
            }
            else
            {
                message += "\"webMessageAsString\": null, ";
            }

            message += "\"webMessageAsJson\": " + EncodeQuote(webMessageAsJson) + " " +
                           "}";
            message += WebViewPropertiesToJsonString(_eventSourceWebView2);

            message += "}";
            PostEventMessage(message);
        }

        public void PostEventMessage(string message)
        {
            _webviewEventView.PostWebMessageAsJson(message);
        }

        private string WebViewPropertiesToJsonString(WebView2Control webView)
        {
            string documentTitle = webView.DocumentTitle; ;
            string source = webView.Source;

            string result = ", \"webview\": {" +
                    "\"documentTitle\": " + EncodeQuote(documentTitle) + ", "
                   + "\"source\": " + EncodeQuote(source) + " "
                   + "}";

            return result;
        }

        private string RequestToJsonString(WebView2WebResourceRequest request)
        {
            IStream content = request.Content;
            WebView2HttpRequestHeaderCollection headers = request.Headers;
            string method = request.Method;
            string uri = request.Uri;

            string result = "{";

            result += "\"content\": ";
            result += (content == null ? "null" : "\"...\"");
            result += ", ";

            result += "\"headers\": " + RequestHeadersToJsonString(headers) + ", ";
            result += "\"method\": " + EncodeQuote(method) + ", ";
            result += "\"uri\": " + EncodeQuote(uri) + " ";

            result += "}";

            return result;
        }

        private void EnableWebResourceRequestedEvent(bool enable)
        {
            if (!enable & _webResourceAttached)
            {
                _eventSourceWebView2.WebResourceRequested -= WebView2WebResourceRequested;
                _webResourceAttached = false;
            }
            else if (enable && !_webResourceAttached)
            {
                _eventSourceWebView2.WebResourceRequested += WebView2WebResourceRequested;
                _webResourceAttached = true;

                _eventSourceWebView2.AddWebResourceRequestedFilter(
                    "*", WEBVIEW2_WEB_RESOURCE_CONTEXT.WEBVIEW2_WEB_RESOURCE_CONTEXT_ALL);
            }
        }

        private void WebView2WebResourceRequested(object sender, WebResourceRequestedEventArgs e)
        {
            WebView2WebResourceRequest webResourceRequest = e.Request;
            WebView2WebResourceResponse webResourceResponse = e.Response;

            string message = "{ \"kind\": \"event\", \"name\": " +
                               "\"WebResourceRequested\", \"args\": {" +
                               "\"request\": " + RequestToJsonString(webResourceRequest) + ", " +
                               "\"response\": null" +
                               "}";

            message += WebViewPropertiesToJsonString(_eventSourceWebView2);
            message += "}";
            PostEventMessage(message);
        }

        private string RequestHeadersToJsonString(WebView2HttpRequestHeaderCollection requestHeaders)
        {
            StringBuilder builder = new StringBuilder("[");
            bool firstWrote = false;

            foreach (var header in requestHeaders.HeaderDictionary)
            {
                if (firstWrote)
                {
                    builder.Append(", ");
                }

                firstWrote = true;
                builder.AppendFormat("{{\"name\":{0}, \"value\": {1} }}",
                    EncodeQuote(header.Key), EncodeQuote(header.Value));
            }
            builder.Append("]");

            return builder.ToString();
        }

        private string EncodeQuote(string raw)
        {
            return "\"" + raw.Replace("\"", "\\\"") + "\"";
        }

        private string WebErrorStatusToString(WEBVIEW2_WEB_ERROR_STATUS status)
        {
            switch (status)
            {
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_UNKNOWN:
                    return "UNKNOWN";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CERTIFICATE_COMMON_NAME_IS_INCORRECT:
                    return "CERTIFICATE_COMMON_NAME_IS_INCORRECT";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CERTIFICATE_EXPIRED:
                    return "CERTIFICATE_EXPIRE";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CLIENT_CERTIFICATE_CONTAINS_ERRORS:
                    return "CLIENT_CERTIFICATE_CONTAINS_ERRORS";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CERTIFICATE_REVOKED:
                    return "CERTIFICATE_REVOKED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CERTIFICATE_IS_INVALID:
                    return "CERTIFICATE_IS_INVALID";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_SERVER_UNREACHABLE:
                    return "SERVER_UNREACHABLE";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_TIMEOUT:
                    return "TIMEOUT";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_ERROR_HTTP_INVALID_SERVER_RESPONSE:
                    return "ERROR_HTTP_INVALID_SERVER_RESPONSE";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CONNECTION_ABORTED:
                    return "CONNECTION_ABORTED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CONNECTION_RESET:
                    return "STATUS_CONNECTION_RESET";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_DISCONNECTED:
                    return "DISCONNECTED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_CANNOT_CONNECT:
                    return "CANNOT_CONNECT";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_HOST_NAME_NOT_RESOLVED:
                    return "HOST_NAME_NOT_RESOLVED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_OPERATION_CANCELED:
                    return "OPERATION_CANCELED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_REDIRECT_FAILED:
                    return "REDIRECT_FAILED";
                case WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_UNEXPECTED_ERROR:
                    return "UNEXPECTED_ERROR";
            }

            return "ERROR";
        }

        private string BoolToString(bool value)
        {
            return value ? "true" : "false";
        }
    }
}
