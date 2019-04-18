using Jasmine.Reflection;

namespace Jasmine.Common
{
    public  interface IParameterResolverFactory<T>
    {
        IParamteterResolver<T> Create(Method method);
    }
}
