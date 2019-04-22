using Jasmine.Orm.Model;
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
        IEnumerable<T> Query<T>(string template, SqlParameters parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string template, SqlParameters parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(SqlTemplate template, SqlParameters parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(SqlTemplate template, SqlParameters parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(SqlTemplate template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(SqlTemplate template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
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
        ICursor QueryCursor(string template, SqlParameters paramters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCusor(string template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCursor(SqlTemplate template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ICursor QueryCursor(SqlTemplate template, SqlParameters parameters, IDbConnectionProvider provider);
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
        int Excute(string template,object parameter, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(SqlTemplate template, SqlParameters parameters, IDbConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Excute(SqlTemplate template, IEnumerable<SqlParameters> parameters, IDbConnectionProvider provider);


    }
}
