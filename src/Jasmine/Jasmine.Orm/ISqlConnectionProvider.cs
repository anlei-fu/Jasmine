using System.Data.Common;

namespace Jasmine.Orm.Interfaces
{
    public interface IDbConnectionProvider
    {
        DbConnection Rent();
        void Recycle(DbConnection connection);
    }
}
