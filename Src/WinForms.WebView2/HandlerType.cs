using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp
{
    internal enum HandlerType
    {
        NavigationStarting = 0,
        NavigationComplete = 1,
        ZoomFactorChanged = 2,
        WebMessageReceived = 3,
        DocumentStateChanged = 4,
        LostFocus = 5,
        FrameNavigationStarting = 6,
        MoveFocusRequested = 7,
        GotFocus = 8,
        WebResourceRequested = 9,
        ScriptDialogOpening = 10,
        PermissionRequested = 11,
        ProcessFailed = 12,

    }
}
