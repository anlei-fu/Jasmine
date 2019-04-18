using Jasmine.Common;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Filters
{
    public class SuccessedFilter : AbstractFilter<HttpFilterContext>
    {
        public SuccessedFilter(string name) : base(name)
        {
        }

        public override Task Filt(HttpFilterContext context)
        {
            context.HttpContext.Response.StatusCode = 200;

            return HasNext ? Next.Filt(context) : Task.CompletedTask;

        }
    }
}
