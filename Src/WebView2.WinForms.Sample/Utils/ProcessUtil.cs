using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtrDev.WebView2.WinForms.Sample.Utils
{
    internal class ProcessUtil
    {
        internal static void EnsureProcessIsClosed(uint processId, int timeoutMs)
        {
            if (processId != 0)
            {
                Process process = Process.GetProcessById((int)processId);
                if (process != null)
                {
                    if (!process.WaitForExit(timeoutMs))
                    {
                        // Force kill the process if it doesn't exit by itself
                        process.Kill();
                    }
                }
            }
        }
    }
}
