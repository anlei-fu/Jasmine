using Jasmine.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Orm.Interfaces
{
    /// <summary>
    ///数据库常用操作封装
    /// </summary>
    public interface ISqlExcuter
    {
        /// <summary>
        /// query
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <returns>results of enumerable</returns>
        IEnumerable<T> Query<T>();
        /// <summary>
        /// query with condition
        /// e.g (a> @a and b<@b)
        /// 
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <returns>results of enumerable</returns>
        IEnumerable<T> QueryWithCondition<T>(string condition,object parameter);
        /// <summary>
        ///query
        /// </summary>
        /// <typeparam name="T"> modal type</typeparam>
        /// <param name="sql"> raw sql </param>
        /// <returns>results of enumerable </returns>
        IEnumerable<T> Query<T>(string sql);
        /// <summary>
        /// query
        /// </summary>
        /// <typeparam name="T"> modal type</typeparam>
        /// <param name="template"> sql template</param>
        /// <param name="paramter">parameters to replace in <see cref="template"/></param>
        /// <returns>result of enumerable</returns>
        IEnumerable<T> Query<T>(string template, object obj);
        /// <summary>
        /// query asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>();
        /// <summary>
        /// query asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryCondictionAsync<T>();
        /// <summary>
        /// query asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <param name="sql">raw sql</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql);
        /// <summary>
        /// quer asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <param name="template">sql template</param>
        /// <param name="obj">parameters to replace in template</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(Template template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        IEnumerable<T> QueryWith<T>(string table);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        IEnumerable<T> QueryCondition<T>(string table,string condition,object parameter);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryWithAsync<T>(string table);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
       Task<IEnumerable<T>> QueryConditionWithAsync<T>(string table, string condition, object parameter);
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ICursor QueryCursor(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        ICursor QueryCursor(string template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCursor(Template template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ICursor QueryCursor<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ICursor QueryCursorCondition<T>(string condition,object parameter);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<ICursor> QueryCursorAsync(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<ICursor> QueryCursorAsync(string template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<ICursor> QueryCursorAsync(Template template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ICursor> QueryCursorAysnc<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ICursor> QueryCursorConditionAsync<T>(string condition, object parameter);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>

        ICursor QueryCursorWith<T>(string table);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<ICursor> QueryCursorWithAsync<T>(string table);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Excute(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> ExcuteAsync(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(string template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(Template template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<int> ExcuteAsync(string template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        /// <returns></returns>

        Task<int> ExcuteAsync(Template template, object obj);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        int Insert<T>(T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        int Insert<T>(string template,T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync<T>(T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> InsertAsync<T>(string template, T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        int InsertWith<T>( string table,T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        int InsertWith<T>(string table,string template, T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<int> InsertWithAsync<T>(string table,T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<int> InsertWithAsync<T>(string table, string template, T data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        int BatchInsert<T>(IEnumerable<T> data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        int BatchInsert<T>(string template,IEnumerable<T> data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> BatchInsertAsync<T>(IEnumerable<T> data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> BatchInsertAsync<T>(string template, IEnumerable<T> data);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        int BatchInsertWith<T>(string table,IEnumerable<T> datas );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        int BatchInsertWith<T>(string table,string template,IEnumerable<T> datas );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<int> BatchInsertWithAsync<T>(string table,IEnumerable<T> datas );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<int> BatchInsertWithAsync<T>(string table, string template,IEnumerable<T> datas);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Drop<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> DropAsync<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        int DropWith<T>(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> DropWithAync<T>(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Create<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CreateAsync<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        int CreateWith<T>(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> CreateWithAsync<T>(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Update<T>(object parameter);
        Task<int> UpdateAsync<T>(object parameter);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateWithAsync<T>(string table, object parameter);

        int UpdateConditional<T>(string template, object parameter);
        Task<int> UpdateConditionalAsyn<T>(string template, object parameter);

        int UpdateConditionalWidth<T>(string table,string template, object parameter);
        Task<int> UpdateConditionalWidthAsyn<T>(string table, string template, object parameter);




    }
}
