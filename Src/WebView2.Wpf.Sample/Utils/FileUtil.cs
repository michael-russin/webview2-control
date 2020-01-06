using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MtrDev.WebView2.WinForms.Sample.Utils
{
    public class FileUtil
    {
        public static string GetLocalUri(string relativePath)
        {
            string path = GetLocalPath(relativePath);

            Uri uri = new Uri(path);

            return uri.AbsoluteUri;
        }

        public static string GetLocalPath(string relativePath)
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            string fulllName = new FileInfo(location.AbsolutePath).Directory.FullName;

            return Path.Combine(fulllName, relativePath);
        }
    }
}
