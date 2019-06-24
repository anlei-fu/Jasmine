using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful
{
    public interface ISessionManager
    {
        string CreateSession(User user);
       User GetUserBySession(string session);
        long Timeout { get; }


    }
}
