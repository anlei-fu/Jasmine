using System;
using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class MethodFilter : AbstractFilter<HttpFilterContext>
    {
        private Func<string, string> _methodGetter;
        public MethodFilter(string name) : base(name)
        {
        }

        public override Task Filt(HttpFilterContext context)
        {
            if (!string.Equals(context.HttpContext.Request.Method, _methodGetter(context.HttpContext.Request.Path), StringComparison.OrdinalIgnoreCase))
            {
                context.HttpContext.Response.StatusCode = 5;
                return Task.CompletedTask;
            }
            else
            {
                return HasNext ? Next.Filt(context) : Task.CompletedTask;
            }
        }
    }
}
