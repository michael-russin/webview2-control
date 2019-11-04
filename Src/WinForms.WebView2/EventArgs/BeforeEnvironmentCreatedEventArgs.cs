using System;

namespace Russinsoft.WinForms
{
    public class BeforeEnvironmentCreatedEventArgs : EventArgs
    {
        public string BrowserExecutableFolder { get; set; }

        public string UserDataFolder { get; set; }

        public string BrowserArguments { get; set; }
    }
}
