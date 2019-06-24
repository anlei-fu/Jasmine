using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAopFilterProvider<TContext>:IReadOnlyCollection<IFilter<TContext>>
    {
   
        IFilter<TContext> GetFilter<T>();
        IFilter<TContext> GetFilter(Type type);
        void AddFilter(IFilter<TContext> filter);
       
       
        void Remove<T>();
        void Remove(Type type);
        bool Contains(Type type);
        bool Contains<T>();
      
        void Clear();
    }
}
