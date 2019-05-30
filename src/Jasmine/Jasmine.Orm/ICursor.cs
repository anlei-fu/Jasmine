using Jasmine.Orm.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Orm.Interfaces
{
    /// <summary>
    /// it's iterator for sql select  result collection
    /// </summary>
    public  interface ICursor
    {
        ConcurrentDictionary<string,QuryResultColumnMetaInfo> Columns { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T ReadOne<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> ReadOne();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Read<T>(int count);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<IEnumerable<object>> Read(int count);
        /// <summary>
        /// 
        /// </summary>
        void Close();
        /// <summary>
        /// 
        /// </summary>
        bool HasRow { get; }
    }
}
