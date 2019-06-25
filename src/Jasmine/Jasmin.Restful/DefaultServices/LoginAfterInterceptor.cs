using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class LoginAfterInterceptor : AbstractFilter<HttpFilterContext>
    {
        public LoginAfterInterceptor(ISessionManager manager,IUserManager userManager ) 
        {
            _sessionManager = manager;
            _userManager = userManager;
        }
        private ISessionManager _sessionManager;
        private IUserManager _userManager;

        public override Task FiltsAsync(HttpFilterContext context)
        {
            if (context.ReturnValue is true)
            {
                var user = _userManager.GetUser(context.HttpContext.Request.Query["user"]);



                context.HttpContext.Response.Cookies.Append("session", _sessionManager.CreateSession(user));
            }

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
    }
}
