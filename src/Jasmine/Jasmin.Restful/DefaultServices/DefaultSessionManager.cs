using Jasmine.Cache;
using System;

namespace Jasmine.Restful.DefaultFilters
{
    public class DefaultSessionManager : ISessionManager
    {
        public DefaultSessionManager(long timeout=7*24*60*60*1000/* 7day */)
        {
            Timeout = timeout;
        }
        protected ITimeoutCache<string, Level> _sessions = new JasmineTimeoutCache<string, Level>(1000,null);

        public long Timeout { get; }

        public string CreateSession(Level level)
        {
            var session = Guid.NewGuid().ToString();

            _sessions.Cache(session, level, Timeout);

            return session;
        }

        public Level? SessionExists(string session)
        {
            return _sessions.Conatins(session)?(Level?)_sessions.GetValue(session):null;
        }
    }
}
