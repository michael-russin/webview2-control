using MtrDev.WebView2.Winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public class ViewComponent
    {
        private MainForm _parent;
        private WebView2Control _webView2;
        private bool _isVisible = true;
        private float _webViewRatio = 1.0f;
        private float _webViewZoomFactor = 1.0f;

        public ViewComponent(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;
            _webView2.ZoomFactorChanged += WebView2ZoomFactorChanged;
        }

        public void CleanUp()
        {
            _webView2.ZoomFactorChanged -= WebView2ZoomFactorChanged;
            _webView2 = null;
            _parent = null;
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
            _parent.Text = message;
        }

        public void ToggleVisibility()
        {
            bool visible = _webView2.Visible;
            _isVisible = !visible;
            _webView2.Visible = _isVisible;
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
            Rectangle bounds = _webView2.Bounds;
            string message = string.Format(" Left:\t {0} \n Top:\t {1} \n Right:\t {2} \n Bottom:\t {3} \n", 
                bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
            MessageBox.Show(message, "WebView Bounds", MessageBoxButtons.OK);
        }
    }
}
