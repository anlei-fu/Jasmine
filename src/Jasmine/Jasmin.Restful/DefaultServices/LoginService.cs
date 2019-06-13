using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;
using Jasmine.Restful.DefaultFilters;
using System.Collections.Concurrent;

namespace Jasmine.Restful.DefaultServices
{
    [Restful]
    public  class LoginService
    {
        private ConcurrentDictionary<string, string> _user = new ConcurrentDictionary<string, string>();

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
