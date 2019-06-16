using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    [Restful]
    public  class LoginService
    {
        public LoginService(IUserManager manager)
        {
            _userManager = manager;
        }

        private IUserManager _userManager;
        [AfterInterceptor("Jasmine.Restful.DefaultServices.LoginAfterInterceptor")]
        [Path("/api/login")]
        public bool Login(string user,string password)
        {
            return _userManager.Validate(user,password);
        }
    }
}
