using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace WebView2Sharp
{
    public class HttpRequestHeaderCollection 
    {
        private IWebView2HttpRequestHeaders _httpHeaders;
        private IDictionary<string, string> _headerNameValues;

        internal HttpRequestHeaderCollection(IWebView2HttpRequestHeaders httpHeaders)
        {
            _httpHeaders = httpHeaders;
            _headerNameValues = new Dictionary<string, string>();

            IWebView2HttpHeadersCollectionIterator iterator;
            _httpHeaders.GetIterator(out iterator);
            if (iterator != null)
            {
                int hasNext;
                iterator.MoveNext(out hasNext);
                while (hasNext != 0)
                {
                    string name;
                    string value;

                    iterator.GetCurrentHeader(out name, out value);
                    _headerNameValues.Add(name, value);
                    iterator.MoveNext(out hasNext);
                }

            }
        }

        public void SetHeader(string name, string value)
        {
            _httpHeaders.SetHeader(name, value);
            _headerNameValues.Add(name, value);
        }

        public void Remove(string name)
        {
            _httpHeaders.RemoveHeader(name);
            _headerNameValues.Remove(name);
        }

        public IReadOnlyDictionary<string, string> HeaderDictionary
        {
            get
            {
                return new ReadOnlyDictionary<string, string>(_headerNameValues);
            }
        }
    }
}
