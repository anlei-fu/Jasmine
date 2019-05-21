using Jasmine.Reflection;

namespace Jasmine.Common
{
    public  interface IParameterResolverFactory<T,TMetaData>
    {
        IParamteterResolver<T> Create(TMetaData metaData);
    }
}
