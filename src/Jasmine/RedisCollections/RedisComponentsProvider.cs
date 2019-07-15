namespace StackExchange.Redis.Wrapper
{
    /// <summary>
    /// wrap <see cref="IDatabase"/> to  some common usage datastructure
    /// create data strcutures and provide <see cref="IDatabase"/> to every component
    /// all components's '_db' field reference to <see cref="DataBase"/>
    /// so can change <see cref="DataBase"/>property to change every component created by it
    /// 
    /// </summary>
    public class RedisComponetsProvider 
    {
        public RedisComponetsProvider(IDatabase db, string name)
        {
        }

        public IDatabase DataBase { get; }
        public RedisDictionary<TValue> GetDictionary<TValue>(string name)
        {
            return new RedisDictionary<TValue>(this,name);
        }

        public RedisLock GetLock(string name)
        {
            return new RedisLock(this, name);
        }
      
    }
}
