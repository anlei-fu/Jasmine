using Jasmine.Orm.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Orm.Interfaces
{
    /// <summary>
    /// it's iterator for sql select  result collection
    /// </summary>
    public interface ICursor:IDisposable
    {
        /// <summary>
        /// still has any row to read
        /// </summary>
        bool HasRow { get; }
        ConcurrentDictionary<string, QuryResultColumnInfo> Columns { get; }
        /// <summary>
        /// read one row
        /// </summary>
        /// <returns></returns>
        T ReadOne<T>();
        /// <summary>
        /// read one row aysnchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ReadOneAsync<T>();
        /// <summary>
        /// read rows ,it may return less count rows, when  rows are not enough
        /// </summary>
        /// <param name="count">count of row to read</param>
        /// <returns></returns>
        IEnumerable<T> Read<T>(int count);
        /// <summary>
        /// read some rows asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <param name="count">count of rows to read</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ReadAsync<T>(int count);
        /// <summary>
        /// close cursor ，internal close sql connection
        /// </summary>
        void Close();
       
    }
}
