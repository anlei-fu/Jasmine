using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class AfterLoginFilter : AbstractFilter<HttpFilterContext>
    {
        public AfterLoginFilter(ISessionManager manager,IUserManager userManager ) 
        {
            _sessionManager = manager;
            _userManager = userManager;
        }
        private ISessionManager _sessionManager;
        private IUserManager _userManager;

        public override Task<bool> FiltsAsync(HttpFilterContext context)
        {
            if (context.ReturnValue is true)
            {
                var user = _userManager.GetUser(context.HttpContext.Request.Query["user"]);

                context.HttpContext.Response.Cookies.Append("session", _sessionManager.CreateSession(user));
            }

            return Task.FromResult(true);
        }
    }
}
