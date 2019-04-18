namespace Jasmine.Common
{
    public  interface IFilterFactory<T>
    {
        IFilter<T> Create();
    }
}
