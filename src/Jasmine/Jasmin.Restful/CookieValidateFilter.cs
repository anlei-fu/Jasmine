using Jasmine.Common;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class CookieValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public CookieValidateFilter(string name) : base(name)
        {
        }

        public override Task FiltAsync(HttpFilterContext context)
        {
            return HasNext ? Next.FiltAsync(context) : Task.CompletedTask;
        }
    }
}
