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
        protected ITimeoutCache<string, User> _sessions = new JasmineTimeoutCache<string, User>(10000,null);

        public long Timeout { get; }

        public string CreateSession(User user)
        {
            var session = Guid.NewGuid().ToString();

            _sessions.Cache(session, user, Timeout);

            return session;
        }

        public User GetUserBySession(string session)
        {
            return _sessions.ConatinsKey(session)?_sessions.GetValue(session):null;
        }
    }
}
