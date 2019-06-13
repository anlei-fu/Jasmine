using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful
{
    public interface ISessionManager
    {
        string CreateSession(Level level);
        Level? SessionExists(string session);
        long Timeout { get; }


    }
}
