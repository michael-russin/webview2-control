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
using MtrDev.WebView2.Interop;

namespace MtrDev.WinForms
{
    internal class TokenDictionary<A, H> where H : new()
    {
        private IDictionary<long, Action<A>> _tokenActionDictionary =
            new Dictionary<long, Action<A>>();

        public long Register(Action<A> callback, Func<EventRegistrationToken> registerFunc)
        {
            H completedHandler = new H();

            EventRegistrationToken token = registerFunc();
            _tokenActionDictionary.Add(token.value, callback);
            return token.value;
        }

        public void Unregister(long token, Action<EventRegistrationToken> action)
        {
            if (_tokenActionDictionary.ContainsKey(token))
            {
                _tokenActionDictionary.Remove(token);
            }
            EventRegistrationToken registrationToken = new EventRegistrationToken();
            registrationToken.value = token;

            action.Invoke(registrationToken);
        }
    }
}
