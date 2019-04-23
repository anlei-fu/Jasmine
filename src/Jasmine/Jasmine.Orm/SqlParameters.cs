using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class SqlParameters
    {
        private IDictionary<string, object> _dic;
        public static SqlParameters  Create(object obj)
        {
            return null;
        }
        public static SqlParameters Create(IDictionary<string,object> dic)
        {
            return null;
        }
        public object GetValue(string name)
        {
            return null;
        }
        public void SetValue(string name,object value)
        {

        }
    }
}
