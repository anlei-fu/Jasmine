using Jasmine.Orm.Model;
using System.Collections.Generic;

namespace Jasmine.Orm.Interfaces
{
    public  interface IUnknowTypeConvertor
    {
        IEnumerable<object> Convert(QueryResultContext context);
    }
}
