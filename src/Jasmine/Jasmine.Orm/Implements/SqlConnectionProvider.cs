using System.Collections.Concurrent;
using System.Data.SqlClient;
using Jasmine.Orm.Interfaces;

namespace Jasmine.Orm.Implements
{
    public class DefaultConnectionProvider : ISqlConnectionProvider
    {
        public DefaultConnectionProvider(string name,string connctionStr,int capacity=20)
        {
            Name = name;
            ConnectionString = connctionStr;
            Capacity = capacity;

        }

        private ConcurrentQueue<SqlConnection> _queue = new ConcurrentQueue<SqlConnection>();

        public string Name { get; }
        public string ConnectionString { get; }
        public int Capacity { get; }

        public void Recycle(SqlConnection connection)
        {
            if (_queue.Count > Capacity)
            {
                connection.Close();
                connection.Dispose();
            }
            else
            {
                _queue.Enqueue(connection);
            }
        }

        public SqlConnection Rent()
        {
            return _queue.TryDequeue(out var result) ? result : new SqlConnection(ConnectionString);
        }
    }
}
