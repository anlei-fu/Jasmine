using Microsoft.AspNetCore.Http;

namespace Jasmine.Restful
{
    public  interface ICookieValidator
    {
        bool Validate(HttpResponse response,IRequestCookieCollection cookies);
    }
}
