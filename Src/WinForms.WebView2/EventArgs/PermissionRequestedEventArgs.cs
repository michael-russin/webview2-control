using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class PermissionRequestedEventArgs : EventArgs, IWebView2PermissionRequestedEventArgs
    {
        private IWebView2PermissionRequestedEventArgs _args;

        internal PermissionRequestedEventArgs(IWebView2PermissionRequestedEventArgs args)
        {
            _args = args;
        }

        public string Uri
        {
            get => _args.Uri;
        }

        public WEBVIEW2_PERMISSION_TYPE PermissionType
        {
            get => _args.PermissionType;
        }

        public bool IsUserInitiated
        {
            get => _args.IsUserInitiated;
        }

        public WEBVIEW2_PERMISSION_STATE State
        {
            get => _args.State;
            set => _args.State = value;
        }

        public IWebView2Deferral GetDeferral()
        {
            return new Deferral(_args.GetDeferral());
        }
    }
}
