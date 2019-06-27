using System.Threading.Tasks;

namespace Jasmine.Common
{
    public interface IFilter<TContext>:INameFearture
        where TContext:IFilterContext
    {
        /// <summary>
        ///  do filt
        /// </summary>
        /// <param name="context">filter context</param>
        /// <returns> decide does run next filter</returns>
        Task<bool> FiltsAsync(TContext context);
    

        IFilterPipeline<TContext> Pipeline { get; set; }
    }
}
