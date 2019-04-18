using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class DefaultConstructorCache : AbstractReflectionCache<Constructor, ConstructorInfo>, IConstructorCache
    {
        private Constructor _defaultContructor;
        public override void Cache(ConstructorInfo info)
        {
            if(_keyMap.TryAdd(info,new Constructor()))
            {
               
                _keyMap[info].RelatedInfo = info;
                _keyMap[info].Name = info.Name;
                _keyMap[info].FullName = info.GetMethodName();
                _nameMap.TryAdd(_keyMap[info].FullName, _keyMap[info]);
                _keyMap[info].ParamerTypies = info.GetParameterTypes();

                if (_keyMap[info].ParamerTypies.Length == 0)
                {
                  _keyMap[info].DefaultInvoker= DynamicMethodDelegateFatory.CreateDefaultConstructor(info.DeclaringType);
                    _defaultContructor = _keyMap[info];
                }
                else
                {
                   _keyMap[info].Invoker = DynamicMethodDelegateFatory.CreateParameterizedConstructor(info);
                }

                var i = 0;

                _keyMap[info].ParameterNames = new string[info.GetParameters().Length];

                foreach (var item in info.GetParameters())
                {
                    _keyMap[info].Parameters.Cache(item);
                    _keyMap[info].ParameterNames[i] = item.Name;
                    i++;
                }

                foreach (var item in info.GetCustomAttributes(true))
                    _keyMap[info].Attributes.Cache((Attribute)item);

            }
        }

        public Constructor FindConstructor(Type[] parameterTypes)
        {
            throw new NotImplementedException();
        }

        public Constructor GetDefaultConstructor()
        {
            return _defaultContructor;
        }

        

      
    }
}
