using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAopFilterProvider<TContext>:IReadOnlyCollection<IFilter<TContext>>
    {
        IFilter<TContext> GetFilter(string name);
        void AddFilter(IFilter<TContext> filter);
        void Remove(string name);
        bool Contains(string name);
        void Clear();
    }
}
