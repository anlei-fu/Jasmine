using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IFilterPipeline<T>:IReadOnlyCollection<IFilter<T>>,IPathFearture
    {
        IFilter<T> ErrorFilter { get; }
        IFilter<T> Root { get; }
        IStat<IStatItem> Stat { get; }

    }
}
