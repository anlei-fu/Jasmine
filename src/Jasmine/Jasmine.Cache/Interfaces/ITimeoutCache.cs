namespace Jasmine.Cache.Interfaces
{
    public  interface ITimeoutCache<TKey,TValue>
    {
        void Cache(TKey key, TValue value, int timeout);
        void AddTime(TKey key,int timeout);
        bool Conatins(TKey key);
        void Delete(TKey key);
        int Count { get; }
    }
}
