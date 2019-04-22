using Jasmine.Common;
using Jasmine.Reflection;
using System;

namespace Jasmine.Restful
{
    public class RestfulParameterResolverFactory : IParameterResolverFactory<HttpFilterContext>
    {
        public IParamteterResolver<HttpFilterContext> Create(Method method)
        {
            throw new NotImplementedException();
        }
    }
}
