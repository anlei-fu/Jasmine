using System.Collections.Generic;

namespace Jasmine.Orm
{
    public  interface IUnknowTypeConvertor
    {
        IEnumerable<object> Convert(QueryResultContext context);
    }
}
