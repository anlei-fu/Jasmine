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
        protected ITimeoutCache<string, AuntenticateLevel> _sessions = new JasmineTimeoutCache<string, AuntenticateLevel>(10000,null);

        public long Timeout { get; }

        public string CreateSession(AuntenticateLevel level)
        {
            var session = Guid.NewGuid().ToString();

            _sessions.Cache(session, level, Timeout);

            return session;
        }

        public AuntenticateLevel? GetSession(string session)
        {
            return _sessions.ConatinsKey(session)?(AuntenticateLevel?)_sessions.GetValue(session):null;
        }
    }
}
