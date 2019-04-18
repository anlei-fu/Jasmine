using Jasmine.Common;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class ValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public ValidateFilter(string name) : base(name)
        {
        }

        public override Task Filt(HttpFilterContext context)
        {
            return HasNext ? Next.Filt(context) : Task.CompletedTask;
        }
    }
}
