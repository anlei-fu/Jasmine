using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;
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
        public override Task<bool> FiltsAsync(HttpFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("session", out var session);

            User user = null;

            if (session != null)
                user = _manager.GetUserBySession(session);

            if (user != null)
            {
                context.PipelineDatas.Add("level", user.Group.Level);

                return Task.FromResult(true);
            }
            else
            {
                context.HttpContext.Response.StatusCode = HttpStatusCodes.VALIDATE_FAILED;

                return Task.FromResult(false);
            }

        }
    }
}
