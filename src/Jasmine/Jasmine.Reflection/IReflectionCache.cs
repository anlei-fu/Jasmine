using System.Collections.Generic;

namespace Jasmine.Reflection
{
    public   interface IReflectionCache<TContent,TRelated>:INameFearture<TContent>,IEnumerable<TContent>
    {
        IEnumerable<TContent> GetAll();
        TContent GetItem(TRelated info);
        bool Contains(TRelated info);
        void Cache(TRelated info);
        int Count { get; }
    }
}
