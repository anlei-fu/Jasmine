using System.Collections.Generic;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;

namespace Jasmine.Orm.Implements
{
    public class DefaultUnkowTypeConvertor : IUnknowTypeConvertor
    {

        private DefaultUnkowTypeConvertor()
        {

        }
        public static readonly IUnknowTypeConvertor Instance = new DefaultUnkowTypeConvertor();
        public IEnumerable<object> Convert(QueryResultContext context)
        {
            var result = new List<object>();

        

            return result;
        }
    }
}
