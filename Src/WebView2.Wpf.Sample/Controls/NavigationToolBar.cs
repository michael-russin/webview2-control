using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtrDev.WebView2.Sample.Common.Controls;

namespace MtrDev.WebView2.WinForms.Sample.Controls
{
    public partial class NavigationToolBar : UserControl, INavigationToolBar
    {
        public NavigationToolBar()
        {
            InitializeComponent();
        }

        public bool CanGoBack
        {
            get => buttonBack.Enabled;
            set => buttonBack.Enabled = value;
        }

        public bool CanGoForward
        {
            get => buttonForward.Enabled;
            set => buttonForward.Enabled = value;
        }

        public bool CanCancel
        {
            get => buttonCancel.Enabled;
            set => buttonCancel.Enabled = value;
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

        private void buttonGo_Click(object sender, EventArgs e)
        {
            EventHandler handler = GoClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            EventHandler handler = BackClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            EventHandler handler = ForwardClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            EventHandler handler = ReloadClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            EventHandler handler = CancelClicked;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
