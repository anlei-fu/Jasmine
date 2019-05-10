using System.Collections.Generic;

namespace Jasmine.Interpreter.TypeSystem
{
    public  class Collections
    {
        public class Map
        {
            private Dictionary<string, JObject> _innerDic = new Dictionary<string, JObject>();

            public int Count => _innerDic.Count;
            public bool Contains(string key)
            {
                return _innerDic.ContainsKey(key);
            }
            public void Set(string key,JObject obj)
            {
                if (!_innerDic.ContainsKey(key))
                    _innerDic.Add(key, obj);
                else
                    _innerDic[key] = obj;
            }
            public void Remove(string key)
            {
                if (_innerDic.ContainsKey(key))
                    _innerDic.Remove(key);
            }

            public JObject this[string parameter]
            {
                get => _innerDic[parameter];

                set
                {
                    _innerDic[parameter] = value;
                }
            }
        }

        public class Array
        {
            private List<JObject> _innerArray = new List<JObject>();

            public JObject this[int index]
            {
                get => _innerArray[index];

                set
                {
                    _innerArray[index] = value;
                }
            }

            public void Add(JObject obj)
            {
                _innerArray.Add(obj);
            }
            public void AddRange(IEnumerable<JObject> collection)
            {
                _innerArray.AddRange(collection);
            }
            public void RemoveAt(int index)
            {
                _innerArray.RemoveAt(index);
            }
            public void RemoveRange(int start,int length)
            {
                _innerArray.RemoveRange(start, length);
            }

            public void Clear()
            {
                _innerArray.Clear();
            }
            public int Count => _innerArray.Count;

            public void InsertAt(int index,JObject obj)
            {
                _innerArray.Insert(index, obj);
            }

            public void InsertRange(int index,IEnumerable<JObject> collection)
            {
                _innerArray.InsertRange(index, collection);
            }

        }



    }
}
