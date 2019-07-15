using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{
    public class RedisList<TElement> : RedisComponent, IList<TElement>, INameFearture
    {
        internal RedisList(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public TElement this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => (int)_db.ListLength(Name);

        public bool IsReadOnly => false;


        public void Add(TElement item)
        {
            _db.ListLeftPush(Name, (string)(object)item);
        }

        public Task AddAsync(TElement item)
        {
            return _db.ListLeftPushAsync(Name, (string)(object)item);
        }

        public long AddRange(IEnumerable<TElement> collection)
        {
            return _db.ListLeftPush(Name, new RedisValue[] { });
        }
        public  Task<long> AddRangeAsync(IEnumerable<TElement> collection)
        {
            return  _db.ListLeftPushAsync(Name, new RedisValue[] { });
        }
        public TElement[] GetRange(int start,int count)
        {
            return null;
        }

        public Task<TElement[]> GetRangeAsync(int start,int count)
        {
            return null;
        }


        public void Clear()
        {
            _db.ListTrim(Name, 0, Count);
        }

        public Task ClearAsync()
        {
            return _db.ListTrimAsync(Name, 0, Count);
        }

        /// <summary>
        ///效率比较低,不支持
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(TElement item)
        {
            throw new NotSupportedException("this method is not surppoted!");
        }

        public void CopyTo(TElement[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(TElement[] array,int arrayIndex)
        {
            return null;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 效率比较低,不支持
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(TElement item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// this mehod  redis not surpporte? 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, TElement item)
        {
            throw new NotSupportedException();
        }


        /// <summary>
        /// 效率比较低,不支持
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(TElement item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            _db.ListTrim(Name, index, index);
        }

        public Task RemoveAtAsync(int index)
        {
            return _db.ListTrimAsync(Name, index, index);
        }


        public void RemoveRange(int index,int count)
        {

        }

        public void RemoveRangeAsync(int index, int count)
        {

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
