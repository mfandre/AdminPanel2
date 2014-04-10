using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnitTest.Utils.Session
{
    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get
            {
                if (m_SessionStorage.ContainsKey(name))
                    return m_SessionStorage[name];

                return null;
            }
            set { m_SessionStorage[name] = value; }
        }

        public override void Add(string name, object value)
        {

            if (m_SessionStorage.ContainsKey(name))
                m_SessionStorage[name] = value;
            else
                m_SessionStorage.Add(name, value);
        }
    }
}