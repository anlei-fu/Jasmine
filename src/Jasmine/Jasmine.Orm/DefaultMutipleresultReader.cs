using System.Data.Common;
using System.Threading.Tasks;

namespace Jasmine.Orm
{
    public class DefaultMutipleResultReader : IMutipleResultReader
    {

        public DefaultMutipleResultReader(ISqlExcuter excutor,DbDataReader reader,DbConnection connection,IDbConnectionProvider provider)
        {
            _reader = reader;
            _connection = connection;
            _provider = provider;
            _excutor = excutor;
        }
        private IDbConnectionProvider _provider;
        private DbConnection _connection;
        private DbDataReader _reader;
        private ISqlExcuter _excutor;

        private readonly object _locker = new object(); 
        public bool Closed { get; private set; }

        public void Close()
        {
           lock(_locker)
            {
                if(!Closed)
                {
                    Closed = true;
                }

                _reader.Close();

                _provider.Recycle(_connection);
            }
        }

        public ICursor NextResult()
        {
            return _reader.NextResult() ? new MutilpleResultReaderCursor(new QueryResultContext(_excutor,_reader,_connection,_provider)) :
                                          null;
        }

        public async Task<ICursor> NextResultAsync()
        {
            return await _reader.NextResultAsync() ? new MutilpleResultReaderCursor(new QueryResultContext(_excutor,_reader,_connection,_provider)) : null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
