using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Session;

namespace Goliath.Web.Session
{
    //https://github.com/NancyFx/Nancy/blob/master/src/Nancy/Session/Session.cs
    public class PersistentSession : ISession
    {
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }

        public object this[string key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool HasChanged { get; }
    }
}
