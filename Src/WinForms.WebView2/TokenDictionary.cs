using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Russinsoft.WebView2.Interop;

namespace Russinsoft.WinForms
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
