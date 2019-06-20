using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Jasmine.Orm
{
    /// <summary>
    /// a main api interface, provide basic sql operations,
    /// 1. string case of all property names,table names,column names, parameter names is ignored as sql
    /// 2.
    /// </summary>
    public interface ISqlExcuter
    {
        #region Query....................

        IMutipleResultReader MutipleQuery(string sql);

        Task<IMutipleResultReader> MutipleQueryAsync(string sql);

        IMutipleResultReader MutipleQuery(SqlTemplate template, object parameter);

        Task<IMutipleResultReader> MutipleQueryAsync(SqlTemplate template, object parameter);
        
        /*
         * raw sql 
         */
         /// <summary>
         /// e.g  select * from table ,query result will be auto convterted to <see cref="IEnumerable{T}"/>
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="sql"></param>
         /// <returns></returns>
        IEnumerable<T> Query<T>(string sql);
        /// <summary>
        /// e.g
        ///template  select * from table where name=@name and age>@age   
        ///parameters: new {name="fal",age=10}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string template, object parameters);
        /// <summary>
        /// <see cref="SqlTemplate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(SqlTemplate template, object obj);


        int CallProcedure(string name, object parameter);
        Task<int> CallProcedureAsync(string name, object parameter);
        int CallProcedure(string name, IEnumerable<DbParameter> parameters);
        Task<int> CallProcedureAsync(string name, IEnumerable<DbParameter> parameters);



        /*
         *  return full column
         */ 
        IEnumerable<T> Query<T>();
        IEnumerable<T> QueryWith<T>(string table);
        IEnumerable<T> QueryOrderByAsc<T>(string orderBy);
        IEnumerable<T> QueryOrderByAscWith<T>(string table, string orderBy);
        IEnumerable<T> QueryOrderByDesc<T>(string orderBy);
        IEnumerable<T> QueryOrderByDescWith<T>(string table, string orderBy);

        /*
         *  full conditional
         *  e.g  condition; age>10 ,'where' is not required
         */ 
        IEnumerable<T> QueryConditional<T>(string condition, object parameter);
        IEnumerable<T> QueryConditionalWith<T>(string table, string condition,object parameter);
        IEnumerable<T> QueryConditionalOrderByAsc<T>(string condition, object parameter, string orderBy);
        IEnumerable<T> QueryConditionalWith<T>(string table, string condition, object parameter, string orderBy);

        /*
         *  partial
         *  
         *  
         */
        /// <summary>
        /// columns must exist in table <see cref="T"/>
        /// join column  use  '.' to replace '_'
        /// e.g
        /// QueryPartial("name","food.vegetable")
        /// 
        /// sql generated : select Name, Food_Vegetible from table
        /// case is ignored ,it will  convert automatically
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        IEnumerable<T> QueryPartial<T>(params string[] columns);
        IEnumerable<T> QueryPartialWith<T>(string table,params string[] columns);
        IEnumerable<T> QueryPartialOrderByAsc<T>(string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialOrderByAscWith<T>(string table, string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialOrderByDesc<T>(string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialOrderByDescWith<T>(string table, string orderBy, params string[] columns);
        // partial conditional
        IEnumerable<T> QueryPartialConditional<T>(string condition, object parameter, params string[] columns);
        IEnumerable<T> QueryPartialConditionalOrderByAsc<T>(string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialConditionalOrderByAscWith<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialConditionalOrderByDesc<T>(string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialConditionalOrderByDescWith<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryPartialConditionalWith<T>(string table, string condition, object parameter, params string[] columns);

       /*
        *  top
        */ 
        // top full
        IEnumerable<T> QueryTop<T>(int count);
        IEnumerable<T> QueryTopWith<T>(string table,int count);
        IEnumerable<T> QueryTopOrderByAsc<T>(int count, string orderBy);
        IEnumerable<T> QueryTopOrderByAscWith<T>(string table, int count, string orderBy);
        IEnumerable<T> QueryTopOrderByDesc<T>(int count, string orderBy);
        IEnumerable<T> QueryTopOrderByDescWith<T>(string table, int count, string orderBy);
        // full conditional
        IEnumerable<T> QueryTopConditional<T>(int count, string condition, object parameter);
        IEnumerable<T> QueryTopConditionalWith<T>(string table, int count, string condition, object parameter);
        IEnumerable<T> QueryTopConditionalOrderByAsc<T>(int count, string condition, object parameter, string orderBy);
        IEnumerable<T> QueryTopConditionalOrderByAscWith<T>(string table,int count, string condition, object parameter, string orderBy);
        IEnumerable<T> QueryTopConditionalOrderByDesc<T>(int count, string condition, object parameter, string orderBy);
        IEnumerable<T> QueryTopConditionalOrderByDescWith<T>(string table, int count, string condition, object parameter, string orderBy);
        // top  partial 
        IEnumerable<T> QueryTopPartial<T>(int count, params string[] columns);
        IEnumerable<T> QueryTopPartialWith<T>(string table, int count, params string[] column);
        IEnumerable<T> QueryTopPartialOrderByAsc<T>(int count, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialOrderByAscWith<T>(string table, int count, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialOrderByDesc<T>(int count, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialOrderByDescWith<T>(string table, int count, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialConditional<T>(int count, string condition, object parameter, params string[] columns);
        IEnumerable<T> QueryTopPartialConditionalWith<T>(string table, int count, string conditiona, object parameter, params string[] columns);
        IEnumerable<T> QueryTopPartialConditionalOrderByAsc<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialConditionalOrderByAscWith<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialConditionalOrderByDesc<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        IEnumerable<T> QueryTopPartialConditionalOrderByDescWith<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);



       /*
        * raw sql
        */
        Task<IEnumerable<T>> QueryAsync<T>(string sql);
        Task<IEnumerable<T>> QueryAsync<T>(string template, object parameter);
        Task<IEnumerable<T>> QueryAsync<T>(SqlTemplate template, object obj);



        /*
         *  full column
         */
        Task<IEnumerable<T>> QueryAsync<T>();
        Task<IEnumerable<T>> QueryWithAsync<T>(string table);
        Task<IEnumerable<T>> QueryOrderByAscAsync<T>(string orderBy);
        Task<IEnumerable<T>> QueryOrderByAscWithAsync<T>(string table, string orderBy);
        Task<IEnumerable<T>> QueryOrderByDescAsync<T>(string orderBy);
        Task<IEnumerable<T>> QueryOrderByDescWithAsync<T>(string table, string orderBy);

        /*
         *  full conditional
         */
        Task<IEnumerable<T>> QueryConditionalAsync<T>(string condition, object parameter);
        Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, object parameter);
        Task<IEnumerable<T>> QueryConditionalOrderByAscAsync<T>(string condition, object parameter, string orderBy);
        Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition, object parameter, string orderBy);

        /*
         *  partial
         */
        Task<IEnumerable<T>> QueryPartialAsync<T>(params string[] columns);
        Task<IEnumerable<T>> QueryPartialWithAsync<T>(string table, params string[] columns);
        Task<IEnumerable<T>> QueryPartialOrderByAscAsync<T>(string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialOrderByAscWithAsync<T>(string table, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialOrderByDescAsync<T>(string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialOrderByDescWithAsync<T>(string table, string orderBy, params string[] columns);
        // partial conditional
        Task<IEnumerable<T>> QueryPartialConditionalAsync<T>(string condition, object parameter, params string[] columns);
        Task<IEnumerable<T>> QueryPartialConditionalOrderByAscAsync<T>(string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialConditionalOrderByAscWithAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialConditionalOrderByDescAsync<T>(string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialConditionalOrderByDescWithAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryPartialConditionalWithAsync<T>(string table, string condition, object parameter, params string[] columns);

        /*
         *  top
         */
        // top full
        Task<IEnumerable<T>> QueryTopAsync<T>(int count);
        Task<IEnumerable<T>> QueryTopWithAsync<T>(string table,int count);
        Task<IEnumerable<T>> QueryTopOrderByAscAsync<T>(int count, string orderBy);
        Task<IEnumerable<T>> QueryTopOrderByAscWithAsync<T>(string table, int count, string orderBy);
        Task<IEnumerable<T>> QueryTopOrderByDescAsync<T>(int count, string orderBy);
        Task<IEnumerable<T>> QueryTopOrderByDescWithAsync<T>(string table, int count, string orderBy);
        // full conditional
        Task<IEnumerable<T>> QueryTopConditionalAsync<T>(int count, string condition, object parameter);
        Task<IEnumerable<T>> QueryTopConditionalWithAsync<T>(string table, int count, string condition, object parameter);
        Task<IEnumerable<T>> QueryTopConditionalOrderByAscAsync<T>(int count, string condition, object parameter, string orderBy);
        Task<IEnumerable<T>> QueryTopConditionalOrderByAscWithAsync<T>(string table, int count, string condition, object parameter, string orderBy);
        Task<IEnumerable<T>> QueryTopConditionalOrderByDescAsync<T>(int count, string condition, object parameter, string orderBy);
        Task<IEnumerable<T>> QueryTopConditionalOrderByDescWithAsync<T>(string table, int count, string condition, object parameter, string orderBy);
        // top  partial 
        Task<IEnumerable<T>> QueryTopPartialAsync<T>(int count, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialWithAsync<T>(string table, int count, params string[] column);
        Task<IEnumerable<T>> QueryTopPartialOrderByAscAsync<T>(int count, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialOrderByAscWithAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialOrderByDescAsync<T>(int count, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialOrderByDescWithAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalAsync<T>(int count, string condition, object parameter, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalWithAsync<T>(string table, int count, string conditiona, object parameter, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscWithAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescWithAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);







        /*
         * raw sql
         */
        ICursor QueryCursor(string sql);
        ICursor QueryCursor<T>(string sql);
        ICursor QueryCursor<T>(string template, object obj);
        ICursor QueryCursor<T>(SqlTemplate template, object obj);



        /*
         *  full column
         */
        ICursor QueryCursor<T>();
        ICursor QueryWithCursor<T>(string table);
        ICursor QueryOrderByAscCursor<T>(string orderBy);
        ICursor QueryOrderByAscWithCursor<T>(string table, string orderBy);
        ICursor QueryOrderByDescCursor<T>(string orderBy);
        ICursor QueryOrderByDescWithCursor<T>(string table, string orderBy);

        /*
         *  full conditional
         */
        ICursor QueryConditionalCursor<T>(string condition, object parameter);
        ICursor QueryConditionalWithCursor<T>(string table, string condition, object parameter);
        ICursor QueryConditionalOrderByAscCursor<T>(string condition, object parameter, string orderBy);
        ICursor QueryConditionalWithCursor<T>(string table, string condition, object parameter, string orderBy);

        /*
         *  partial
         */
        ICursor QueryPartialCursor<T>(params string[] columns);
        ICursor QueryPartialWithCursor<T>(string table, params string[] columns);
        ICursor QueryPartialOrderByAscCursor<T>(string orderBy, params string[] columns);
        ICursor QueryPartialOrderByAscWithCursor<T>(string table, string orderBy, params string[] columns);
        ICursor QueryPartialOrderByDescCursor<T>(string orderBy, params string[] columns);
        ICursor QueryPartialOrderByDescWithCursor<T>(string table, string orderBy, params string[] columns);
        // partial conditional
        ICursor QueryPartialConditionalCursor<T>(string condition, object parameter, params string[] columns);
        ICursor QueryPartialConditionalOrderByAscCursor<T>(string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByAscWithCursor<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByDescCursor<T>(string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByDescWithCursor<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalWithCursor<T>(string table, string condition, object parameter, params string[] columns);

        /*
         *  top
         */
        // top full
        ICursor QueryTopCursor<T>(int count);
        ICursor QueryTopWithCursor<T>(string table,int count);
        ICursor QueryTopOrderByAscCursor<T>(int count, string orderBy);
        ICursor QueryTopOrderByAscWithCursor<T>(string table, int count, string orderBy);
        ICursor QueryTopOrderByDescCursor<T>(int count, string orderBy);
        ICursor QueryTopOrderByDescWithCursor<T>(string table, int count, string orderBy);
        // full conditional
        ICursor QueryTopConditionalCursor<T>(int count, string condition, object parameter);
        ICursor QueryTopConditionalWithCursor<T>(string table, int count, string condition, object parameter);
        ICursor QueryTopConditionalOrderByAscCursor<T>(int count, string condition, object parameter, string orderBy);
        ICursor QueryTopConditionalOrderByAscWithCursor<T>(string table, int count, string condition, object parameter, string orderBy);
        ICursor QueryTopConditionalOrderByDescCursor<T>(int count, string condition, object parameter, string orderBy);
        ICursor QueryTopConditionalOrderByDescWithCursor<T>(string table, int count, string condition, object parameter, string orderBy);
        // top  partial 
        ICursor QueryTopPartialCursor<T>(int count, params string[] columns);
        ICursor QueryTopPartialWithCursor<T>(string table, int count, params string[] column);
        ICursor QueryTopPartialOrderByAscCursor<T>(int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByAscWithCursor<T>(string table, int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByDescCursor<T>(int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByDescWithCursor<T>(string table, int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalCursor<T>(int count, string condition, object parameter, params string[] columns);
        ICursor QueryTopPartialConditionalWithCursor<T>(string table, int count, string conditiona, object parameter, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByAscCursor<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByAscWithCursor<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByDescCursor<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByDescWithCursor<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);




        /*
         * raw sql
         */
        Task<ICursor> QueryCursorAsync<T>(string sql);
        Task<ICursor> QueryCursorAsync<T>(string template, object obj);
        Task<ICursor> QueryCursorAsync<T>(SqlTemplate template, object obj);



        /*
         *  full column
         */
        Task<ICursor> QueryCursorAsync<T>();
        Task<ICursor> QueryWithCursorAsync<T>(string table);
        Task<ICursor> QueryOrderByAscCursorAsync<T>(string orderBy);
        Task<ICursor> QueryOrderByAscWithCursorAsync<T>(string table, string orderBy);
        Task<ICursor> QueryOrderByDescCursorAsync<T>(string orderBy);
        Task<ICursor> QueryOrderByDescWithCursorAsync<T>(string table, string orderBy);

     

        /*
         *  partial
         */
        Task<ICursor> QueryPartialCursorAsync<T>(params string[] columns);
        Task<ICursor> QueryPartialWithCursorAsync<T>(string table, params string[] columns);
        Task<ICursor> QueryPartialOrderByAscCursorAsync<T>(string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByAscWithCursorAsync<T>(string table, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByDescCursorAsync<T>(string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByDescWithCursorAsync<T>(string table, string orderBy, params string[] columns);
        // partial conditional
        Task<ICursor> QueryPartialConditionalCursorAsync<T>(string condition, object parameter, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByAscCursorAsync<T>(string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByAscWithCursorAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByDescCursorAsync<T>(string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByDescWithCursorAsync<T>(string table, string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalWithCursorAsync<T>(string table, string condition, object parameter, params string[] columns);

        /*
         *  top
         */
        // top full
        Task<ICursor> QueryTopCursorAsync<T>(int count);
        Task<ICursor> QueryTopWithCursorAsync<T>(string table,int count);
        Task<ICursor> QueryTopOrderByAscCursorAsync<T>(int count, string orderBy);
        Task<ICursor> QueryTopOrderByAscWithCursorAsync<T>(string table, int count, string orderBy);
        Task<ICursor> QueryTopOrderByDescCursorAsync<T>(int count, string orderBy);
        Task<ICursor> QueryTopOrderByDescWithCursorAsync<T>(string table, int count, string orderBy);
        // full conditional
        Task<ICursor> QueryTopConditionalCursorAsync<T>(int count, string condition, object parameter);
        Task<ICursor> QueryTopConditionalWithCursorAsync<T>(string table, int count, string condition, object parameter);
        Task<ICursor> QueryTopConditionalOrderByAscCursorAsync<T>(int count, string condition, object parameter, string orderBy);
        Task<ICursor> QueryTopConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy);
        Task<ICursor> QueryTopConditionalOrderByDescCursorAsync<T>(int count, string condition, object parameter, string orderBy);
        Task<ICursor> QueryTopConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy);
        // top  partial 
        Task<ICursor> QueryTopPartialCursorAsync<T>(int count, params string[] columns);
        Task<ICursor> QueryTopPartialWithCursorAsync<T>(string table, int count, params string[] column);
        Task<ICursor> QueryTopPartialOrderByAscCursorAsync<T>(int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByAscWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByDescCursorAsync<T>(int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByDescWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalCursorAsync<T>(int count, string condition, object parameter, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalWithCursorAsync<T>(string table, int count, string conditiona, object parameter, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByAscCursorAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByDescCursorAsync<T>(int count, string condition, object parameter, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, object parameter, string orderBy, params string[] columns);

        #endregion

        /*
         * batch insert
         */
        int BatchInsert<T>(IEnumerable<object> data,DbTransaction transanction=null);
        int BatchInsertWith<T>(string table, IEnumerable<object> datas, DbTransaction transanction=null);
        int BatchInsertPartial<T>(string[] columns, IEnumerable<object> data,DbTransaction transanction= null);
        int BatchInsertPartialWith<T>(string table, string[] columns, IEnumerable<object> datas, DbTransaction transanction=null);


        /*
         *  create
         */ 
        int Create<T>(DbTransaction transaction = null);
        int CreateWith<T>(string name, DbTransaction transaction = null);

        /*
         * delete 
         */ 
        int Delete(string table, string condition, object parameter, DbTransaction transanction=null);
        int Delete<T>(string condition, object parameter, DbTransaction transanction=null);

        /*
         *  drop
         */ 
        int Drop(string name, DbTransaction transaction = null);
        int Drop<T>(DbTransaction transaction = null);


        /*
         *  excute commond
         */ 
        int Excute(string sql, DbTransaction transanction=null);

        int Excute(string template, object obj, DbTransaction transanction=null);

        int Excute(SqlTemplate template, object obj, DbTransaction transanction=null);

       
        /*
         *  insert single row
         */ 
        int Insert<T>(object data,DbTransaction transaction = null);
        int InsertWith<T>(string table, object data, DbTransaction transaction = null);
        int InsertPartial<T>( object data, DbTransaction transaction = null,params string[] columns);
        int InsertPartialWith<T>(string table,  object data, DbTransaction transaction = null,params string[] columns);


        /*
         *  update
         *  e.g
         *  Update(new{name="jasmine"})
         *  sql generated  : update table  set name='jasmine'
         *  requie anonimous  class 's properties all exist in table,case is ignored
         *  
         *  e.g
         *  Update(new{name="jasmine" ,food=new {name="ss"} })
         *  
         *  sql generated : update table set name='jasmine', food_name='ss'
         */
        int Update<T>(object parameter,DbTransaction transaction=null);
        int UpdateWith<T>(string table, object parameter, DbTransaction transaction = null);
        int UpdateConditional<T>(object parameter, string condition, object conditionParameter=null, DbTransaction transaction = null);
        int UpdateConditionalWidth<T>(string table, object parameter, string condition,object conditionParameter=null, DbTransaction transaction = null);


        /*
        * batch insert
        */
        Task<int> BatchInsertAsync<T>(IEnumerable<object> data, DbTransaction transanction=null);
        Task<int> BatchInsertWithAsync<T>(string table, IEnumerable<object> datas, DbTransaction transanction=null);
        Task<int> BatchInsertPartialAsync<T>(IEnumerable<object> data, DbTransaction transanction , params string[] columns);
        Task<int> BatchInsertPartialWithAsync<T>(string table,  IEnumerable<object> datas, DbTransaction transanction , params string[] columns);


        /*
         *  create
         */
        Task<int> CreateAsync<T>(DbTransaction transaction = null);
        Task<int> CreateWithAsync<T>(string name, DbTransaction transaction = null);

        /*
         * delete 
         */
        Task<int> DeleteAsync(string table, string condition, object parameter, DbTransaction transanction=null);
        Task<int> DeleteAsync<T>(string condition, object parameter, DbTransaction transanction=null);

        /*
         *  drop
         */
        Task<int> DropAsync(string name, DbTransaction transaction = null);
        Task<int> DropAsync<T>(DbTransaction transaction = null);


        /*
         *  excute commond
         */
        Task<int> ExcuteAsync(string sql, DbTransaction transanction=null);

        Task<int> ExcuteAsync(string template, object obj, DbTransaction transanction=null);

        Task<int> ExcuteAsync(SqlTemplate template, object obj, DbTransaction transanction=null);


        /*
         *  insert single row
         */
        Task<int> InsertAsync<T>(object data, DbTransaction transaction = null);
        Task<int> InsertWithAsync<T>(string table, object data, DbTransaction transaction = null);
        Task<int> InsertPartialAsync<T>( object data, DbTransaction transaction ,params string[] columns );
        Task<int> InsertPartialWithAsync<T>(string table,  object data, DbTransaction transaction ,params string[] columns);


        /*
         *  update
         */
        Task<int> UpdateAsync<T>(object parameter, DbTransaction transaction = null);
        Task<int> UpdateWithAsync<T>(string table, object parameter, DbTransaction transaction = null);
        Task<int> UpdateConditionalAsync<T>(object parameter, string condition,object conditionParameter=null, DbTransaction transaction = null);
        Task<int> UpdateConditionalWidthAsync<T>(string table,  object parameter, string condition,object conditionParameter=null, DbTransaction transaction = null);


    }
}
