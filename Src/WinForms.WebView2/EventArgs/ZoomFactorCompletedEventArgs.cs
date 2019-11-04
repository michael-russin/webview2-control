using System;

namespace Russinsoft.WinForms
{
    public class ZoomFactorCompletedEventArgs : EventArgs
    {
        public ZoomFactorCompletedEventArgs(object args)
        {
        }

        public double Factor { get; private set; }
    }
}
