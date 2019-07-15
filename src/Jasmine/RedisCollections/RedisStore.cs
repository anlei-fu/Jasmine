using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{
    /// <summary>
    /// simple wrapper of <see cref="IDatabase"/>,and serialization style is json
    /// </summary>
    public class RedisStore : RedisComponent
    {
        public RedisStore(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }
        /// <summary>
        /// Use a prefix to classify diffirent  store,avoid key repeat and overlay
        /// </summary>
        public string Prefix { get; }

        public  bool Store(string key,object data,TimeSpan? expiry)
        {
           return  _db.StringSet(key, (string)(object)data, expiry);
        }
        public Task<bool> StoreAsync(string key, object data, TimeSpan? expiry)
        {
            return _db.StringSetAsync(key, (string)(object)data, expiry);
        }

        public bool Store(IEnumerable<KeyValuePair<string,object>> entries,TimeSpan? expiry)
        {
            return true;
        }
        public Task<bool> StoreAsync(IEnumerable<KeyValuePair<string, object>> entries, TimeSpan? expiry)
        {
            return null;
        }
        public bool Exists(string key)
        {
            return true;
        }

        public TimeSpan? GetExpiry(string key)
        {
            return null;
        }
        public Task<TimeSpan?> GetExpiryAsync(string key)
        {
            return null;
        }

        public T Get<T>(string key)
        {
            return default(T);
        }
        public Task<T> GetAsync<T>(string key)
        {
            return null;
        }

        public IEnumerable<T> BulkGet<T>(IEnumerable<string> keys)
        {
            return null;
        }

        public bool Unstore(string key)
        {
            return _db.KeyDelete(key);
        }
        public Task<bool> UnstoreAsync(string key)
        {
            return null;
        }
        public bool Unstore(IEnumerable<string> keys)
        {
            return true;
        }
        public Task<bool> UnstoreAsync(IEnumerable<string> keys)
        {
            return null;
        }


        public bool AdjustExpiry(string key,TimeSpan? _new)
        {
            return true;
        }
    }
}
