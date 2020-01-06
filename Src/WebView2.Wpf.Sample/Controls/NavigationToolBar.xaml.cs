using System;
using System.Windows;
using System.Windows.Controls;

namespace MtrDev.WebView2.Wpf.Sample.Controls
{
    /// <summary>
    /// Interaction logic for NavigationToolBar.xaml
    /// </summary>
    public partial class NavigationToolBar : UserControl
    {
        public NavigationToolBar()
        {
            InitializeComponent();
        }

        public bool CanGoBack
        {
            get => buttonBack.IsEnabled;
            set => buttonBack.IsEnabled = value;
        }

        public bool CanGoForward
        {
            get => buttonForward.IsEnabled;
            set => buttonForward.IsEnabled = value;
        }

        public bool CanCancel
        {
            get => buttonCancel.IsEnabled;
            set => buttonCancel.IsEnabled = value;
        }

        public string Url
        {
            get => textBoxUrl.Text;
            set => textBoxUrl.Text = value;
        }

        public event EventHandler GoClicked;
        public event EventHandler BackClicked;
        public event EventHandler ForwardClicked;
        public event EventHandler CancelClicked;
        public event EventHandler ReloadClicked;

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = GoClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = BackClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonForward_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = ForwardClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonReload_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = ReloadClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = CancelClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
