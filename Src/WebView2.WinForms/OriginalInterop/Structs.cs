using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Sharp.Interop
{
    public struct __MIDL___MIDL_itf_webview22Elibrary_formatted_0007_0001_0001
    {
        public uint Data1;

        public ushort Data2;

        public ushort Data3;

        public byte[] Data4;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct __MIDL_IWinTypes_0009
    {
        [FieldOffset(0)]
        public int hInproc;

        [FieldOffset(0)]
        public int hRemote;
    }

    public struct _FILETIME
    {
        public uint dwLowDateTime;

        public uint dwHighDateTime;
    }

    public struct _LARGE_INTEGER
    {
        public long QuadPart;
    }

    public struct _RemotableHandle
    {
        public int fContext;

        public __MIDL_IWinTypes_0009 u;
    }

    public struct _ULARGE_INTEGER
    {
        public ulong QuadPart;
    }

    public struct EventRegistrationToken
    {
        public long @value;
    }
    public struct GUID
    {
        public uint Data1;

        public ushort Data2;

        public ushort Data3;

        public byte[] Data4;
    }

    public struct tagRECT
    {
        public int left;

        public int top;

        public int right;

        public int bottom;
    }

    public struct tagSTATSTG
    {
        public string pwcsName;

        public uint type;

        public _ULARGE_INTEGER cbSize;

        public _FILETIME mtime;

        public _FILETIME ctime;

        public _FILETIME atime;

        public uint grfMode;

        public uint grfLocksSupported;

        [ComAliasName("WebView2.Interop.GUID")]
        public GUID clsid;

        public uint grfStateBits;

        public uint reserved;
    }
}
