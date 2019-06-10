namespace Jasmine.Cache
{ 
    public  interface ICache<TKey,Tvalue>
    {
        void Cache(TKey key, Tvalue value);
        bool Conatins(TKey key);
        void Delete(TKey key);
        int  Count { get; }
    }
}
