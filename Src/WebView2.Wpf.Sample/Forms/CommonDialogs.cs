
using System.Windows;

namespace MtrDev.WebView2.WinForms.Sample.Forms
{
    public class CommonDialogs
    {
        public static void ShowFailure(int hr, string message)
        {
            string formattedMessage = message + " " + hr.ToString("X");
            MessageBox.Show(formattedMessage, "Error", MessageBoxButton.OK);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
