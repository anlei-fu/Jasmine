using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public  class RpcCallManager:IEnumerable<RpcCall>
    {
        private ConcurrentDictionary<long, RpcCall> _map = new ConcurrentDictionary<long, RpcCall>();
        public void AddCall(RpcCall call)
        {
            _map.TryAdd(call.Id, call);
        }

        public void Remove(long id)
        {
            _map.TryRemove(id, out var _);
        }

        public RpcCall GetCall(long id)
        {
            return _map.TryGetValue(id, out var value) ? value : null;
        }

        public IEnumerator<RpcCall> GetEnumerator()
        {
            foreach (var item in _map.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }

    public class RpcCall
    {
        public long Id { get; set; }
        public object Locker { get; set; }
        public RpcResponse Response { get; set; }
    }


}
