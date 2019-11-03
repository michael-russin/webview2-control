using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp.Events
{
    public class ScriptDialogOpeningEventArgs : EventArgs, IWebView2ScriptDialogOpeningEventArgs
    {
        IWebView2ScriptDialogOpeningEventArgs _args;

        internal ScriptDialogOpeningEventArgs(IWebView2ScriptDialogOpeningEventArgs args)
        {
            _args = args;
        }

        public string Uri
        {
            get => _args.Uri; 
        }

        public WEBVIEW2_SCRIPT_DIALOG_KIND Kind
        {
            get => _args.Kind;
        }

        public string Message
        {
            get => _args.Message;
        }

        public void Accept()
        {
            _args.Accept();
        }

        public string DefaultText
        {
            get => _args.DefaultText;
        }

        public string ResultText
        {
            get => _args.ResultText;
            set => _args.ResultText = value;
        }

        public Deferral GetDeferral()
        {
            return new Deferral(_args.GetDeferral());
        }

        IWebView2Deferral IWebView2ScriptDialogOpeningEventArgs.GetDeferral()
        {
            throw new NotImplementedException();
        }
    }
}
