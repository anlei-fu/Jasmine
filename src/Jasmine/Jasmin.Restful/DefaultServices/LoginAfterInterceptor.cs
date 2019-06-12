using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful.DefaultServices
{
    public class LoginAfterInterceptor : AbstractFilter<HttpFilterContext>
    {
        public LoginAfterInterceptor() : base("login-after-interceptor")
        {
        }

        public override Task FiltsAsync(HttpFilterContext context)
        {
            if (context.ReturnValue is true)
                context.HttpContext.Response.Cookies.Append("token", "001");

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
    }
}
