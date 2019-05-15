namespace Jasmine.Common
{
    public interface IFilterPipeline<T>
    {
        IFilter<T> First { get; }
        IFilter<T> Last { get; }
        int Count { get; }
        void AddFirst(IFilter<T> filter);
        void AddLast(IFilter<T> filter);
        void AddBefore(string name, IFilter<T> filter);
        void AddAfter(string name, IFilter<T> filter);
        bool Contains(string name);

    }
}
