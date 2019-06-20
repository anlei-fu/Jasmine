using Jasmine.Extensions;
using Jasmine.Orm.Implements;
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
        public JasmineSqlExcutor(IDbConnectionProvider provider)
        {
            _connectionProvider = provider;
        }
        private ITableTemplateCacheProvider _templateProvider =>DefaultTableTemplateCacheProvider.Instance;
        private ITableMetaDataProvider _tableMetaDataProvider => DefaultTableMetaDataProvider.Instance;

        private IDbConnectionProvider _connectionProvider;


        #region Batch insert.........................
        /// <summary>
        /// do batch insert
        /// </summary>
        /// <typeparam name="T">associate table</typeparam>
        /// <param name="datas">datas to insert</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int BatchInsert<T>(IEnumerable<object> datas, DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));
            //if has join table can just insert one by one
            if(table.HasJoinTable)
            {
                var template = _templateProvider.GetCache<T>().GetInsert();

                var builder = new StringBuilder();

                foreach (var item in datas)
                {
                    builder.Append(template.Render(item))
                           .Append("\r\n");
                }

                return Excute(builder.ToString(), transaction);

            }
            else
            {
                switch (_connectionProvider.DataSource)
                {
                    case DataSource.SqlServer:
                    case DataSource.MySql:
                    case DataSource.Db2:
                    case DataSource.Sqlite:
                    case DataSource.Postgre:
                    case DataSource.Access:

                        var t = 0;

                        foreach (var item in getBatchInsertNormalSql<T>(datas, table.GetAllColumnName()))
                        {
                          t+=  Excute(item, transaction);
                        }

                        return t;
                    case DataSource.Oracle:
                        break;
                    case DataSource.SyBase:
                        break;
                    default:
                        break;
                }
            }

            throw new NotImplementedException();
        }

       
      
        public async Task<int> BatchInsertAsync<T>(IEnumerable<object> datas, DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));
     
            //if has join table can just insert one by one, need get a method to optimize
            if (table.HasJoinTable)
            {
                var template = _templateProvider.GetCache<T>().GetInsert();

                var builder = new StringBuilder();

                foreach (var item in datas)
                {
                    builder.Append(template.Render(item))
                           .Append("\r\n");
                }

                return await ExcuteAsync(builder.ToString(), transaction);

            }
            else
            {
                switch (_connectionProvider.DataSource)
                {
                    case DataSource.SqlServer:
                    case DataSource.MySql:
                    case DataSource.Db2:
                    case DataSource.Sqlite:
                    case DataSource.Postgre:
                    case DataSource.Access:

                        var t = 0;
                        foreach (var item in getBatchInsertNormalSql<T>(datas, table.GetAllColumnName()))
                        {
                            t += await ExcuteAsync(item, transaction);
                        }

                        return t;

                    case DataSource.Oracle:
                        break;
                    case DataSource.SyBase:
                        break;
                    default:
                        break;
                }
            }

            throw new NotImplementedException();
        }

        public int BatchInsertPartial<T>(string[] columns, IEnumerable<object> datas, DbTransaction transaction=null)
        {
            switch (_connectionProvider.DataSource)
            {
                case DataSource.SqlServer:
                case DataSource.MySql:
                case DataSource.Db2:
                case DataSource.Sqlite:
                case DataSource.Postgre:
                case DataSource.Access:

                    var t = 0;

                    foreach (var item in getBatchInsertNormalSql<T>(datas, columns))
                    {
                        t +=  Excute(item, transaction);
                    }

                    return t;


                case DataSource.Oracle:
                    break;
                case DataSource.SyBase:
                    break;
                default:
                    break;
            }

            return 0;
        }
    
        public async Task<int> BatchInsertPartialAsync<T>( IEnumerable<object> datas, DbTransaction transaction, params string[] columns)
        {
            switch (_connectionProvider.DataSource)
            {
                case DataSource.SqlServer:
                case DataSource.MySql:
                case DataSource.Db2:
                case DataSource.Sqlite:
                case DataSource.Postgre:
                case DataSource.Access:

                    var t = 0;

                    foreach (var item in getBatchInsertNormalSql<T>(datas, columns))
                    {
                        t += await ExcuteAsync(item, transaction);
                    }

                    return t;

                case DataSource.Oracle:
                    break;
                case DataSource.SyBase:
                    break;
                default:
                    break;
            }

            return  0;
        }

        public int BatchInsertPartialWith<T>(string table, string[] columns, IEnumerable<object> datas, DbTransaction transaction=null)
        {
            switch (_connectionProvider.DataSource)
            {
                case DataSource.SqlServer:
                case DataSource.MySql:
                case DataSource.Db2:
                case DataSource.Sqlite:
                case DataSource.Postgre:
                case DataSource.Access:

                    var t = 0;

                    foreach (var item in getBatchInsertNormalSql(table, datas, columns))
                    {
                        t+= Excute(item, transaction);
                    }

                    return t;
                case DataSource.Oracle:
                    break;
                case DataSource.SyBase:
                    break;
                default:
                    break;
            }

            return 0;
        }

        public async Task<int> BatchInsertPartialWithAsync<T>(string table,  IEnumerable<object> datas, DbTransaction transaction=null , params string[] columns)
        {
            switch (_connectionProvider.DataSource)
            {
                case DataSource.SqlServer:
                case DataSource.MySql:
                case DataSource.Db2:
                case DataSource.Sqlite:
                case DataSource.Postgre:
                case DataSource.Access:
                    var t = 0;

                    foreach (var item in getBatchInsertNormalSql(table, datas, columns))
                    {
                        t += await ExcuteAsync(item, transaction);
                    }

                    return t;
                case DataSource.Oracle:
                    break;
                case DataSource.SyBase:
                    break;
                default:
                    break;
            }

            return 0;
        }

        public int BatchInsertWith<T>(string tableName, IEnumerable<object> datas, DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));
            //if has join table can just insert one by one
            if (table.HasJoinTable)
            {
                var template = _templateProvider.GetCache<T>().GetInsertWith(tableName);

                var builder = new StringBuilder();

                foreach (var item in datas)
                {
                    builder.Append(template.Render(item))
                           .Append("\r\n");
                }

                return Excute(builder.ToString(), transaction);

            }
            else
            {
                switch (_connectionProvider.DataSource)
                {
                    case DataSource.SqlServer:
                    case DataSource.MySql:
                    case DataSource.Db2:
                    case DataSource.Sqlite:
                    case DataSource.Postgre:
                    case DataSource.Access:

                        var t = 0;

                        foreach (var item in getBatchInsertNormalSql(tableName, datas, table.GetAllColumnName()))
                        {
                            t += Excute(item, transaction);
                        }

                        return t;
                    case DataSource.Oracle:
                        break;
                    case DataSource.SyBase:
                        break;
                    default:
                        break;
                }
            }

            throw new NotImplementedException();
        }

        public async Task<int> BatchInsertWithAsync<T>(string tableName, IEnumerable<object> datas, DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));
            //if has join table can just insert one by one
            if (table.HasJoinTable)
            {
                var template = _templateProvider.GetCache<T>().GetInsertWith(tableName);

                var builder = new StringBuilder();

                foreach (var item in datas)
                {
                    builder.Append(template.Render(item))
                           .Append("\r\n");
                }

                return await ExcuteAsync(builder.ToString(), transaction);

            }
            else
            {
                switch (_connectionProvider.DataSource)
                {
                    case DataSource.SqlServer:
                    case DataSource.MySql:
                    case DataSource.Db2:
                    case DataSource.Sqlite:
                    case DataSource.Postgre:
                    case DataSource.Access:

                        var t = 0;

                        foreach (var item in getBatchInsertNormalSql(tableName, datas, table.GetAllColumnName()))
                        {
                          t += await  ExcuteAsync(item, transaction);
                        }

                        return t;
                        
                    case DataSource.Oracle:
                        break;
                    case DataSource.SyBase:
                        break;
                    default:
                        break;
                }
            }

            throw new NotImplementedException();
        }

        private List<string> getBatchInsertNormalSql<T>(IEnumerable<object> datas, params string[] columns)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));

            return getBatchInsertNormalSql(table.Name, datas, columns);

        }

        /// <summary>
        /// sql server max batch size is 1056
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>

        private List<string> getBatchInsertNormalSql(string table, IEnumerable<object> datas, params string[] columns)
        {
            var ls = new List<string>();
            var left = SqlTemplateMaker.MakeInsertLeft(table, columns);
            var right = SqlTemplateMaker.MakeInsertRight(columns);

            var builder = new StringBuilder();

            var t = 0;

            foreach (var item in datas)
            {
                if(t==0)
                {
                    builder.Append(left);
                }

                builder.Append(right.Render(item));

                builder.Append(",");

                if(t>1000)
                {
                    ls.Add(builder.RemoveLastComa().ToString());
                    t = 0;
                    builder.Clear();
                }
                t++;
            }

            if (builder.Length != 0)
                ls.Add(builder.RemoveLastComa().ToString());


            return ls;

        }

        private int batchInsertOracle<T>(IEnumerable<object> datas)
        {
            return 0;
        }
        #endregion



        #region Create ...................................
        public int Create<T>(DbTransaction transantion = null)
        {
            return Excute(_templateProvider.GetCache<T>().GetCreate());
        }

        public Task<int> CreateAsync<T>(DbTransaction transantion = null)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetCreate());
        }

        public int CreateWith<T>(string table, DbTransaction transantion = null)
        {
            return Excute(_templateProvider.GetCache<T>().GetCreateWith(table));
        }

        public Task<int> CreateWithAsync<T>(string table,DbTransaction transantion=null)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetCreateWith(table));
        }
        #endregion



        #region Delete ................................

        public int DeleteAll<T>(DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));

            return Excute($"Delete From {table.Name} ",transaction);

        } 
        public Task<int> DeleteAllAsync<T>(DbTransaction transaction=null)
        {
            var table = _tableMetaDataProvider.GetTable(typeof(T));

            return ExcuteAsync($"Delete From {table.Name} ",transaction);
        }

        public int DeleteAll(string table,DbTransaction transaction=null)
        {
            return Excute($"Delete From {table} ");
        }
        public Task<int> DeleteAllAsync(string table, DbTransaction transaction = null)
        {
            return ExcuteAsync($"Delete From {table} ", transaction);
        }

        public int Delete(string table, string condition,object parameter, DbTransaction transaction=null)
        {
            return Excute($" Delete From {table} Where {condition}", transaction);
        }

        public int Delete<T>(string condition,object parameter, DbTransaction transaction=null)
        {

            condition =SqlTemplate.Parse(condition).Render(parameter);

            return Excute(_templateProvider.GetCache<T>().GetDelete(condition), transaction);
        }

        public Task<int> DeleteAsync(string table, string condition,object parameter, DbTransaction transaction=null)
        {
            return ExcuteAsync($" Delete From {table} Where {condition}", transaction);
        }

        public Task<int> DeleteAsync<T>(string condition, object parameter,DbTransaction transaction=null)
        {
            return ExcuteAsync($" Delete From {_tableMetaDataProvider.GetTable(typeof(T)).Name} Where {condition}", transaction);
        }
        #endregion



        #region Drop..........................................
        public int Drop(string name, DbTransaction transaction = null)
        {
            return Excute($" Drop Table {name}",transaction);
        }

        public int Drop<T>(DbTransaction transaction = null)
        {
            return Excute($" Drop Table {_tableMetaDataProvider.GetTable(typeof(T)).Name}",transaction);
        }

        public Task<int> DropAsync(string name, DbTransaction transaction = null)
        {
            return ExcuteAsync($" Drop Table {name}",transaction);
        }

        public Task<int> DropAsync<T>(DbTransaction transaction = null)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetDrop(),transaction);
        }
        #endregion



        #region Excute..........................................
        public int Excute(string template, object obj, DbTransaction transaction = null)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return Excute(sql,transaction);
        }

        public int Excute(SqlTemplate template, object obj, DbTransaction transaction = null)
        {
            return Excute(template.Render(obj), transaction);
        }
        public int Excute(string sql, DbTransaction transaction=null)
        {

            var connection = transaction!=null?transaction.Connection: _connectionProvider.Rent();
            
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
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                _connectionProvider.Recycle(connection);
            }
        }

        public Task<int> ExcuteAsync(string template, object obj, DbTransaction transaction = null)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return ExcuteAsync(sql,transaction);
        }

        public Task<int> ExcuteAsync(SqlTemplate template, object obj, DbTransaction transaction = null)
        {
            return ExcuteAsync(template, obj, transaction);
        }
        public async Task<int> ExcuteAsync(string sql, DbTransaction transaction=null)
        {

            var connection =transaction!=null?transaction.Connection:_connectionProvider.Rent();

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
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _connectionProvider.Recycle(connection);
            }


        }
        #endregion



        #region Insert....................................................
        public int Insert<T>(object data, DbTransaction transaction = null)
        {
            return Excute(_templateProvider.GetCache<T>().GetInsert().Render(data),transaction);
        }


        public Task<int> InsertAsync<T>(object data, DbTransaction transaction = null)
        {
            return ExcuteAsync(_templateProvider.GetCache<T>().GetInsert(), data,transaction);
        }

        public int InsertPartial<T>( object data, DbTransaction transaction , params string[] columns)
        {
            var sql = SqlTemplateMaker.MakeInsert(_tableMetaDataProvider.GetTable(typeof(T)).Name, columns).Render(data);

            return Excute(sql,transaction);
        }

        public Task<int> InsertPartialAsync<T>( object data, DbTransaction transaction , params string[] columns)
        {
            var sql = SqlTemplateMaker.MakeInsert(_tableMetaDataProvider.GetTable(typeof(T)).Name, columns).Render(data);

            return ExcuteAsync(sql,transaction);
        }

        public int InsertPartialWith<T>(string table, object data, DbTransaction transaction , params string[] columns)
        {
            var sql = SqlTemplateMaker.MakeInsert(table, columns).Render(data);

            return Excute(sql,transaction);
        }

        public Task<int> InsertPartialWithAsync<T>(string table, object data, DbTransaction transaction , params string[] columns)
        {
            var sql = SqlTemplateMaker.MakeInsert(table, columns).Render(data);

            return ExcuteAsync(sql,transaction);
        }

        public int InsertWith<T>(string table, object data, DbTransaction transaction = null)
        {
            var sql = _templateProvider.GetCache<T>().GetInsertWith(table).Render(data);

            return Excute(sql,transaction);
        }

        public Task<int> InsertWithAsync<T>(string table, object data, DbTransaction transaction = null)
        {
            var sql = _templateProvider.GetCache<T>().GetInsertWith(table).Render(data);

            return ExcuteAsync(sql,data,transaction);
        }
        #endregion



        #region Query Full Columns .......................
      

        public IEnumerable<T> Query<T>(string template, object obj)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return Query<T>(sql);
        }

        public IEnumerable<T> Query<T>(SqlTemplate template, object obj)
        {
            return Query<T>(template.Render(obj));
        }

      
        public IEnumerable<T> Query<T>()
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQuery());
        }

     

        public Task<IEnumerable<T>> QueryAsync<T>(string template, object obj=null)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return QueryAsync<T>(sql);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(SqlTemplate template, object obj)
        {
            return QueryAsync<T>(template.Render(obj));
        }

        public Task<IEnumerable<T>> QueryAsync<T>()
        {
            var sql = _templateProvider.GetCache<T>().GetQuery();

            return QueryAsync<T>(sql);
        }
        #endregion



        #region Query ....................................................
        public IEnumerable<T> QueryConditional<T>(string condition,object parameter=null)
        {
            
            condition =SqlTemplate.Parse(condition).Render(parameter);

            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public Task<IEnumerable<T>> QueryConditionalAsync<T>(string condition,object parameter=null)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public ICursor QueryConditionalCursor<T>(string condition,object parameter=null)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public Task<ICursor> QueryConditionalCursorAsync<T>(string condition,object parameter=null)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditional(condition));
        }

        public IEnumerable<T> QueryConditionalOrderByAsc<T>(string condition,object parameter, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition,orderBy));
        }

        public Task<IEnumerable<T>> QueryConditionalOrderByAscAsync<T>(string condition,object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public ICursor QueryConditionalOrderByAscCursor<T>(string condition,object parameter ,string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public Task<ICursor> QueryConditionalOrderByAscCursorAsync<T>(string condition, object parameter, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalOrderByAsc(condition, orderBy));
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition, object parameter)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public IEnumerable<T> QueryConditionalWith<T>(string table, string condition, object parameter, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, object parameter)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition, object parameter)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public ICursor QueryConditionalWithCursor<T>(string table, string condition, object parameter, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition, object parameter)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryConditionalWith(table, condition));
        }

        public Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition, object parameter, string orderBy)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(string template, object obj=null)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return QueryCursor<T>(sql);

        }

        public ICursor QueryCursor<T>(SqlTemplate template, object obj)
        {
            return QueryCursor<T>(template.Render(obj));
        }

        public ICursor QueryCursor<T>()
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQuery());
        }
        public Task<ICursor> QueryCursorAsync<T>(string template, object obj=null)
        {
            var sql = SqlTemplate.Parse(template).Render(obj);

            return QueryCursorAsync<T>(sql);
        }

        public Task<ICursor> QueryCursorAsync<T>(SqlTemplate template, object obj=null)
        {
            return QueryCursorAsync<T>(template.Render(obj));
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

        public IEnumerable<T> QueryPartialConditional<T>(string condition, object parameter, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition,columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalAsync<T>(string condition,object parameter, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public ICursor QueryPartialConditionalCursor<T>(string condition, object parameter, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public Task<ICursor> QueryPartialConditionalCursorAsync<T>(string condition, object parameter, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditional(condition, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAsc<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition,orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscAsync<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByAscCursor<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscCursorAsync<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAsc(condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByAscWith<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table,condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByAscWithAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByAscWithCursor<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByAscWithCursorAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByAscWith(table, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDesc<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescAsync<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByDescCursor<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescCursorAsync<T>(string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDesc(condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalOrderByDescWith<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table,condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalOrderByDescWithAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public ICursor QueryPartialConditionalOrderByDescWithCursor<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public Task<ICursor> QueryPartialConditionalOrderByDescWithCursorAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalOrderByDescWith(table, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryPartialConditionalWith<T>(string table, string condition, object parameter, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public Task<IEnumerable<T>> QueryPartialConditionalWithAsync<T>(string table, string condition, object parameter, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public ICursor QueryPartialConditionalWithCursor<T>(string table, string condition, object parameter, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryPartialConditionalWith(table, condition, columns));
        }

        public Task<ICursor> QueryPartialConditionalWithCursorAsync<T>(string table, string condition, object parameter, params string[] columns)
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

        public IEnumerable<T> QueryTopConditional<T>(int count, string condition, object parameter=null)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count,condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalAsync<T>(int count, string condition, object parameter=null)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public ICursor QueryTopConditionalCursor<T>(int count, string condition,object parameter=null)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public Task<ICursor> QueryTopConditionalCursorAsync<T>(int count, string condition, object parameter=null)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditional(count, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByAsc<T>(int count, string condition, object parameter, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count,orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscAsync<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByAscCursor<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByAscCursorAsync<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAsc(count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByAscWith<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table,count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByAscWithAsync<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByAscWithCursor<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByAscWith(table, count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByDesc<T>(int count, string condition, object parameter, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc( count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescAsync<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByDescCursor<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByDescCursorAsync<T>(int count, string condition, object parameter, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDesc(count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalOrderByDescWith<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table,count, orderBy, condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalOrderByDescWithAsync<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public ICursor QueryTopConditionalOrderByDescWithCursor<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public Task<ICursor> QueryTopConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalOrderByDescWith(table, count, orderBy, condition));
        }

        public IEnumerable<T> QueryTopConditionalWith<T>(string table, int count, string condition,object parameter=null)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count,condition));
        }

        public Task<IEnumerable<T>> QueryTopConditionalWithAsync<T>(string table, int count, string condition, object parameter=null)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count, condition));
        }

        public ICursor QueryTopConditionalWithCursor<T>(string table, int count, string condition, object parameter=null)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopConditionalWith(table, count, condition));
        }

        public Task<ICursor> QueryTopConditionalWithCursorAsync<T>(string table, int count, string condition,object parameter=null)
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

        public IEnumerable<T> QueryTopPartialConditional<T>(int count, string condition, object parameter, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count,condition, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalAsync<T>(int count, string condition, object parameter, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public ICursor QueryTopPartialConditionalCursor<T>(int count, string condition, object parameter, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalCursorAsync<T>(int count, string condition, object parameter, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditional(count, condition, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAsc<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy,columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByAscCursor<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscCursorAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAsc(count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByAscWith<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table,count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscWithAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByAscWithCursor<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByAscWith(table, count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDesc<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc( count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByDescCursor<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescCursorAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDesc(count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalOrderByDescWith<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table,count, condition, orderBy, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescWithAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public ICursor QueryTopPartialConditionalOrderByDescWithCursor<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns)
        {
            return QueryCursorAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalOrderByDescWith(table, count, condition, orderBy, columns));
        }

        public IEnumerable<T> QueryTopPartialConditionalWith<T>(string table, int count, string condition, object parameter, params string[] columns)
        {
            return Query<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table,count, condition, columns));
        }

        public Task<IEnumerable<T>> QueryTopPartialConditionalWithAsync<T>(string table, int count, string condition, object parameter, params string[] columns)
        {
            return QueryAsync<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table, count, condition, columns));
        }

        public ICursor QueryTopPartialConditionalWithCursor<T>(string table, int count, string condition, object parameter, params string[] columns)
        {
            return QueryCursor<T>(_templateProvider.GetCache<T>().GetQueryTopPartialConditionalWith(table, count, condition, columns));
        }

        public Task<ICursor> QueryTopPartialConditionalWithCursorAsync<T>(string table, int count, string condition, object parameter, params string[] columns)
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

                var context = new QueryResultContext(this,command.ExecuteReader(),connection,_connectionProvider);

                var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver((typeof(T)));


                return resolver.Resolve<T>(context);

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

                var context = new QueryResultContext(this,await command.ExecuteReaderAsync(),connection,_connectionProvider);

                return null;
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

                var context = new QueryResultContext(this,command.ExecuteReader(),connection,_connectionProvider);

                return null;

                // return new DefaultCursor(context, connection, _connectionProvider);
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

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
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

                var context = new QueryResultContext(this,await command.ExecuteReaderAsync(),connection,_connectionProvider);

                //  var resolver = DefaultQueryResultResolverProvider.Instance.GetResolver<T>();

                var ls = new List<T>();

                while (await context.Reader.ReadAsync())
                {
                    //   ls.Add((T)resolver.Resolve(context, typeof(T)));
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

        #endregion




        #region Update ..................

        public int Update<T>(object parameter,DbTransaction transaction=null)
        {
            return Excute(buildUpdate<T>(_tableMetaDataProvider.GetTable(typeof(T)).Name,parameter),transaction);
       }
 
        public Task<int> UpdateAsync<T>(object parameter,DbTransaction transaction=null)
        {
            return ExcuteAsync(buildUpdate<T>(_tableMetaDataProvider.GetTable(typeof(T)).Name, parameter),transaction);
        }

        public int UpdateConditional<T>( object parameter,string condition,object confitionParameter=null, DbTransaction transaction=null)
        {
            return Excute(buildUpdate<T>(_tableMetaDataProvider.GetTable(typeof(T)).Name, parameter, SqlTemplate.Parse(condition).Render(confitionParameter)),transaction);
        }

        public Task<int> UpdateConditionalAsync<T>( object parameter,string condition,object conditionParameter=null,DbTransaction transaction=null)
        {
            return ExcuteAsync(buildUpdate<T>(_tableMetaDataProvider.GetTable(typeof(T)).Name, parameter, SqlTemplate.Parse(condition).Render(conditionParameter)),transaction);
        }

        public int UpdateConditionalWidth<T>(string table,  object parameter, string condition,object conditionParameter=null, DbTransaction transaction=null)
        {
            return Excute(buildUpdate<T>(table, parameter, SqlTemplate.Parse(condition).Render(conditionParameter)),transaction);
        }

        public Task<int> UpdateConditionalWidthAsync<T>(string table,  object parameter, string condition,object conditionParameter=null, DbTransaction transantion = null)
        {
            return ExcuteAsync(buildUpdate<T>(table, parameter, SqlTemplate.Parse(condition).Render(conditionParameter)),transantion);
        }

        public int UpdateWith<T>(string table, object parameter,DbTransaction transaction=null)
        {
            return Excute(buildUpdate<T>(table, parameter),transaction);
        }

        public Task<int> UpdateWithAsync<T>(string table, object parameter,DbTransaction transantion=null)
        {
            return ExcuteAsync(buildUpdate<T>(table, parameter),transantion);
        }

        private string buildUpdate<T>(string table, object parameter, string condition = null)
        {
            var builder = new StringBuilder();

            builder.Append($"Update Table {table}");

            foreach (var item in JasmineReflectionCache.Instance.GetItem(typeof(T)).Properties)
            {
                //   builder.Append($"{item.Name}={DefaultBaseTypeConvertor.Instance.ConvertToSqlString(item.PropertyType, item.Getter.Invoke(parameter))},");
            }

            builder.RemoveLastComa();

            if (condition != null)
                builder.Append($"Where {condition}");

            return builder.ToString();
        }

        #endregion




        #region Procedure ..................................
        public int CallProcedure(string name, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> CallProcedureAsync(string name, object parameter)
        {
            throw new NotImplementedException();
        }

        public int CallProcedure(string name, IEnumerable<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> CallProcedureAsync(string name, IEnumerable<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public IMutipleResultReader MutipleQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<IMutipleResultReader> MutipleQueryAsync(string sql)
        {
            throw new NotImplementedException();
        }

        public IMutipleResultReader MutipleQuery(SqlTemplate template, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<IMutipleResultReader> MutipleQueryAsync(SqlTemplate template, object parameter)
        {
            throw new NotImplementedException();
        }
        #endregion

        public ICursor QueryCursor( string sql)
        {
            throw new NotImplementedException();
        }
    }
}
