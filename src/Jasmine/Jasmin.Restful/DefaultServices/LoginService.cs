using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;
using System.Collections.Concurrent;

namespace Jasmine.Restful.DefaultServices
{
    [Restful]
    public  class LoginService
    {
        private ConcurrentDictionary<string, string> _user = new ConcurrentDictionary<string, string>();

        public LoginService()
        {
            _user.TryAdd("admin", "admin");
        }
        [AfterInterceptor("Jasmine.Restful.DefaultServices.LoginAfterInterceptor")]
        [Path("/api/login")]
        public bool Login(string user,string password)
        {
            return _user.TryGetValue(user, out var value) ? value == password : false;
        }
    }
}
