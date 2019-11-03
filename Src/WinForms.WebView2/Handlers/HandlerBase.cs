using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Handlers
{
    internal class HandlerBase<T>
    {
        public HandlerBase(Action<T> callback)
        {
            Callback = callback;
        }

        protected Action<T> Callback { get; private set; }
    }
}
