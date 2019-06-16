using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAopFilterProvider<TContext>:IReadOnlyCollection<IFilter<TContext>>
    {
        IFilter<TContext> GetFilter(string name);
        IFilter<TContext> GetFilter<T>();
        IFilter<TContext> GetFilter(Type type);
        void AddFilter(IFilter<TContext> filter);
        void AddFilter(string name, Type type);
        void AddFilter<T>() where T : IFilter<TContext>;
        void Remove(string name);
        void Remove<T>();
        void Remove(Type type);
        bool Contains(Type type);
        bool Contains<T>();
        bool Contains(string name);
        void Clear();
    }
}
