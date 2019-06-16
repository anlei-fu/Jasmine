using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful
{
    public interface ISessionManager
    {
        string CreateSession(AuntenticateLevel level);
        AuntenticateLevel? GetSession(string session);
        long Timeout { get; }


    }
}
