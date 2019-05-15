using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    /// use to dispatch a request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  interface IDispatcher<T>:INameFearture
    {
        /// <summary>
        /// dispatch every request
        /// </summary>
        /// <param name="path">request path</param>
        /// <param name="context">filter context</param>
        /// <returns></returns>
        Task DispatchAsync(string path, T context);
    }
}
