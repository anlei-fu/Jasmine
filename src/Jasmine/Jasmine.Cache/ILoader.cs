namespace Jasmine.Cache
{
    public  interface ILoader<Tkey,TValue>
    {
        TValue Load(Tkey key);
    }
}
