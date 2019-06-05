using Jasmine.Configuration;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Orm.Implements
{
    public class JasmineSqlExcutor : ISqlExcuter
    {
        private ITableTemplateCacheProvider _templateProvider;
        private SqltemplateConverter _templateConverter;
        private IDbConnectionProvider _connectionProvider;
        public int BatchInsert<T>(IEnumerable<T> data, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertAsync<T>(IEnumerable<T> data, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int BatchInsertPartial<T>(string[] columns, IEnumerable<T> data, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertPartialAsync<T>(string[] columns, IEnumerable<T> data, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int BatchInsertPartialWith<T>(string table, string[] columns, IEnumerable<T> datas, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertPartialWithAsync<T>(string table, string[] columns, IEnumerable<T> datas, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int BatchInsertWith<T>(string table, IEnumerable<T> datas, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertWithAsync<T>(string table, IEnumerable<T> datas, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Create<T>()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync<T>()
        {
            throw new NotImplementedException();
        }

        public int CreateWith<T>(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateWithAsync<T>(string name)
        {
            throw new NotImplementedException();
        }

        public int Delete(string table, string condition, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(string condition, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string table, string condition, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(string condition, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Drop(string name)
        {
            throw new NotImplementedException();
        }

        public int Drop<T>()
        {
            throw new NotImplementedException();
        }

        public Task<int> DropAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> DropAsync<T>()
        {
            throw new NotImplementedException();
        }

        public int Excute(string sql, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Excute(string template, object obj, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Excute(Template template, object obj, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExcuteAsync(string sql, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExcuteAsync(string template, object obj, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExcuteAsync(Template template, object obj, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T data)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync<T>(T data)
        {
            throw new NotImplementedException();
        }

        public int InsertPartial<T>(string template, T data)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertPartialAsync<T>(string template, T data)
        {
            throw new NotImplementedException();
        }

        public int InsertPartialWith<T>(string table, string template, T data)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertPartialWithAsync<T>(string table, string template, T data)
        {
            throw new NotImplementedException();
        }

        public int InsertWith<T>(string table, T data)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertWithAsync<T>(string table, T data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            var connection = _connectionProvider.Rent();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                var context = new QueryResultContext(command.ExecuteReader());

                var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver<T>();

                var ls = new List<T>();

                while (context.Reader.Read())
                {
                    ls.Add((T)resolver.Resolve(context, typeof(T)));
                }

                return ls;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _connectionProvider.Recycle(connection);
            }
        }

        public IEnumerable<T> Query<T>(string template, object obj)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return Query<T>(_templateConverter.Convert(tpt, obj));
        }

        public IEnumerable<T> Query<T>(SqlTemplate template, object obj)
        {
            return Query<T>(_templateConverter.Convert(template, obj));
        }

        private string convert(SqlTemplate template, object parameter)
        {
            return _templateConverter.Convert(template, parameter);
        }
        public IEnumerable<T> Query<T>()
        {
            return Query<T>(convert(_templateProvider.GetCache<T>().GetQuery(), null));
        }

        public IEnumerable<T> QueryAsync<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string template, object obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync<T>(Template template, object obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryConditional<T>(string condition)
        {
            return Query<T>(convert(_templateProvider.GetCache<T>().GetQueryConditional(condition), null));
        }

        public Task<IEnumerable<T>> QueryConditionalAsync<T>(string condition)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryConditionalCursor<T>(string condition)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalCursorAsync<T>(string condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryConditionalOrderByAsc<T>(string condition, string orderBy)
        {
            return Query<T>(convert(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition,orderBy), null));
        }

        public Task<IEnumerable<T>> QueryConditionalOrderByAscAsync<T>(string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryConditionalOrderByAscCursor<T>(string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalOrderByAscCursorAsync<T>(string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(string template, object obj)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(Template template, object obj)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>()
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryCursorAsyc<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryCursorAsync<T>(string template, object obj)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryCursorAsync<T>(Template template, object obj)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryCursorAsync<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryOrderByAsc<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryOrderByAscAsync<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryOrderByAscCursor<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryOrderByAscCursorAsync<T>(string orderBy)
        {
            return Query<T>(convert(_templateProvider.GetCache<T>().GetQueryOrderByAsc(orderBy), null));
        }

        public IEnumerable<T> QueryOrderByAscWith<T>(string table, string orderBy)
        {
            return Query<T>(convert(_templateProvider.GetCache<T>().GetQueryOrderByAscWith(condition, orderBy), null));
        }

        public Task<IEnumerable<T>> QueryOrderByAscWithAsync<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryOrderByAscWithCursor<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryOrderByAscWithCursorAsync<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryOrderByDesc<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryOrderByDescAsync<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryOrderByDescCursor<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryOrderByDescCursorAsync<T>(string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryOrderByDescWith<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryOrderByDescWithAsync<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryOrderByDescWithCursor<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryOrderByDescWithCursorAsync<T>(string table, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartial<T>(params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialAsync<T>(params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditional<T>(string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalAsync<T>(string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalCursor<T>(string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalCursorAsync<T>(string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAsc<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscAsync<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalOrderByAscCursor<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscCursorAsync<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAscWith<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscWithAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalOrderByAscWithCursor<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDesc<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescAsync<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalOrderByDescCursor<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescCursorAsync<T>(string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDescWith<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescWithAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalOrderByDescWithCursor<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialConditionalWith<T>(string table, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialConditionalWithAsync<T>(string table, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialConditionalWithCursor<T>(string table, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialConditionalWithCursorAsync<T>(string table, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialCursor<T>(params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialCursorAsync<T>(params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialOrderByAsc<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialOrderByAscAsync<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialOrderByAscCursor<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialOrderByAscCursorAsync<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialOrderByAscWith<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialOrderByAscWithAsync<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialOrderByAscWithCursor<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialOrderByAscWithCursorAsync<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialOrderByDesc<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialOrderByDescAsync<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialOrderByDescCursor<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialOrderByDescCursorAsync<T>(string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialOrderByDescWith<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialOrderByDescWithAsync<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialOrderByDescWithCursor<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialOrderByDescWithCursorAsync<T>(string table, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPartialWith<T>(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryPartialWithAsync<T>(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryPartialWithCursor<T>(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryPartialWithCursorAsync<T>(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTop<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopAsync<T>(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditional<T>(int count, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalAsync<T>(int count, string condition)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalCursor<T>(int count, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalCursorAsync<T>(int count, string condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditionalOrderByAsc<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscAsync<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalOrderByAscCursor<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalOrderByAscCursorAsync<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditionalOrderByAscWith<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscWithAsync<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalOrderByAscWithCursor<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditionalOrderByDesc<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescAsync<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalOrderByDescCursor<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalOrderByDescCursorAsync<T>(int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditionalOrderByDescWith<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescWithAsync<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalOrderByDescWithCursor<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopConditionalWith<T>(string table, int count, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopConditionalWithAsync<T>(string table, int count, string condition)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopConditionalWithCursor<T>(string table, int count, string condition)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopConditionalWithCursorAsync<T>(string table, int count, string condition)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopCursor<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopCursorAsync<T>(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopOrderByAsc<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopOrderByAscAsync<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopOrderByAscCursor<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopOrderByAscCursorAsync<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopOrderByAscWith<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopOrderByAscWithAsync<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopOrderByAscWithCursor<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopOrderByAscWithCursorAsync<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopOrderByDesc<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopOrderByDescAsync<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopOrderByDescCursor<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopOrderByDescCursorAsync<T>(int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopOrderByDescWith<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopOrderByDescWithAsync<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopOrderByDescWithCursor<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopOrderByDescWithCursorAsync<T>(string table, int count, string orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartial<T>(int count, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialAsync<T>(int count, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditional<T>(int count, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalAsync<T>(int count, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalCursor<T>(int count, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalCursorAsync<T>(int count, string condition, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAsc<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalOrderByAscCursor<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscCursorAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAscWith<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalOrderByAscWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDesc<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalOrderByDescCursor<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescCursorAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDescWith<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalOrderByDescWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialConditionalWith<T>(string table, int count, string conditiona, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalWithAsync<T>(string table, int count, string conditiona, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialConditionalWithCursor<T>(string table, int count, string conditiona, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialConditionalWithCursorAsync<T>(string table, int count, string conditiona, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialCursor<T>(int count, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialCursorAsync<T>(int count, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialOrderByAsc<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByAscAsync<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialOrderByAscCursor<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialOrderByAscCursorAsync<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialOrderByAscWith<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByAscWithAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialOrderByAscWithCursor<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialOrderByAscWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialOrderByDesc<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByDescAsync<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialOrderByDescCursor<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialOrderByDescCursorAsync<T>(int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialOrderByDescWith<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByDescWithAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialOrderByDescWithCursor<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialOrderByDescWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopPartialWith<T>(string table, int count, params string[] column)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopPartialWithAsync<T>(string table, int count, params string[] column)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopPartialWithCursor<T>(string table, int count, params string[] column)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopPartialWithCursorAsync<T>(string table, int count, params string[] column)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryTopWith<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryTopWithAsync<T>(int count)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryTopWithCursor<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryTopWithCursorAsync<T>(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryWith<T>(string table)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryWithAsync<T>(string table)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryWithCursor<T>(string table)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryWithCursorAsync<T>(string table)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(object parameter)
        {
            throw new NotImplementedException();
        }

        public int UpdateConditional<T>(string condition, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateConditionalAsync<T>(string condition, object parameter)
        {
            throw new NotImplementedException();
        }

        public int UpdateConditionalWidth<T>(string table, string condition, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateConditionalWidthAsync<T>(string table, string condition, object parameter)
        {
            throw new NotImplementedException();
        }

        public int UpdateWith<T>(string table, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateWithAsync<T>(string table, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
