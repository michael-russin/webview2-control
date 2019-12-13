using MtrDev.WebView2.Interop;
using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Demo
{
    public partial class PopupForm : Form
    {
        private WebView2Control _childWebView;
        private WebView2Environment _environment;
        private NewWindowRequestedEventArgs _args;
        private IWebView2Deferral _deferral;

        public PopupForm()
        {
            InitializeComponent();
        }

        public PopupForm(WebView2Environment environment, NewWindowRequestedEventArgs args)
        {
            // Save the environment so we can create the webview
            _environment = environment;
            _args = args;

            // Get a deferral since we have to wait for the window creation to finish
            _deferral = _args.GetDeferral();
            InitializeComponent();
        }

        private void PopupForm_Load(object sender, EventArgs e)
        {
            // Start the webview2 creation
            _childWebView = new WebView2Control(_environment);
            _childWebView.BrowserCreated += _childWebView_BrowserCreated;
            _childWebView.DocumentTitleChanged += _childWebView_DocumentTitleChanged;
            _childWebView.Dock = DockStyle.Fill;
            Controls.Add(_childWebView);
        }

        private void _childWebView_DocumentTitleChanged(object sender, DocumentTitleChangedEventArgs e)
        {
            Text = string.Format("Popup {0}", _childWebView.DocumentTitle);
        }

        private void _childWebView_BrowserCreated(object sender, EventArgs e)
        {

            IWebView2WebView wv = null;
            wv = (IWebView2WebView)_childWebView.InnerWebView2WebView;
            _args.NewWindow = wv;
            _args.Handled = true;
            _deferral.Complete();
        }
    }
}
