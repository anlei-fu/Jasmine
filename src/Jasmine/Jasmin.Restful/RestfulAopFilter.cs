using Jasmine.Common;

namespace Jasmine.Restful
{
    public abstract class RestfulAopFilter : AbstractFilter<HttpFilterContext>
    {
        public RestfulAopFilter(string name) : base(name)
        {
        }
    }
}
