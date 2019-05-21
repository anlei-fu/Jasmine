using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface IFilterPipeline<T>:IFilterPiplelineBuilder<T>,IReadOnlyCollection<IFilter<T>>
    {
        IFilter<T> First { get; }
        IFilter<T> Last { get; }
      
        bool Contains(string name);

    }
}
