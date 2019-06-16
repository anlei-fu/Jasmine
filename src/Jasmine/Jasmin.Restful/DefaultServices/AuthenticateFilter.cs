using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class AuthenticateFilter : AbstractFilter<HttpFilterContext>
    {
        public AuthenticateFilter(AuntenticateLevel level) : base("Aunthenticate-Filter")
        {
            _level = level;
        }

        private AuntenticateLevel _level;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            bool pass = false;

            if(context.Datas.ContainsKey("level"))
            {
                if ((int)context.Datas["level"] >= (int)_level)
                    pass = true;
            }

            if (!pass)
            {
                context.HttpContext.Response.StatusCode = HttpStatusCodes.FORBIDDEN;

                return Task.CompletedTask;
            }
            else
            {
                return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
            }
        }
    }
}
