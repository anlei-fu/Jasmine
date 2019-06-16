namespace Jasmine.Cache
{
    public  interface ITimeoutCache<TKey,TValue>:ICache<TKey,TValue>
    {
        bool Cache(TKey key, TValue value, long timeout);
        bool AdjustTimeout(TKey key,long timeout);
        
    }
}
