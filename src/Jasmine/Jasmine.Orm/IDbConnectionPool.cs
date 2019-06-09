using Jasmine.Common;
using System.Data.Common;

namespace Jasmine.Orm
{
    public interface IDbConnectionProvider:IPool<DbConnection>
    {
       
    }
}
