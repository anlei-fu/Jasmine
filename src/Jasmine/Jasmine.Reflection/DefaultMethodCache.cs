using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class DefaultMethodCache :  IMethodCache
    {
        public int Count => throw new NotImplementedException();

        private ConcurrentDictionary<MethodInfo, Method> _keyMap = new ConcurrentDictionary<MethodInfo, Method>();
        private ConcurrentDictionary<string, IList<Method>> _nameMap = new ConcurrentDictionary<string, IList<Method>>();


        public  void Cache(MethodInfo info)
        {
            if (_keyMap.TryAdd(info, new Method()))
            {
                _keyMap[info].Name = info.Name;
                _keyMap[info].RelatedInfo = info;
                _keyMap[info].ReturnType = info.ReturnType;
                _keyMap[info].FullName = info.GetMethodName();

                _nameMap.TryAdd(_keyMap[info].Name, new List<Method>());
                _nameMap[info.Name].Add(_keyMap[info]);
                
                _keyMap[info].ParamerTypies = info.GetParameterTypes();
                _keyMap[info].Invoker = DynamicMethodDelegateFatory.CreateMethodCall(info);

                foreach (var item in info.GetParameters())
                    _keyMap[info].Parameters.Cache(item);

                foreach (var item in info.GetCustomAttributes(true))
                    _keyMap[info].Attributes.Cache((Attribute)item);

                _keyMap[info].Name = info.Name;

            }
        }

        public bool Contains(MethodInfo info)
        {
            return _keyMap.ContainsKey(info);
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        public Method FindMethod(string name, Type[] parameterTypes)
        {
            Method ret = null;

            if (_nameMap.TryGetValue(name, out var value))
            {

                var methods = new List<Method>();

                foreach (var item in value)
                {
                    if (item.ParamerTypies.Length != parameterTypes.Length)
                        continue;

                    if(TypeUtils.CompareTypes(item.ParamerTypies,parameterTypes))
                    {
                        methods.Add(item);
                    }
                }

                if(methods.Count!=0)
                {
                    var max = 0;

                    ret = methods[0];

                    foreach (var method in methods)
                    {

                        var t = 0;

                        for (int i = 0; i < method.ParamerTypies.Length; i++)
                        {
                            if (parameterTypes[i] == null)
                                continue;
                            else if (parameterTypes[i] == method.ParamerTypies[i])
                                t += 2;
                            else
                                t += 1;
                        }

                        if(t>max)
                        {
                            ret = method;
                            max = t;
                        }
                    }
                }

            }

            return ret;
        }

        public IEnumerable<Method> GetAll()
        {
            return _keyMap.Values;
        }

        public string[] GetAllNames()
        {
            return _nameMap.Keys.ToArray();
        }

        public IEnumerator<Method> GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item;
            }
        }

        public Method GetItem(MethodInfo info)
        {
            return _keyMap.TryGetValue(info, out var result) ? result : null;
        }
        [Obsolete]
        public Method GetItemByName(string name)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyMap.Values.GetEnumerator();
        }

        public Method[] GetMethodsByName(string name)
        {
            return _nameMap.TryGetValue(name, out var result) ? result.ToArray() : null;
        }
    }
}
