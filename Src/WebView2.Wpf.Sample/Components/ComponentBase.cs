
using System.Windows.Input;

namespace MtrDev.WebView2.WinForms.Sample.Components
{
    public abstract class ComponentBase
    {
        public abstract void CleanUp();

        public abstract void RunCommand(ICommand command, ExecutedRoutedEventArgs args);
    }
}
