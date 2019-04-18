using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IFilterPipeline<T>:IReadOnlyCollection<IFilter<T>>
    {
        
        IFilter<T> Root { get; }
    }
}
