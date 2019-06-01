using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public bool HasRow => _context.Reader.HasRows;

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


            while (_context.Reader.HasRows)
            {
                if (t++ > count && !_context.Reader.Read())
                {
                    break;
                }
                else
                {
                    result.Add((T)convertor.Resolve(_context, type));
                }
            }

            return result;
        }

        public IEnumerable<IEnumerable<object>> Read(int count)
        {
            var t = 0;

            var result = new List<IEnumerable<object>>();

            while (_context.Reader.HasRows)
            {
                if (t++ > count && !_context.Reader.Read())
                {
                    break;
                }
                else
                {
                    result.Add(_unknowTypeConvertor.Convert(_context));
                }
            }

            return result;
        }

        public T ReadOne<T>()
        {
            return _context.Reader.HasRows && _context.Reader.Read() ?
                                                (T)_convertorProvider.GetResolver(typeof(T)).Resolve(_context, typeof(T)) : default(T);

        }

        public IEnumerable<object> ReadOne()
        {
            return _context.Reader.HasRows && _context.Reader.Read() ?
                               _unknowTypeConvertor.Convert(_context) : null;
        }
    }
}
