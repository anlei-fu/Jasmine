using Jasmine.Common;
using System.Data.Common;

namespace Jasmine.Orm.Interfaces
{
    public interface IDbConnectionProvider:IPool<DbConnection>
    {
       
    }
}
