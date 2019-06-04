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
        public DefaultCursor(QueryResultContext context, SqlConnection connection, IDbConnectionProvider provider)
        {
            _context = context;
            _provider = provider;
            _connection = connection;

        }
        private SqlConnection _connection;
        private QueryResultContext _context;
        private IDbConnectionProvider _provider;
        public bool Closed { get; private set; }

        public ConcurrentDictionary<string, QuryResultColumnInfo> Columns => _context.ResultTable.Columns;



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

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            while (t++ <= count && _context.Reader.Read())
            {
                result.Add((T)resolver.Resolve(_context, type));
            }

            return result;
        }


        public T ReadOne<T>()
        {
            var type = typeof(T);

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            return _context.Reader.Read() ? (T)resolver.Resolve(_context, type)
                                          : default(T);
        }


        public async Task<T> ReadOneAsync<T>()
        {
            var type = typeof(T);

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            return await _context.Reader.ReadAsync().ConfigureAwait(false) ? (T)resolver.Resolve(_context, type)
                                                                           : default(T);
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(int count)
        {
            var result = new List<T>();

            var type = typeof(T);

            var t = 0;

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            while (t++ <= count && await _context.Reader.ReadAsync().ConfigureAwait(false))
            {
                result.Add((T)resolver.Resolve(_context, type));
            }

            return result;
        }

        public IEnumerable<T> ReadToEnd<T>()
        {
            var result = new List<T>();

            var type = typeof(T);

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            while (_context.Reader.Read())
            {
                result.Add((T)resolver.Resolve(_context, type));
            }

            return result;
        }

        public async Task<IEnumerable<T>> ReadToEndAsync<T>()
        {
            var result = new List<T>();

            var type = typeof(T);

            var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver(type);

            while (await _context.Reader.ReadAsync().ConfigureAwait(false))
            {
                result.Add((T)resolver.Resolve(_context, type));
            }

            return result;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
