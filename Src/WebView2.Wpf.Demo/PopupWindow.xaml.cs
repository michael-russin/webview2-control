using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Wpf;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebView2.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        private WebView2Control _childWebView;
        private WebView2Environment _environment;
        private NewWindowRequestedEventArgs _args;
        private IWebView2Deferral _deferral;

        public PopupWindow()
        {
            InitializeComponent();
        }

        public PopupWindow(WebView2Environment environment, NewWindowRequestedEventArgs args)
            : this()
        {
            // Save the environment so we can create the webview
            _environment = environment;
            _args = args;

            // Get a deferral since we have to wait for the window creation to finish
            _deferral = _args.GetDeferral();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the webview2 creation
            _childWebView = new WebView2Control(_environment);
            _childWebView.BrowserCreated += _childWebView_BrowserCreated;
            _childWebView.DocumentTitleChanged += _childWebView_DocumentTitleChanged;

            Grid.SetRow(_childWebView, 1);
            Grid.SetColumn(_childWebView, 1);
            Grid.SetZIndex(_childWebView, 100);
            gridLayout.Children.Add(_childWebView);
        }

        private void _childWebView_DocumentTitleChanged(object sender, DocumentTitleChangedEventArgs e)
        {
            Title = string.Format("Popup {0}", _childWebView.DocumentTitle);
        }

        private void _childWebView_BrowserCreated(object sender, EventArgs e)
        {
            // Browser is created so set the webview in the args and complete
            // the deferral
            _args.NewWindow = _childWebView.InnerWebView2WebView;
            _args.Handled = true;
            _deferral.Complete();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_childWebView != null)
            {
                _childWebView.BrowserCreated -= _childWebView_BrowserCreated;
                _childWebView.DocumentTitleChanged -= _childWebView_DocumentTitleChanged;
                _childWebView.Dispose();
            }
            _environment = null;
            _args = null;

            base.OnClosed(e);
        }
    }
}
