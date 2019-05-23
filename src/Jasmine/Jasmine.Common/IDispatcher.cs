using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    ///  dispatch a request
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public  interface IDispatcher<TContext>:INameFearture
    {
        /// <summary>
        /// dispatch every request
        /// </summary>
        /// <param name="path">request path</param>
        /// <param name="context">filter context</param>
        /// <returns></returns>
        Task DispatchAsync(string path, TContext context);
    }
}
