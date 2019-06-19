using System.Collections.Generic;

namespace Jasmine.Cache
{
    public  interface ICache<TKey,Tvalue>:IReadOnlyCollection<KeyValuePair<TKey,Tvalue>>
    {
        Tvalue GetValue(TKey key);
        bool ConatinsKey(TKey key);
        void Delete(TKey key);

        IList<TKey> Keys { get; }
        IList<Tvalue> Values { get; }
      
    }
}
