using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful
{
    public interface ISessionManager
    {
        string CreateSession(User user);
       User GetSession(string session);
        long Timeout { get; }


    }
}
