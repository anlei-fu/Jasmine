using Jasmine.Common;
using Jasmine.Orm.Model;
using System.Data.Common;

namespace Jasmine.Orm
{
    public interface IDbConnectionProvider:IPool<DbConnection>
    {
       DataSource DataSource { get; }
    }
}
