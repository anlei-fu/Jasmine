using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Restful.DefaultFilters;
using System.Threading.Tasks;

namespace Jasmine.Restful.DefaultServices
{
    [BeforeInterceptor(typeof(SessionValidateFilter))]
    public class AuthenticateFilter : AbstractFilter<HttpFilterContext>
    {
    
        public AuthenticateFilter(AuthenticateLevel level) 
        {
            _level = level;
        }

        private AuthenticateLevel _level;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            bool pass = false;

            if(context.PipelineDatas.ContainsKey("level"))
            {
                if ((int)(AuthenticateLevel)context.PipelineDatas["level"] >= (int)_level)
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
