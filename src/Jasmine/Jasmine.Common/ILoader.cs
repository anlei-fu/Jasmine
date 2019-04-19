namespace Jasmine.Common
{
    public interface ILoader<in TKey,out TValue>
    {
        TValue Load(TKey key);
    }
}
