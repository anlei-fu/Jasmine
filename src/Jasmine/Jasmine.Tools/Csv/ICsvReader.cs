using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Tools.Csv
{
    public  interface ICsvReader
    {
        /// <summary>
        /// read one row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ReadOne<T>();
        /// <summary>
        /// read one row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fatory"></param>
        /// <returns></returns>
        T ReadOne<T>(Func<string[],T>fatory);
        /// <summary>
        /// reflect usage
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        dynamic ReadOne(Type type);
        /// <summary>
        /// read some records by given count
        /// maybe return less than  given count,if records not enough
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Read<T>(int count);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        IEnumerable<T>  Read<T>(int count,Func<string[],T>factory);
        /// <summary>
        /// reflect usage
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        dynamic Read(Type type, int count);
        /// <summary>
        /// read all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ReadToEnd<T>();
        /// <summary>
        /// reflect usage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<dynamic> ReadToEnd<T>(Type type);
        /// <summary>
        /// read one row
        /// </summary>
        /// <param name="consumer"> do something by given record fields </param>
        void ReadOne(Action<string[]> consumer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="count"></param>
        void Read(Action<string[]> consumer,int count);

        Task<T> ReadOneAsync<T>();
        Task<T> ReadOneAsync<T>(Func<string[], T> fatory);

        Task<dynamic> ReadAsync(Type type);

        Task<T> ReadAsync<T>(int count);
        Task<T> ReadAsync<T>(int count, Func<string[], T> factory);
        Task<dynamic> ReadAsync(Type type, int count);

        Task<IEnumerable<T>> ReadToEndAsync<T>();

        Task<IEnumerable<dynamic>> ReadToEndAsync<T>(Type type);

        Task ReadOneAsync(Action<string[]> consumer);
        Task ReadAsync(Action<string[]> consumer, int count);




    }
}
