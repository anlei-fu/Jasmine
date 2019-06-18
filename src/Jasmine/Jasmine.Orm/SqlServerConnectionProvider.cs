using Jasmine.Common;
using System.Data.Common;
using System.Data.SqlClient;

namespace Jasmine.Orm
{
    public class SqlServerConnectionProvider :AbstractSimpleQueuedPool<DbConnection>, IDbConnectionProvider
    {
        public SqlServerConnectionProvider(string conntionString):base(20)
        {
            ConnctionString = conntionString;
        }
        public string ConnctionString { get; }

        public DataSource DataSource => DataSource.SqlServer;

        protected override DbConnection newInstance()
        {
            return new SqlConnection(ConnctionString);
        }
        public override void Recycle(DbConnection item)
        {
            item.Close();

            base.Recycle(item);
        }
    }

    public class SqlSeverMaxConcurrecyConnectionProvider : AbstractMaxConcurrencyPool<DbConnection>, IDbConnectionProvider
    {
        public SqlSeverMaxConcurrecyConnectionProvider(string connectionString ,int maxConcurrency) : base(maxConcurrency)
        {
            ConnctionString = connectionString;
        }

        public  DataSource DataSource => DataSource.SqlServer;
        public string ConnctionString { get; }
        protected override DbConnection newInstance()
        {
            return new SqlConnection(ConnctionString);
        }
    }

}
