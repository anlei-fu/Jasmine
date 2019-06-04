using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractFilter<T> : IFilter<T>
    {
        public AbstractFilter(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public IFilter<T> Next { get; set; }

        public bool HasNext => Next != null;

        public IFilterPipeline<T> Pipeline { get; set; }

        public abstract Task FiltsAsync(T context);
       
    }
}
