using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Russinsoft.WinForms
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        public static class DpiAwarenessContext
        {
            public const int UNAWARE = -1;
            public const int SYSTEM_AWARE = -2;
            public const int PER_MONITOR_AWARE = -3;
            public const int PER_MONITOR_AWARE_V2 = -4;
            public const int UNAWARE_GDISCALED = -5;
        }

        // for Windows 10 version RS2 and above
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

    }
}
