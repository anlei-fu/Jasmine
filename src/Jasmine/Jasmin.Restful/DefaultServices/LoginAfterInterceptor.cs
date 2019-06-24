using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class LoginAfterInterceptor : AbstractFilter<HttpFilterContext>
    {
        public LoginAfterInterceptor(ISessionManager manager,IUserManager ) 
        {
            _sessionManager = manager;
        }
        private ISessionManager _sessionManager;

        public override Task FiltsAsync(HttpFilterContext context)
        {
            if (context.ReturnValue is true)
            {
                context.HttpContext.Response.Cookies.Append("session", _sessionManager.CreateSession(context.HttpContext.Request.Query["user"]);
            }

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
    }
}
