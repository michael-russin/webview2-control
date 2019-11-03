using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebView2Sharp.Events;

namespace WebView2Sharp
{
    public class WebView2Control : WebView2ControlBase
    {
        public WebView2Control()
        {
            BackColor = SystemColors.ControlDark;
        }

        public WebView2Control(WebViewEnvironment webViewEnvironment) :
            base(webViewEnvironment)
        {
            BackColor = SystemColors.ControlDark;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeWebView();
        }

        [
            Bindable(true),
            DefaultValue(null)
        ]       
        public string Url
        {
            get
            {
                return InternalUrl;
            }
            set
            {
                if (value != null && value.ToString() == "")
                {
                    value = null;
                }
                InternalNavigate(value);
            }
        }

        public event EventHandler<BeforeEnvironmentCreatedEventArgs> BeforeEnvironmentCreated;

        protected override void OnBeforeEnvironmentCreated(BeforeEnvironmentCreatedEventArgs e)
        {
            if (BeforeEnvironmentCreated != null)
            {
                BeforeEnvironmentCreated(this, e);
            }
        }

        public event EventHandler<EnvironmentCreatedEventArgs> EnvironmentCreated;

        protected override void OnEnvironmentCreated(EnvironmentCreatedEventArgs e)
        {
            if (EnvironmentCreated != null)
            {
                EnvironmentCreated(this, e);
            }
        }

        public event EventHandler<EventArgs> BrowserCreated;

        protected override void OnBrowserCreated(EventArgs e)
        {
            if (BrowserCreated != null)
            {
                BrowserCreated(this, e);
            }
        }

        public event EventHandler<NavigationStartingEventArgs> NavigationStarting;

        protected override void OnNavigationStarting(NavigationStartingEventArgs e)
        {
            if (NavigationStarting != null)
            {
                NavigationStarting(this, e);
            }
        }

        public event EventHandler<NavigationCompletedEventArgs> NavigationCompleted;

        protected override void OnNavigationCompleted(NavigationCompletedEventArgs e)
        {
            if (NavigationCompleted != null)
            {
                NavigationCompleted(this, e);
            }
        }

        public event EventHandler<ZoomFactorCompletedEventArgs> ZoomFactorChanged;

        protected override void OnZoomFactorChanged(ZoomFactorCompletedEventArgs e)
        {
            if (ZoomFactorChanged != null)
            {
                ZoomFactorChanged(this, e);
            }
        }

        public event EventHandler<WebMessageReceivedEventArgs> WebMessageRecieved;

        protected override void OnWebMessageRecieved(WebMessageReceivedEventArgs e)
        {
            if (WebMessageRecieved != null)
            {
                WebMessageRecieved(this, e);
            }
        }

        public event EventHandler<DocumentStateChangedEventArgs> DocumentStateChanged;

        protected override void OnDocumentStateChanged(DocumentStateChangedEventArgs e)
        {
            if (DocumentStateChanged != null)
            {
                DocumentStateChanged(this, e);
            }
        }
    }

}
