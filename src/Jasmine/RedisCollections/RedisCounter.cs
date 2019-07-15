using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{
    public class RedisCounter:RedisComponent
    {
        public RedisCounter(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public long Increment(long step=1)
        {
            return 1;
        }
        public Task<long> IncrementAsync(long step = 1)
        {
            return null;
        }
        public long Decrement(long step=1)
        {
            return 0;
        }
        public Task<long> DecrementAsync(long step = 1)
        {
            return null;
        }

        public long GetCurrent()
        {
            return -1;
        }
        public Task<long> GetCurrentAsync()
        {
            return null;
        }
    }
}
