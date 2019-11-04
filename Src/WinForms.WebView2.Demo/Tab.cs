using Russinsoft.WinForms;
using System;

namespace WinForms.WebView2.Demo
{
    public class Tab : WebView2Control
    {
        public static Tab CreateNewTab(WebView2Browser parentBrowser, WebView2Environment environment, int id, bool shouldBeActive)
        {
            Tab tab = new Tab(parentBrowser, environment, id, shouldBeActive);
            return tab;
        }

        private Tab(WebView2Browser parentBrowser, WebView2Environment environment, int tabId, bool shouldBeActive) : 
            base(environment)
        {
            _tabId = tabId;
            _parentBrowser = parentBrowser;
            _shouldBeActive = shouldBeActive;
        }

        private int _tabId;
        private bool _shouldBeActive;
        private WebView2Browser _parentBrowser;

        protected override void OnWebMessageRecieved(WebMessageReceivedEventArgs e)
        {
            _parentBrowser.HandleTabMessageReceived(_tabId, this, e);
            base.OnWebMessageRecieved(e);
        }

        protected override void OnDocumentStateChanged(DocumentStateChangedEventArgs e)
        {
            _parentBrowser.HandleTabURIUpdate(_tabId, this);
            base.OnDocumentStateChanged(e);
        }

        protected override void OnNavigationStarting(NavigationStartingEventArgs e)
        {
            _parentBrowser.HandleTabNavStarting(_tabId, this);
            base.OnNavigationStarting(e);
        }

        protected override void OnNavigationCompleted(NavigationCompletedEventArgs e)
        {
            _parentBrowser.HandleTabNavCompleted(_tabId, this, e.IsSuccess);
            base.OnNavigationCompleted(e);
        }

        protected override void OnDevToolsProtocolEventReceived(DevToolsProtocolEventReceivedEventArgs e)
        {
            _parentBrowser.HandleTabSecurityUpdate(_tabId, e);
            base.OnDevToolsProtocolEventReceived(e);
        }

        protected override void OnBrowserCreated(EventArgs e)
        {
            // Enable listening for security events to update secure icon
            CallDevToolsProtocolMethod("Security.enable", "{}", null);

            // Forward security status updates to browser
            StartListeningDevToolsProtocolEvent("Security.securityStateChanged");

            Navigate("https://www.bing.com");

            _parentBrowser.HandleTabCreated(_tabId, _shouldBeActive);

            base.OnBrowserCreated(e);
        }
    }
}
