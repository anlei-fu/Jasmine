using System;
using System.Collections.Concurrent;
using Jasmine.Scheduling;

namespace Jasmine.Rpc.Server
{
    public class ChannelConnectivityChecker
    {
        private const int DEFAULT_HEARTBEAT_TIMEOUT = 60 * 1000;
        private ConcurrentDictionary<string, ConnectivityCheckJob> _map = new ConcurrentDictionary<string, ConnectivityCheckJob>();
        private JasmineTimeoutScheduler _scheduler = new JasmineTimeoutScheduler(new TimeoutJobManager(10000));
        public void Register(string id)
        {
            if(_map.ContainsKey(id))
            {
                if (_scheduler.Cancel(_map[id].Id))
                {
                    _map[id] = new ConnectivityCheckJob(this,id,DateTime.Now.AddMilliseconds(DEFAULT_HEARTBEAT_TIMEOUT));

                    _scheduler.Schedule(_map[id]);
                }
            }
            else
            {
                var job= new ConnectivityCheckJob(this, id, DateTime.Now.AddMilliseconds(DEFAULT_HEARTBEAT_TIMEOUT));

                _map.TryAdd(id, job);

                _scheduler.Schedule(_map[id]);
            }
        }
        public void UpdateTimeout(string id)
        {
            if (_map.ContainsKey(id))
            {
                if (_scheduler.Cancel(_map[id].Id))
                {
                    _map[id] = new ConnectivityCheckJob(this,id,DateTime.Now.AddMilliseconds(DEFAULT_HEARTBEAT_TIMEOUT));

                    _scheduler.Schedule(_map[id]);
                }
            }
        }
        public void Unregister(string id)
        {
            if (_map.ContainsKey(id))
            {
                _scheduler.Cancel(_map[id].Id);
            }

            _map.TryRemove(id, out var _);

        }
        public bool IsRegistered(string id)
        {
            return _map.ContainsKey(id);
        }

        public void Clear()
        {
            _map.Clear();
            _scheduler.Clear();
        }

        private class ConnectivityCheckJob : TimeoutJob
        {
            public ConnectivityCheckJob(ChannelConnectivityChecker checker,string connectionId, DateTime time) : base(time)
            {
                _checker = checker;
                _connectionId = connectionId;
            }
            private string _connectionId;
            private ChannelConnectivityChecker _checker;
            public override void Excute()
            {
                _checker.Unregister(_connectionId);
            }
        }
    }


}
