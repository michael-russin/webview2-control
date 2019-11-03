using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class ZoomFactorCompletedEventArgs
    {
        public ZoomFactorCompletedEventArgs(object args)
        {
        }

        public double Factor { get; private set; }
    }
}
