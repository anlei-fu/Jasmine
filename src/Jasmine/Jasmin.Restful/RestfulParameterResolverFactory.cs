using Jasmine.Common;

namespace Jasmine.Restful
{
    internal class RestfulParameterResolverFactory : IParameterResolverFactory<HttpFilterContext,RestfulServiceMetaData>
    {
        private RestfulParameterResolverFactory()
        {

        }

        public static readonly IParameterResolverFactory<HttpFilterContext, RestfulServiceMetaData> Instance = new RestfulParameterResolverFactory();

        public IParamteterResolver<HttpFilterContext> Create(RestfulServiceMetaData metaData)
        {
            return new RestfulParameterResolver(metaData.Parameters);
        }
    }
}
