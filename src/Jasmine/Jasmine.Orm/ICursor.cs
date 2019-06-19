using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Orm
{
    /// <summary>
    /// it's a iterator to read sql qury  results collection
    /// </summary>
    public interface ICursor :IDisposable
    {
        /// <summary>
        /// read one row
        /// </summary>
        /// <returns></returns>
        T ReadOne<T>(bool doAssociateQuery=false);
        /// <summary>
        /// read one row aysnchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ReadOneAsync<T>(bool doAssociateQuery = false);
        /// <summary>
        /// read rows ,it may return less than count rows, when  rows are not enough
        /// </summary>
        /// <param name="count">count of row to read</param>
        /// <returns></returns>
        IEnumerable<T> Read<T>(int count,bool doAssociateQuery=false);
        /// <summary>
        /// read some rows asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <param name="count">count of rows to read</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ReadAsync<T>(int count, bool withAssociate = false);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        IEnumerable<T> ReadToEnd<T>(bool doAssociateQuery = false);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> ReadToEndAsync<T>(bool doAssociateQuery = false);

        /// <summary>
        /// read one row
        /// </summary>
        /// <returns></returns>
        dynamic ReadOne(Type type,bool doAssociateQuery = false);
        /// <summary>
        /// read one row aysnchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<dynamic> ReadOneAsync(Type type,bool doAssociateQuery = false);
        /// <summary>
        /// read rows ,it may return less than count rows, when  rows are not enough
        /// </summary>
        /// <param name="count">count of row to read</param>
        /// <returns></returns>
        IEnumerable<dynamic> Read(Type type,int count, bool doAssociateQuery = false);
        /// <summary>
        /// read some rows asynchronous
        /// </summary>
        /// <typeparam name="T">modal type</typeparam>
        /// <param name="count">count of rows to read</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ReadAsync(Type type,int count, bool doAssociateQuery = false);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        IEnumerable<dynamic> ReadToEnd(Type type,bool doAssociateQuery = false);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ReadToEndAsync(Type type,bool doAssociateQuery = false);
        /// <summary>
        /// close cursor ，internal close sql connection
        /// </summary>
        void Close();
       
    }
}
