namespace Jasmine.Common
{
    public   interface IParamteterResolver<T>
    {
        object[] Resolve(T context);
    }
}
