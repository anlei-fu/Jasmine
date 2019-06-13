using Jasmine.Common;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class CookieValidateFilter : AbstractFilter<HttpFilterContext>
    {
        public CookieValidateFilter(ICookieValidator validator) : base("cookie-validate-filter")
        {
            _validator = validator;
        }
        private ICookieValidator _validator;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            if(_validator.Validate(context.HttpContext.Response,context.HttpContext.Request.Cookies))
            {
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
