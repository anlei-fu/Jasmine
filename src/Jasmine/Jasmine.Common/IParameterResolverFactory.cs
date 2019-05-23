using Jasmine.Reflection;

namespace Jasmine.Common
{
    public  interface IParameterResolverFactory<T,TMetaData>
    {
        IRequestParamteterResolver<T> Create(TMetaData metaData);
    }
}
