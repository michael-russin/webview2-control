using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wpf.Sample;
using System.Windows;
using System.Windows.Input;
using WebView2.Wpf.Sample;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ViewComponent : ComponentBase
    {
        private MainWindow _parent;
        private WebView2Control _webView2;
        private Visibility _visibility = Visibility.Visible;
        private float _webViewRatio = 1.0f;
        private float _webViewZoomFactor = 1.0f;

        public ViewComponent(MainWindow parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;
            _webView2.ZoomFactorChanged += WebView2ZoomFactorChanged;
        }

        public override void CleanUp()
        {
            _webView2.ZoomFactorChanged -= WebView2ZoomFactorChanged;
            _webView2 = null;
            _parent = null;
        }

        public override void RunCommand(ICommand command, ExecutedRoutedEventArgs args)
        {
            if (command == ViewCommands.ToggleVisibility)
            {
                ToggleVisibility();
            }
            else if (command == ViewCommands.GetWebViewBounds)
            {
                ShowWebViewBounds();
            }
            else if (command == ViewCommands.ZoomHalf)
            {
                SetZoomFactor(.5f);
            }
            else if (command == ViewCommands.ZoomWhole)
            {
                SetZoomFactor(1f);
            }
            else if (command == ViewCommands.ZoomTwice)
            {
                SetZoomFactor(2f);
            }
            else if (command == ViewCommands.Area25Percent)
            {

            }
            else if (command == ViewCommands.Area50Percent)
            {

            }
            else if (command == ViewCommands.Area75Percent)
            {

            }
            else if (command == ViewCommands.Area100Percent)
            {
            }
            else if (command == ViewCommands.SetFocus)
            {
                _webView2.Focus();
            }
            else if (command == ViewCommands.TabIn)
            {
                _webView2.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_NEXT);
            }
            else if (command == ViewCommands.ReverseTabIn)
            {
                _webView2.MoveFocus(WEBVIEW2_MOVE_FOCUS_REASON.WEBVIEW2_MOVE_FOCUS_REASON_PREVIOUS);
            }
            else if (command == ViewCommands.ToggleTabHandling)
            {
                _parent.AutoTabHandle = !_parent.AutoTabHandle;
            }
        }

        /// <summary>
        /// Register a handler for the ZoomFactorChanged event.
        /// This handler just announces the new level of zoom on the window's title bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView2ZoomFactorChanged(object sender, Wrapper.ZoomFactorCompletedEventArgs e)
        {
            double zoomFactor = _webView2.ZoomFactor;

            string message = string.Format("WebView2APISample (Zoom: {0}%)", (int)(zoomFactor *100));
            _parent.Title = message;
        }

        public void ToggleVisibility()
        {
            Visibility visible = _webView2.Visibility;
            _visibility = (visible==Visibility.Visible)?Visibility.Hidden:Visibility.Visible;
            _webView2.Visibility = _visibility;
        }

        public void SetSizeRatio(float ratio)
        {
            _webViewRatio = ratio;
        }

        public void SetZoomFactor(float zoom)
        {
            _webViewZoomFactor = zoom;
            _webView2.ZoomFactor = _webViewZoomFactor;
        }

        public void ShowWebViewBounds()
        {
            string message = string.Format(" Left:\t {0} \n Top:\t {1} \n Right:\t {2} \n Bottom:\t {3} \n", 
                0, 0, _webView2.ActualWidth, _webView2.ActualHeight);
            MessageBox.Show(message, "WebView Bounds", MessageBoxButton.OK);
        }
    }
}
