using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    internal class RpcParameterResolverFactory : IParameterResolverFactory<RpcFilterContext,RpcRequestMetaData>
    {
        private RpcParameterResolverFactory()
        {

        }

        public static readonly IParameterResolverFactory<RpcFilterContext, RpcRequestMetaData> Instance = new RpcParameterResolverFactory();

        public IRequestParamteterResolver<RpcFilterContext> Create(RpcRequestMetaData metaData)
        {
            return new RpcRequestParameterResolver(metaData.Parameters);
        }
    }
}
