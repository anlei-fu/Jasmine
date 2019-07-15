using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{

    /// <summary>
    /// <see cref="Name"/> the dictionary name
    /// 
    ///  Redis->Name->Key to match value
    /// 
    /// value  serialized with json style
    /// 
    /// redis raw value all is string ,guess stack exchange lib  also base on string
    /// <see cref="RedisKey"/> and <see cref="RedisValue"/> finally convert to string or from string
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class RedisDictionary<TValue> :RedisComponent, IDictionary<string, TValue>
    {
        internal RedisDictionary(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }


        /// <summary>
        /// accessor to redis
        /// </summary>
        public TValue  this [string key]
        {
            get 
            {
               return  (TValue)(object)(string)_db.HashGet(Name, key);
            }
            set
            {
                _db.HashSet(Name,key, (string)(object)value);
            }
        }


        public ICollection<string> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count =>(int)_db.HashLength(Name);

        public bool IsReadOnly => true;

        /// <summary>
        /// 添加之前需要先判断,redis的处理是覆盖而不是报错
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, TValue value)
        {
            _db.HashSet(Name, key, (string)(object)value);
        }

        public async Task AddAsync(string key, TValue value)
        {
            var result= await _db.HashSetAsync(Name, key, (string)(object)value)
                                 .ConfigureAwait(false);

            //should be strict with c# grammer,or ignore?
            if (!result)
                throw new Exception();
        }
        public void Add(KeyValuePair<string, TValue> item)
        {
            _db.HashSet(Name, item.Key, (string)(object)item.Value);
        }

        public bool TryAdd(string key,TValue value)
        {
            return true;
        }

        public void Clear()
        {
            _db.HashDelete(Name, (RedisValue[])(object)Keys.ToArray());
        }

        public bool Contains(KeyValuePair<string, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public bool ContainsKey(string key)
        {
            return _db.HashExists(Name, key);
        }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string,TValue> Copy()
        {
            return null;
        }
    }
}
