using System.Threading.Tasks;

namespace Jasmine.Common
{
    public interface IFilter<T>:INameFearture
    {
        /// <summary>
        ///  do filt
        /// </summary>
        /// <param name="context">filter context</param>
        /// <returns></returns>
        Task FiltsAsync(T context);
        /// <summary>
        /// attached next filter
        /// </summary>
        IFilter<T> Next { get; set; }

        IFilterPipeline<T> Pipeline { get; set; }
    }
}
