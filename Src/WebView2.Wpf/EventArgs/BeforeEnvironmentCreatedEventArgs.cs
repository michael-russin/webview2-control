using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtrDev.WebView2.Wpf
{
    /// <summary>
    /// 
    /// </summary>
    public class BeforeEnvironmentCreatedEventArgs : EventArgs
    {
        public string BrowserExecutableFolder { get; set; }

        public string UserDataFolder { get; set; }

        public string BrowserArguments { get; set; }
    }
}
