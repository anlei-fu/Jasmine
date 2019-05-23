using Jasmine.Common;

namespace Jasmine.Restful
{
    internal class RestfulParameterResolverFactory : IParameterResolverFactory<HttpFilterContext,RestfulRequestMetaData>
    {
        private RestfulParameterResolverFactory()
        {

        }

        public static readonly IParameterResolverFactory<HttpFilterContext, RestfulRequestMetaData> Instance = new RestfulParameterResolverFactory();

        public IRequestParamteterResolver<HttpFilterContext> Create(RestfulRequestMetaData metaData)
        {
            return new RestfulRequestParameterResolver(metaData.Parameters);
        }
    }
}
