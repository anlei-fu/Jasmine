using System;
using System.Collections.Concurrent;
using Jasmine.Scheduling;

namespace Jasmine.Rpc.Server
{
    public class ChannelConnectiveChecker
    {
        private const int DEFAULT_TIMEOUT = 10 * 1000;
        private ConcurrentDictionary<string, ConnectiveCheckJob> _map = new ConcurrentDictionary<string, ConnectiveCheckJob>();
        private JasmineTimeoutScheduler _sheduler = new JasmineTimeoutScheduler(new TimeoutJobManager(10000));
        public void Register(string id)
        {
            if(_map.ContainsKey(id))
            {
                if (_sheduler.Cancel(_map[id].Id))
                {
                    _map[id] = new ConnectiveCheckJob(this,id,DateTime.Now.AddMilliseconds(DEFAULT_TIMEOUT));

                    _sheduler.Schedule(_map[id]);
                }
            }
            else
            {
                var job= new ConnectiveCheckJob(this, id, DateTime.Now.AddMilliseconds(DEFAULT_TIMEOUT));
                _map.TryAdd(id, job);
                _sheduler.Schedule(_map[id]);
            }
        }
        public void UpdateTimeout(string id)
        {
            if (_map.ContainsKey(id))
            {
                if (_sheduler.Cancel(_map[id].Id))
                {
                    _map[id] = new ConnectiveCheckJob(this,id,DateTime.Now.AddMilliseconds(DEFAULT_TIMEOUT));

                    _sheduler.Schedule(_map[id]);
                }
            }
        }
        public void Unregister(string id)
        {
            if (_map.ContainsKey(id))
            {
                _sheduler.Cancel(_map[id].Id);
            }

            _map.TryRemove(id, out var _);

        }
        public bool IsRegistered(string id)
        {
            return _map.ContainsKey(id);
        }

        private class ConnectiveCheckJob : TimeoutJob
        {
            public ConnectiveCheckJob(ChannelConnectiveChecker checker,string connectionId, DateTime time) : base(time)
            {
                _checker = checker;
                _connectionId = connectionId;
            }
            private string _connectionId;
            private ChannelConnectiveChecker _checker;
            public override void Excute()
            {
                _checker.Unregister(_connectionId);
            }
        }
    }


}
