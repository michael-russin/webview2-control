using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Forms
{
    public partial class TextInputDialog : Form
    {
        public TextInputDialog(string title, string prompt, string description, string defaultText, bool readOnly)
        {
            InitializeComponent();

            Text = title;
            labelStatic.Text = prompt;
            labelDescription.Text = description;
            textBoxInput.Text = defaultText;
            if (readOnly)
            {
                textBoxInput.ReadOnly = readOnly;
            }
        }

        public string Input
        {
            get
            {
                return textBoxInput.Text;
            }
        }
    }
}
