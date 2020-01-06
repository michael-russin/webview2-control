using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wpf.Sample.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ControlComponent : ComponentBase
    {
        private MainWindow _parent;
        private WebView2Control _webView2;
        private NavigationToolBar _toolbar;

        public ControlComponent(MainWindow parent, NavigationToolBar toolbar, WebView2Control webView2)
        {
            _parent = parent;
            _toolbar = toolbar;
            _webView2 = webView2;
            _webView2.NavigationStarting += WebView2NavigationStarting;
            _webView2.DocumentStateChanged += WebView2DocumentStateChanged;
            _webView2.NavigationCompleted += WebView2NavigationCompleted;
            _webView2.MoveFocusRequested += WebView2MoveFocusRequested;
            _webView2.AcceleratorKeyPressed += WebView2AcceleratorKeyPressed;
        }

        public override void CleanUp()
        {
            _webView2.NavigationStarting -= WebView2NavigationStarting;
            _webView2.DocumentStateChanged -= WebView2DocumentStateChanged;
            _webView2.NavigationCompleted -= WebView2NavigationCompleted;
            _webView2.MoveFocusRequested -= WebView2MoveFocusRequested;
            _webView2.AcceleratorKeyPressed -= WebView2AcceleratorKeyPressed;
            _webView2 = null;
            _toolbar = null;
            _parent = null;
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register a handler for the NavigationStarting event.
        /// This handler just enables the Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2NavigationStarting(object sender, Wrapper.NavigationStartingEventArgs e)
        {
            _toolbar.CanCancel = true;
        }

        /// <summary>
        /// Register a handler for the DocumentStateChanged event.
        /// This handler will read the webview's source URI and update
        /// the app's address bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2DocumentStateChanged(object sender, Wrapper.DocumentStateChangedEventArgs e)
        {
            string source = _webView2.Source;
            if (source == "about::blank")
                source = "";
            _toolbar.Url = source;

        }

        /// <summary>
        /// Register a handler for the NavigationCompleted event.
        /// Check whether the navigation succeeded, and if not, do something.
        /// Also update the Back, Forward, and Cancel buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2NavigationCompleted(object sender, Wrapper.NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                WEBVIEW2_WEB_ERROR_STATUS webErrorStatus = e.WebErrorStatus;
                if (webErrorStatus == WEBVIEW2_WEB_ERROR_STATUS.WEBVIEW2_WEB_ERROR_STATUS_DISCONNECTED)
                {
                    // Do something here if you want to handle a specific error case.
                    // In most cases this isn't necessary, because the WebView will
                    // display its own error page automatically.
                }
            }

            _toolbar.CanGoBack = _webView2.CanGoBack;
            _toolbar.CanGoForward = _webView2.CanGoForward;
            _toolbar.CanCancel = false;
        }

        /// <summary>
        /// Register a handler for the MoveFocusRequested event.
        /// This event will be fired when the user tabs out of the webview.
        /// The handler will focus another window in the app, depending on which
        /// direction the focus is being shifted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2MoveFocusRequested(object sender, Wrapper.MoveFocusRequestedEventArgs e)
        {
            if (!_parent.AutoTabHandle)
            {
                WEBVIEW2_MOVE_FOCUS_REASON reason = e.Reason;

                if (reason == WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_NEXT)
                {
                    _parent.SelectNextControl(_webView2);
                }
                else if (reason == WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PREVIOUS)
                {
                    _parent.SelectNextControl(_webView2);
                }
                e.Handled = true;
            }
        }

        private void WebView2AcceleratorKeyPressed(object sender, Wrapper.AcceleratorKeyPressedEventArgs e)
        {
            WEBVIEW2_KEY_EVENT_TYPE type = e.KeyEventType;
            // We only care about key down events.
            if (type == WEBVIEW2_KEY_EVENT_TYPE.WEBVIEW2_KEY_EVENT_TYPE_KEY_DOWN ||
                type == WEBVIEW2_KEY_EVENT_TYPE.WEBVIEW2_KEY_EVENT_TYPE_SYSTEM_KEY_DOWN)
            {
                uint key = e.VirtualKey;
                // Check if the key is one we want to handle.
                Action action = _parent.GetAcceleratorKeyFunction(key);
                if (action != null)
                {
                    // Keep the browser from handling this key, whether it's autorepeated or
                    // not.
                    e.Handle(1);

                    // Filter out autorepeated keys.
                    WEBVIEW2_PHYSICAL_KEY_STATUS status = e.PhysicalKeyStatus;
                    if (status.WasKeyDown != 1)
                    {
                        // Perform the action asynchronously to avoid blocking the
                        // browser process's event queue.
                        Task.Run(action);
                    }
                }
            }
        }

        public void NavigateToAddressBar()
        {
            string url = _toolbar.Url;
            _webView2.Navigate(url);
        }
    }
}
