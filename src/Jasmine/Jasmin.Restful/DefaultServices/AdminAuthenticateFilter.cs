using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class AdminAuthenticateFilter : AuthenticateFilter
    {
        public AdminAuthenticateFilter() : base(AuthenticateLevel.Admin)
        {
        }
    }
}
