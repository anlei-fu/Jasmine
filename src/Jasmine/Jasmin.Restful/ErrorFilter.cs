using System;
using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful.Filters
{
    public class ErrorFilter : IRequestFilter
    {
        public string Name => throw new NotImplementedException();

        public IRequestFilter Next { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IFilter<HttpFilterContext> IFilter<HttpFilterContext>.Next => throw new NotImplementedException();

        public Task Filt(HttpFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
