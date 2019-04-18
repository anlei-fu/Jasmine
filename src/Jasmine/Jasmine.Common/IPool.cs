namespace Jasmine.Common
{
    public interface IPool<T>
    {
        T Rent();
        void Recycle(T item);
    }
}
