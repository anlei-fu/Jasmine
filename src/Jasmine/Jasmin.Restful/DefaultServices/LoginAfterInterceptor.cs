using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful.DefaultServices
{
    public class LoginAfterInterceptor : AbstractFilter<HttpFilterContext>
    {
        public LoginAfterInterceptor(ISessionManager manager) 
        {
            _manager = manager;
        }
        private ISessionManager _manager;

        public override Task FiltsAsync(HttpFilterContext context)
        {
            //if (context.ReturnValue is true)
            //    context.HttpContext.Response.Cookies.Append("session", _manager.CreateSession(DefaultFilters.AuntenticateLevel.Admin));

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
    }
}
