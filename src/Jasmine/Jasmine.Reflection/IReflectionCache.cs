using System;
using System.Collections.Generic;

namespace Jasmine.Reflection
{
    public   interface IReflectionCache<TContent,TRelated>:INameMapper<TContent>,IReadOnlyCollection<TContent>
    {
        IEnumerable<TContent> Get(Predicate<TContent> predict);
        IEnumerable<TContent> GetAll();
        TContent GetItem(TRelated info);
        bool Contains(TRelated info);
        void Cache(TRelated info);
    }
}
