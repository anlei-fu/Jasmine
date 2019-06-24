using Jasmine.Common;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class SessionValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public SessionValidateFilter(ISessionManager manager) 
        {
            _manager = manager;
        }
        private ISessionManager _manager;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("session", out var session);

            var user = _manager.GetUserBySession(session);

            if (user!=null)
            {
                context.PipelineDatas.Add("level", user);

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
