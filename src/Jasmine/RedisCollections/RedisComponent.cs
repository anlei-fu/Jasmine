using System;

namespace StackExchange.Redis.Wrapper
{
    public  abstract  class RedisComponent:INameFearture
    {
        internal RedisComponent(RedisComponetsProvider provider,string name)
        {
            Name = name??throw new ArgumentNullException(nameof(name));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


       
        public RedisComponetsProvider Provider { get; }
        protected IDatabase _db => Provider.DataBase;

        protected string serialize(object data)
        {
            return null;
        }

        protected T deserialize<T>(string json)
        {
            return default;
        }
        public string Name { get; }
    }
}
