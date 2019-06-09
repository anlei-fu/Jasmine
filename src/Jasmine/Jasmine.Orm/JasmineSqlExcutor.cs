using Jasmine.Extensions;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Orm
{
    public class JasmineSqlExcutor : ISqlExcuter
    {
        private ITableTemplateCacheProvider _templateProvider;
        private SqltemplateConverter _templateConverter;
        private IDbConnectionProvider _connectionProvider;
        private ITableMetaDataProvider _metaDataProvider;
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

        public Task<int> BatchInsertPartialAsync<T>( IEnumerable<T> data, bool transanction, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public int BatchInsertPartialWith<T>(string table, string[] columns, IEnumerable<T> datas, bool transanction = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchInsertPartialWithAsync<T>(string table,  IEnumerable<T> datas, bool transanction , params string[] columns)
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
            return Excute(_templateProvider.GetCache<T>().GetCreate());
        }

        public Task<int> CreateAsync<T>()
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetCreate());
        }

        public int CreateWith<T>(string table)
        {
            return Excute(_templateProvider.GetCache<T>().GetCreateWith(table));
        }

        public Task<int> CreateWithAsync<T>(string table)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetCreateWith(table));
        }

        public int Delete(string table, string condition, bool transanction = false)
        {
            return Excute($" Delete From {table} Where {condition}", transanction);
        }

        public int Delete<T>(string condition, bool transanction = false)
        {
            return Excute(_templateProvider.GetCache<T>().GetDelete(condition), transanction);
        }

        public Task<int> DeleteAsync(string table, string condition, bool transanction = false)
        {
            return ExcuteAsync($" Delete From {table} Where {condition}", transanction);
        }

        public Task<int> DeleteAsync<T>(string condition, bool transanction = false)
        {
            return ExcuteAsync($" Delete From {_metaDataProvider.GetTable(typeof(T)).Name} Where {condition}", transanction);
        }

        public int Drop(string name)
        {
            return Excute($" Drop Table {name}");
        }

        public int Drop<T>()
        {
            return Excute($" Drop Table {_metaDataProvider.GetTable(typeof(T)).Name}");
        }

        public Task<int> DropAsync(string name)
        {
            return ExcuteAsync($" Drop Table {name}");
        }

        public Task<int> DropAsync<T>()
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetDrop());
        }

        public int Excute(string sql, bool transanction = false)
        {
            if (transanction)
                return ExcuteTransanction(sql);

            var connection = _connectionProvider.Rent();

            
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                return command.ExecuteNonQuery();
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

        private int ExcuteTransanction(string sql)
        {
            var connection = _connectionProvider.Rent();

            DbTransaction transanction = null;

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                transanction = connection.BeginTransaction();

                transanction.Commit();

                return 1;
            }
            catch (Exception)
            {
                if (transanction != null)
                    transanction.Rollback();
                
                throw;
            }
            finally
            {
                _connectionProvider.Recycle(connection);
            }
        }

        public int Excute(string template, object obj, bool transanction = false)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return Excute(tpt, obj, transanction);
        }

        public int Excute(SqlTemplate template, object obj, bool transanction = false)
        {
            return Excute(template, obj, transanction);
        }

        public async Task<int> ExcuteAsync(string sql, bool transanction = false)
        {
            if (transanction)
                return await ExcuteTransanctionAsync(sql);

            var connection = _connectionProvider.Rent();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                   await  connection.OpenAsync();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                return await command.ExecuteNonQueryAsync();
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


        private async Task<int> ExcuteTransanctionAsync(string sql)
        {
            var connection = _connectionProvider.Rent();

            DbTransaction transanction = null;
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                   await connection.OpenAsync().ConfigureAwait(false);
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                transanction = connection.BeginTransaction();

                transanction.Commit();

                return 1;
            }
            catch (Exception)
            {
                if (transanction != null)
                    transanction.Rollback();

                throw;
            }
            finally
            {
                _connectionProvider.Recycle(connection);
            }
        }

        public Task<int> ExcuteAsync(string template, object obj, bool transanction = false)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return ExcuteAsync(tpt,obj, transanction);
        }

        public Task<int> ExcuteAsync(SqlTemplate template, object obj, bool transanction = false)
        {
            return ExcuteAsync(template, obj, transanction);
        }

        public int Insert<T>(T data)
        {
            return Excute(_templateProvider.GetCache<T>().GetInsert(), data);
        }

        public Task<int> InsertAsync<T>(T data)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetInsert(), data);
        }

        public int InsertPartial<T>( T data,params string[] columns)
        {
            return Excute(_templateProvider.GetCache<T>().GetInsertPartial(columns), data);
        }

        public Task<int> InsertPartialAsync<T>( T data, params string[] columns)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetInsertPartial(columns), data);
        }

        public int InsertPartialWith<T>(string table, T data, params string[] columns)
        {
            return Excute(_templateProvider.GetCache<T>().GetInsertPartialWith(table,columns), data);
        }

        public Task<int> InsertPartialWithAsync<T>(string table, T data, params string[] columns)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetInsertPartialWith(table, columns), data);
        }

        public int InsertWith<T>(string table, T data)
        {
            return Excute(_templateProvider.GetCache<T>().GetInsertWith(table), data);
        }

        public Task<int> InsertWithAsync<T>(string table, T data)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetInsertWith(table), data);
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

            return Query<T>(tpt, obj);
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
            return Query<T>(_templateProvider.GetCache<T>().GetQuery());
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            var connection = _connectionProvider.Rent();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                   await  connection.OpenAsync();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                var context = new QueryResultContext(await command.ExecuteReaderAsync());

                var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver<T>();

                var ls = new List<T>();

                while (await  context.Reader.ReadAsync())
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

        public Task<IEnumerable<T>> QueryAsync<T>(string template, object obj)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return QueryAsync<T>(tpt, obj);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(SqlTemplate template, object obj)
        {
            return QueryAsync<T>(_templateConverter.Convert(template, obj));
        }

        public Task<IEnumerable<T>> QueryAsync<T>()
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQuery());
        }

        public IEnumerable<T> QueryConditional<T>(string condition)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public Task<IEnumerable<T>> QueryConditionalAsync<T>(string condition)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public ICursor QueryConditionalCursor<T>(string condition)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public Task<ICursor> QueryConditionalCursorAsync<T>(string condition)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public IEnumerable<T> QueryConditionalOrderByAsc<T>(string condition, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition,orderBy));
        }

        public Task<IEnumerable<T>> QueryConditionalOrderByAscAsync<T>(string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public ICursor QueryConditionalOrderByAscCursor<T>(string condition, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public Task<ICursor> QueryConditionalOrderByAscCursorAsync<T>(string condition, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(string sql)
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

                return new DefaultCursor(context, connection, _connectionProvider);
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

        public ICursor QueryCursor<T>(string template, object obj)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return QueryCursor<T>(tpt, obj);

        }

        public ICursor QueryCursor<T>(SqlTemplate template, object obj)
        {
            return QueryCursor<T>(convert(template, obj));
        }

        public ICursor QueryCursor<T>()
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQuery());
        }

        public async Task<ICursor> QueryCursorAsync<T>(string sql)
        {
            var connection = _connectionProvider.Rent();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var command = connection.CreateCommand();

                command.CommandText = sql;

                var context = new QueryResultContext(await command.ExecuteReaderAsync());

                return new DefaultCursor(context, connection, _connectionProvider);
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

        public Task<ICursor> QueryCursorAsync<T>(string template, object obj)
        {
            var tpt = SqlTemplateParser.Parse(template);

            return QueryCursorAsync<T>(tpt, obj);
        }

        public Task<ICursor> QueryCursorAsync<T>(SqlTemplate template, object obj)
        {
            return QueryCursorAsync<T>(convert(template, obj));
        }

        public Task<ICursor> QueryCursorAsync<T>()
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQuery());
        }

        public IEnumerable<T> QueryOrderByAsc<T>(string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryOrderByAsc(orderBy));
        }

        public Task<IEnumerable<T>> QueryOrderByAscAsync<T>(string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByAsc(orderBy));
        }

        public ICursor QueryOrderByAscCursor<T>(string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryOrderByAsc(orderBy));
        }

        public Task<ICursor> QueryOrderByAscCursorAsync<T>(string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByAsc(orderBy));
        }

        public IEnumerable<T> QueryOrderByAscWith<T>(string table, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryOrderByAscWith(table, orderBy));
        }

        public Task<IEnumerable<T>> QueryOrderByAscWithAsync<T>(string table, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByAscWith(table, orderBy));
        }

        public ICursor QueryOrderByAscWithCursor<T>(string table, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryOrderByAscWith(table, orderBy));
        }

        public Task<ICursor> QueryOrderByAscWithCursorAsync<T>(string table, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByAscWith(table, orderBy));
        }

        public IEnumerable<T> QueryOrderByDesc<T>(string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryOrderByDesc(orderBy));
        }

        public Task<IEnumerable<T>> QueryOrderByDescAsync<T>(string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByDesc(orderBy));
        }

        public ICursor QueryOrderByDescCursor<T>(string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryOrderByDesc(orderBy));
        }

        public Task<ICursor> QueryOrderByDescCursorAsync<T>(string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByDesc(orderBy));
        }

        public IEnumerable<T> QueryOrderByDescWith<T>(string table, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryOrderByDescWith(table, orderBy));
        }

        public Task<IEnumerable<T>> QueryOrderByDescWithAsync<T>(string table, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByDescWith(table, orderBy));
        }

        public ICursor QueryOrderByDescWithCursor<T>(string table, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryOrderByDescWith(table, orderBy));
        }

        public Task<ICursor> QueryOrderByDescWithCursorAsync<T>(string table, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryOrderByDescWith(table, orderBy));
        }

        public IEnumerable<T> QueryPartial<T>(params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartial(columns));
        }

        public Task<IEnumerable<T>> QueryPartialAsync<T>(params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartial(columns));
        }

        public IEnumerable<T> QueryPartialConditional<T>(string condition, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition,columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalAsync<T>(string condition, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public ICursor QueryPartialConditionalCursor<T>(string condition, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public Task<ICursor> QueryPartialConditionalCursorAsync<T>(string condition, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAsc<T>(string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition,orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscAsync<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByAscCursor<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscCursorAsync<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAscWith<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table,condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscWithAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByAscWithCursor<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDesc<T>(string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescAsync<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByDescCursor<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescCursorAsync<T>(string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDescWith<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table,condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescWithAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByDescWithCursor<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalWith<T>(string table, string condition, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalWithAsync<T>(string table, string condition, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public ICursor QueryPartialConditionalWithCursor<T>(string table, string condition, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public Task<ICursor> QueryPartialConditionalWithCursorAsync<T>(string table, string condition, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public ICursor QueryPartialCursor<T>(params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartial(columns));
        }

        public Task<ICursor> QueryPartialCursorAsync<T>(params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartial(columns));
        }

        public IEnumerable<T> QueryPartialOrderByAsc<T>(string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAsc(orderBy,columns));
        }

        public Task<IEnumerable<T>> QueryPartialOrderByAscAsync<T>(string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAsc(orderBy, columns));
        }

        public ICursor QueryPartialOrderByAscCursor<T>(string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAsc(orderBy, columns));
        }

        public Task<ICursor> QueryPartialOrderByAscCursorAsync<T>(string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAsc(orderBy, columns));
        }

        public IEnumerable<T> QueryPartialOrderByAscWith<T>(string table, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAscWith(table,orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialOrderByAscWithAsync<T>(string table, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAscWith(table, orderBy, columns));
        }

        public ICursor QueryPartialOrderByAscWithCursor<T>(string table, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAscWith(table, orderBy, columns));
        }

        public Task<ICursor> QueryPartialOrderByAscWithCursorAsync<T>(string table, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByAscWith(table, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialOrderByDesc<T>(string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDesc(orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialOrderByDescAsync<T>(string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDesc(orderBy, columns));
        }

        public ICursor QueryPartialOrderByDescCursor<T>(string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDesc(orderBy, columns));
        }

        public Task<ICursor> QueryPartialOrderByDescCursorAsync<T>(string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDesc(orderBy, columns));
        }

        public IEnumerable<T> QueryPartialOrderByDescWith<T>(string table, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDescWith(table,orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialOrderByDescWithAsync<T>(string table, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDescWith(table, orderBy, columns));
        }

        public ICursor QueryPartialOrderByDescWithCursor<T>(string table, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDescWith(table, orderBy, columns));
        }

        public Task<ICursor> QueryPartialOrderByDescWithCursorAsync<T>(string table, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialOrderByDescWith(table, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialWith<T>(string table, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialWith(table, columns));
        }

        public Task<IEnumerable<T>> QueryPartialWithAsync<T>(string table, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialWith(table, columns));
        }

        public ICursor QueryPartialWithCursor<T>(string table, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialWith(table, columns));
        }

        public Task<ICursor> QueryPartialWithCursorAsync<T>(string table, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialWith(table, columns));
        }

        public IEnumerable<T> QueryTop<T>(int count)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTop(count));
        }

        public Task<IEnumerable<T>> QueryTopAsync<T>(int count)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTop(count));
        }

        public IEnumerable<T> QueryTopConditional<T>(int count, string condition)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count,condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalAsync<T>(int count, string condition)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public ICursor QueryTopConditionalCursor<T>(int count, string condition)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public Task<ICursor> QueryTopConditionalCursorAsync<T>(int count, string condition)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByAsc<T>(int count, string condition, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count,orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscAsync<T>(int count, string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByAscCursor<T>(int count, string condition, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByAscCursorAsync<T>(int count, string condition, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByAscWith<T>(string table, int count, string condition, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table,count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscWithAsync<T>(string table, int count, string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByAscWithCursor<T>(string table, int count, string condition, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByDesc<T>(int count, string condition, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc( count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescAsync<T>(int count, string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByDescCursor<T>(int count, string condition, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByDescCursorAsync<T>(int count, string condition, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByDescWith<T>(string table, int count, string condition, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table,count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescWithAsync<T>(string table, int count, string condition, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByDescWithCursor<T>(string table, int count, string condition, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalWith<T>(string table, int count, string condition)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count,condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalWithAsync<T>(string table, int count, string condition)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count, condition));
        }

        public ICursor QueryTopConditionalWithCursor<T>(string table, int count, string condition)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count, condition));
        }

        public Task<ICursor> QueryTopConditionalWithCursorAsync<T>(string table, int count, string condition)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count, condition));
        }

        public ICursor QueryTopCursor<T>(int count)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTop( count));
        }

        public Task<ICursor> QueryTopCursorAsync<T>(int count)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTop(count));
        }

        public IEnumerable<T> QueryTopOrderByAsc<T>(int count, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAsc(count,orderBy));
        }

        public Task<IEnumerable<T>> QueryTopOrderByAscAsync<T>(int count, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAsc(count, orderBy));
        }

        public ICursor QueryTopOrderByAscCursor<T>(int count, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAsc(count, orderBy));
        }

        public Task<ICursor> QueryTopOrderByAscCursorAsync<T>(int count, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAsc(count, orderBy));
        }

        public IEnumerable<T> QueryTopOrderByAscWith<T>(string table, int count, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAscWith(table,count, orderBy));
        }

        public Task<IEnumerable<T>> QueryTopOrderByAscWithAsync<T>(string table, int count, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAscWith(table, count, orderBy));
        }

        public ICursor QueryTopOrderByAscWithCursor<T>(string table, int count, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAscWith(table, count, orderBy));
        }

        public Task<ICursor> QueryTopOrderByAscWithCursorAsync<T>(string table, int count, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByAscWith(table, count, orderBy));
        }

        public IEnumerable<T> QueryTopOrderByDesc<T>(int count, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDesc( count, orderBy));
        }

        public Task<IEnumerable<T>> QueryTopOrderByDescAsync<T>(int count, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDesc(count, orderBy));
        }

        public ICursor QueryTopOrderByDescCursor<T>(int count, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDesc(count, orderBy));
        }

        public Task<ICursor> QueryTopOrderByDescCursorAsync<T>(int count, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDesc(count, orderBy));
        }

        public IEnumerable<T> QueryTopOrderByDescWith<T>(string table, int count, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDescWith(table,count, orderBy));
        }

        public Task<IEnumerable<T>> QueryTopOrderByDescWithAsync<T>(string table, int count, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDescWith(table, count, orderBy));
        }

        public ICursor QueryTopOrderByDescWithCursor<T>(string table, int count, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDescWith(table, count, orderBy));
        }

        public Task<ICursor> QueryTopOrderByDescWithCursorAsync<T>(string table, int count, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopOrderByDescWith(table, count, orderBy));
        }

        public IEnumerable<T> QueryTopPartial<T>(int count, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartial(count, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialAsync<T>(int count, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartial(count, columns));
        }

        public IEnumerable<T> QueryTopPartialConditional<T>(int count, string condition, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count,condition, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalAsync<T>(int count, string condition, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public ICursor QueryTopPartialConditionalCursor<T>(int count, string condition, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalCursorAsync<T>(int count, string condition, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAsc<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy,columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByAscCursor<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscCursorAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAscWith<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table,count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByAscWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDesc<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc( count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByDescCursor<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescCursorAsync<T>(int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDescWith<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table,count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByDescWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalWith<T>(string table, int count, string condition, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table,count, condition, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalWithAsync<T>(string table, int count, string condition, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table, count, condition, columns));
        }

        public ICursor QueryTopPartialConditionalWithCursor<T>(string table, int count, string condition, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table, count, condition, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalWithCursorAsync<T>(string table, int count, string condition, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table, count, condition, columns));
        }

        public ICursor QueryTopPartialCursor<T>(int count, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartial(count, columns));
        }

        public Task<ICursor> QueryTopPartialCursorAsync<T>(int count, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartial(count, columns));
        }

        public IEnumerable<T> QueryTopPartialOrderByAsc<T>(int count, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAsc(count,orderBy ,columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByAscAsync<T>(int count, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAsc(count, orderBy, columns));
        }

        public ICursor QueryTopPartialOrderByAscCursor<T>(int count, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAsc(count, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialOrderByAscCursorAsync<T>(int count, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAsc(count, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialOrderByAscWith<T>(string table, int count, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAscWith(table,count, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByAscWithAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAscWith(table, count, orderBy, columns));
        }

        public ICursor QueryTopPartialOrderByAscWithCursor<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAscWith(table, count, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialOrderByAscWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopParitialOrderByAscWith(table, count, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialOrderByDesc<T>(int count, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDesc( count, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByDescAsync<T>(int count, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDesc(count, orderBy, columns));
        }

        public ICursor QueryTopPartialOrderByDescCursor<T>(int count, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDesc(count, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialOrderByDescCursorAsync<T>(int count, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDesc(count, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialOrderByDescWith<T>(string table, int count, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDescWith(table,count, orderBy, columns));

        }

        public Task<IEnumerable<T>> QueryTopPartialOrderByDescWithAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDescWith(table, count, orderBy, columns));
        }

        public ICursor QueryTopPartialOrderByDescWithCursor<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDescWith(table, count, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialOrderByDescWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialOrderByDescWith(table, count, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialWith<T>(string table, int count, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialWith(table, count, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialWithAsync<T>(string table, int count, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialWith(table, count, columns));
        }

        public ICursor QueryTopPartialWithCursor<T>(string table, int count, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialWith(table, count, columns));
        }

        public Task<ICursor> QueryTopPartialWithCursorAsync<T>(string table, int count, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialWith(table, count, columns));
        }

        public IEnumerable<T> QueryTopWith<T>(string table,int count)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopWith(table, count));
        }

        public Task<IEnumerable<T>> QueryTopWithAsync<T>(string table,int count)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopWith(table, count));
        }

        public ICursor QueryTopWithCursor<T>(string table,int count)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopWith(table, count));
        }

        public Task<ICursor> QueryTopWithCursorAsync<T>(string table,int count)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopWith(table, count));
        }

        public IEnumerable<T> QueryWith<T>(string table)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryWith(table));
        }

        public Task<IEnumerable<T>> QueryWithAsync<T>(string table)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryWith(table));
        }

        public ICursor QueryWithCursor<T>(string table)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryWith(table));
        }

        public Task<ICursor> QueryWithCursorAsync<T>(string table)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryWith(table));
        }

        public int Update<T>(object parameter)
        {
            return Excute(buildUpdate<T>(_metaDataProvider.GetTable(typeof(T)).Name,parameter));
       }

        private string buildUpdate<T>(string table,object parameter,string condition=null)
        {
            var builder = new StringBuilder();

            builder.Append($"Update Table {table}");

            foreach (var item in JasmineReflectionCache.Instance.GetItem(typeof(T)).Properties)
            {
                builder.Append($"{item.Name}={DefaultBaseTypeConvertor.Instance.ConvertToSqlString(item.PropertyType, item.Getter.Invoke(parameter))},");
            }

            builder.RemoveLastComa();

            if(condition!=null)
                 builder.Append($"Where {condition}");

            return builder.ToString();
        }

        public Task<int> UpdateAsync<T>(object parameter)
        {
            return ExcuteAsync(buildUpdate<T>(_metaDataProvider.GetTable(typeof(T)).Name, parameter));
        }

        public int UpdateConditional<T>(string condition, object parameter)
        {
            return Excute(buildUpdate<T>(_metaDataProvider.GetTable(typeof(T)).Name, parameter, condition));
        }

        public Task<int> UpdateConditionalAsync<T>(string condition, object parameter)
        {
            return ExcuteAsync(buildUpdate<T>(_metaDataProvider.GetTable(typeof(T)).Name, parameter, condition));
        }

        public int UpdateConditionalWidth<T>(string table, string condition, object parameter)
        {
            return Excute(buildUpdate<T>(table, parameter, condition));
        }

        public Task<int> UpdateConditionalWidthAsync<T>(string table, string condition, object parameter)
        {
            return ExcuteAsync(buildUpdate<T>(table, parameter, condition));
        }

        public int UpdateWith<T>(string table, object parameter)
        {
            return Excute(buildUpdate<T>(table, parameter));
        }

        public Task<int> UpdateWithAsync<T>(string table, object parameter)
        {
            return ExcuteAsync(buildUpdate<T>(table, parameter));
        }
    }
}
