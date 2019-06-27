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
        public override Task<bool> FiltsAsync(HttpFilterContext context)
        {
            bool pass = false;

            if(context.PipelineDatas.ContainsKey("level"))
            {
                if ((int)(AuthenticateLevel)context.PipelineDatas["level"] >= (int)_level)
                    pass = true;
            }

            return Task.FromResult(pass);
        }
    }
}
