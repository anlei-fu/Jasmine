using System;
using Microsoft.AspNetCore.Http;

namespace Jasmine.Restful.Implement
{
    public class JasmineRestfulMiddleWareFactory : IMiddlewareFactory
    {
        public IMiddleware Create(Type middlewareType)
        {
            throw new NotImplementedException();
        }

        public void Release(IMiddleware middleware)
        {
            throw new NotImplementedException();
        }
    }
}
