using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtrDev.WebView2.WinForms.Sample.Forms
{
    public class CommonDialogs
    {
        public static void ShowFailure(int hr, string message)
        {
            string formattedMessage = message + " " + hr.ToString("H");
            MessageBox.Show(formattedMessage, "Erorr", MessageBoxButtons.OK);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
