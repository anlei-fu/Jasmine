using Jasmine.Orm.Interfaces;
using System.Collections.Generic;

namespace Jasmine.Orm
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
