using Jasmine.Common;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class CookieValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public CookieValidateFilter(string name) : base(name)
        {
        }

        public override Task FiltsAsync(HttpFilterContext context)
        {
            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
    }
}
