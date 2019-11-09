#region License
// Copyright (c) 2019 Michael T. Russin
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtrDev.WebView2.Interop;

namespace MtrDev.WinForms
{
    public class WebView2HttpRequestHeaderCollection 
    {
        private IWebView2HttpRequestHeaders _httpHeaders;
        private IDictionary<string, string> _headerNameValues;

        internal WebView2HttpRequestHeaderCollection(IWebView2HttpRequestHeaders httpHeaders)
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
