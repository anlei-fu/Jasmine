namespace Jasmine.Restful.DefaultServices
{
    public  class UserAuthenticateFilter:AuthenticateFilter
    {
        public UserAuthenticateFilter():base(DefaultFilters.AuthenticateLevel.User)
        {

        }
    }
}
