namespace Jasmine.Common
{
    public  interface IInterceptor<T>:IFilter<T>
    {
        bool Intercept(string path);
    }
}
