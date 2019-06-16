using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class SessionValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public SessionValidateFilter(ISessionManager manager) : base("Session-Validate-Filter")
        {
            _manager = manager;
        }
        private ISessionManager _manager;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("session", out var session);

            var level = _manager.GetSession(session);

            if (level!=null)
            {
                context.Datas.Add("level", (AuntenticateLevel)level);

                return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
            }
            else
            {
                context.HttpContext.Response.StatusCode = HttpStatusCodes.VALIDATE_FAILED;

                return Task.CompletedTask;
            }
            
        }
    }
}
