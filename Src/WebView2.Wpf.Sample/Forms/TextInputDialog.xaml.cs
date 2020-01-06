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

namespace MtrDev.WebView2.Wpf.Sample.Forms
{
    /// <summary>
    /// Interaction logic for TextInputDialog.xaml
    /// </summary>
    public partial class TextInputDialog : Window
    {
        public TextInputDialog() :this(string.Empty, string.Empty, string.Empty, string.Empty, true)
        {
        }

        public TextInputDialog(string title, string prompt, string description, string defaultText, bool readOnly)
        {
            InitializeComponent();

            Title= title;
            labelStatic.Header = prompt;
            labelDescription.Text = description;
            textBoxInput.Text = defaultText;
            if (readOnly)
            {
                textBoxInput.IsReadOnly = readOnly;
            }
        }

        public string Input
        {
            get
            {
                return textBoxInput.Text;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
