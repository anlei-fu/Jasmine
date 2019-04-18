using System.Data.Common;

namespace Jasmine.Orm.Interfaces
{
    public interface ISqlConnectionProvider
    {
        DbConnection Rent();
        void Recycle(DbConnection connection);
    }
}
