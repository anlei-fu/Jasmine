using System.Collections.Generic;

namespace Jasmine.Cache
{
    public  interface ITimeoutCache<TKey,TValue>:IReadOnlyCollection<KeyValuePair<TKey,TValue>>
    {
        bool Cache(TKey key, TValue value, long timeout);
        TValue GetValue(TKey key);
        bool AdjustTimeout(TKey key,long timeout);
        bool Conatins(TKey key);
        void Delete(TKey key);
        
    }
}
