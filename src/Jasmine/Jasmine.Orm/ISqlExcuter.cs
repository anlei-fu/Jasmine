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
        IEnumerable<T> Query<T>(string sql,ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string sql, ISqlConnectionProvider provider);
        /// <summary>
        /// 带参数的查询，返回所有结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string template, IDictionary<string, object> paramter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string template, IDictionary<string, object> paramter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string template, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(string template, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Query(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 查询，以游标的方式返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ICursor QueryCursor(string sql,ISqlConnectionProvider provider);
        /// <summary>
        /// 带参数的查询，以游标返回
        /// </summary>
        /// <param name="template"></param>
        /// <param name="paramter"></param>
        /// <returns></returns>
        ICursor QueryCursor(string template, IDictionary<string, object> paramter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCusor(string template, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ICursor QueryCursor(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ICursor QueryCursor(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameter, ISqlConnectionProvider provider);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Excute(string sql, ISqlConnectionProvider provider);
        /// <summary>
        /// 带参数的命令执行
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(string template,object parameter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int Excute(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameter, ISqlConnectionProvider provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Excute(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, ISqlConnectionProvider provider);


    }
}
