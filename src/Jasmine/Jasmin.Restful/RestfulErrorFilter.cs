using Jasmine.Common;

namespace Jasmine.Restful
{
    public abstract class RestfulErrorFilter : AbstractFilter<HttpFilterContext>
    {
        public RestfulErrorFilter(string name) : base(name)
        {
        }
    }
}
