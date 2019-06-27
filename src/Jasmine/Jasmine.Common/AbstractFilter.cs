using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractFilter<T> : IFilter<T>
        where T:IFilterContext
    {
        public AbstractFilter()
        {
          
        }
        public  string Name => GetType().FullName;

        public IFilterPipeline<T> Pipeline { get; set; }

        public abstract Task<bool> FiltsAsync(T context);
       
    }
}
