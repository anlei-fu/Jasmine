using Jasmine.Spider.Common;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Spider.Controller
{
    public interface ISpiderTaskManager:IDictionary<long,ISpiderTask>
    {
        ISpiderTask   GetTask(long taskiId);
        bool RemoveTask(long taskId);
        bool ContainsTask(long taskId);
        int GetTaskCount();
    }

    public class SpiderTaskManager : ISpiderTaskManager
    {
        public ISpiderTask this[long key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public ICollection<long> Keys => throw new System.NotImplementedException();

        public ICollection<ISpiderTask> Values => throw new System.NotImplementedException();

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => throw new System.NotImplementedException();

        public void Add(long key, ISpiderTask value)
        {
            throw new System.NotImplementedException();
        }

        public void Add(KeyValuePair<long, ISpiderTask> item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<long, ISpiderTask> item)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsKey(long key)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<long, ISpiderTask>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<long, ISpiderTask>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(long key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<long, ISpiderTask> item)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(long key, out ISpiderTask value)
      

       
    }
}
