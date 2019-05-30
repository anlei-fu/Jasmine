using Jasmine.Configuration;
using System.Collections.Generic;

namespace Jasmine.Orm.Interfaces
{
    /// <summary>
    ///数据库常用操作封装
    /// </summary>
    public interface ISqlExcuter
    {
        /// <summary>
        /// 查询，以Ienumerable 的形式返回所有结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql,IDbConnectionProvider provider);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string sql, IDbConnectionProvider provider);
        /// <summary>
        /// 带参数的查询，返回所有结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string template, object obj, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string template, object obj, IDbConnectionProvider provider);
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(Template template, object obj, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(Template template, object obj, IDbConnectionProvider provider);
      
        /// <summary>
        /// 查询，以游标的方式返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ICursor QueryCursor(string sql,IDbConnectionProvider provider);
        /// <summary>
        /// 带参数的查询，以游标返回
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        ICursor QueryCursor(string template, object obj, IDbConnectionProvider provider);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCursor(Template template, object obj, IDbConnectionProvider provider);
       
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Excute(string sql, IDbConnectionProvider provider);
        /// <summary>
        /// 带参数的命令执行
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(string template,object obj, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(Template template, object obj, IDbConnectionProvider provider);

        int BatchInsert<T>(IEnumerable<T> data, IDbConnectionProvider provider);
        int BatchInsert<T>(IEnumerable<T> datas, string table, IDbConnectionProvider provider);
        int Insert<T>(T data, IDbConnectionProvider provider);
        int Insert<T>(T data,string table, IDbConnectionProvider provider);
        int Drop<T>( IDbConnectionProvider provider);
        int Drop<T>(string name, IDbConnectionProvider provider);
        int Create(string name, IDbConnectionProvider provider);
        int Create<T>(string name, IDbConnectionProvider provider);
        ICursor QueryCursor<T>( IDbConnectionProvider provider);
        ICursor QueryCursor<T>(string table, IDbConnectionProvider provider);
        IEnumerable<T> Query<T>(IDbConnectionProvider provider);
        IEnumerable<T> QueryWith<T>(string table, IDbConnectionProvider provider);



    }
}
