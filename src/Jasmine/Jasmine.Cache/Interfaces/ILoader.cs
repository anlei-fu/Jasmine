namespace Jasmine.Cache.Interfaces
{
    public  interface ILoader<Tkey,TValue>
    {
        TValue Load(Tkey key);
    }
}
