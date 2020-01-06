using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.WinForms.Sample.Components;
using MtrDev.WebView2.WinForms.Sample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MtrDev.WebView2.WinForms.Sample.Scenarios
{
    public class ScenarioAddRemoteObject : ComponentBase
    {
        private MainForm _parent;
        private WebView2Control _webView2;
        private object _remoteObject;

        string _samplePath = "Scenarios\\ScenarioAddRemoteObject.html";
        string _sampleUri;

        public ScenarioAddRemoteObject(MainForm parent, WebView2Control webView2)
        {
            _parent = parent;
            _webView2 = webView2;

            _sampleUri = FileUtil.GetLocalUri(_samplePath);

            _webView2.IsWebMessageEnabled = true;

            _webView2.NavigationStarting += _webView2_NavigationStarting;

            // Changes to IWebView2Settings::IsWebMessageEnabled apply to the next document
            // to which we navigate.
            _webView2.Navigate(_sampleUri);
        }

        private void _webView2_NavigationStarting(object sender, Wrapper.NavigationStartingEventArgs e)
        {
            string navigationTargetUri = e.Uri;

            if (_sampleUri == navigationTargetUri)
            {
                //! [AddRemoteObject]
                //                _remoteObject = new RemoteObjectSampleNet();

                string progId = "RemoteComObjectImpl.1";
                Type comType = Type.GetTypeFromProgID(progId, true);
                //Guid clsId = new Guid("19C0E72A-9D34-4F10-A92E-1119F53D1645");
                //Type comType = Type.GetTypeFromCLSID(clsId, true);
                _remoteObject = Activator.CreateInstance(comType);

                //                VARIANT remoteObjectAsVariant = { };
                //                m_remoteObject.query_to<IDispatch>(&remoteObjectAsVariant.pdispVal);
                //                remoteObjectAsVariant.vt = VT_DISPATCH;

                // We can call AddRemoteObject multiple times in a row without
                // calling RemoveRemoteObject first. This will replace the previous object
                // with the new object. In our case this is the same object and everything
                // is fine.
                _webView2.AddRemoteObject("sample", ref _remoteObject);
//                remoteObjectAsVariant.pdispVal->Release();
                //! [AddRemoteObject]
            }
            else
            {
                // We can call RemoveRemoteObject multiple times in a row without
                // calling AddRemoteObject first. This will produce an error result
                // so we ignore the failure.
                _webView2.RemoveRemoteObject("sample");

                // When we navigate elsewhere we're off of the sample
                // scenario page and so should remove the scenario.
                _parent.DeleteComponent(this);
            }

        }

        public override void CleanUp()
        {
            _webView2 = null;
            _parent = null;
        }

    }
}
