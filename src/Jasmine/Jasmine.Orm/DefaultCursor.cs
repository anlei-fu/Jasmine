using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Jasmine.Orm.Implements
{
    public class DefaultCursor : ICursor
    {
        public DefaultCursor(QueryResultContext context, IQueryResultResolverProvider sqlConvertorProvider, SqlConnection connection, IDbConnectionProvider provider)
        {
            _context = context;
            _provider = provider;
            _connection = connection;
            _convertorProvider = sqlConvertorProvider;
        }
        private SqlConnection _connection;
        private QueryResultContext _context;
        private IDbConnectionProvider _provider;
        private IQueryResultResolverProvider _convertorProvider;
        private IUnknowTypeConvertor _unknowTypeConvertor;
        public bool Closed { get; private set; }

        public ConcurrentDictionary<string,QuryResultColumnInfo> Columns => _context.ResultTable.Columns;

      

        public void Close()
        {
            Closed = true;
            _context.Reader.Close();
            _provider.Recycle(_connection);
        }

        public IEnumerable<T> Read<T>(int count)
        {
            var result = new List<T>();

            var type = typeof(T);

            var t = 0;

            var convertor = _convertorProvider.GetResolver(type);


           

            return result;
        }


        public T ReadOne<T>()
        {
            return default;
        }

       
        public Task<T> ReadOneAsync<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAsync<T>(int count)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ReadToEnd<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadToEndAsync<T>()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
