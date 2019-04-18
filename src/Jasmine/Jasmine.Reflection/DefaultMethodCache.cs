using Jasmine.Reflection.Models;
using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class DefaultMethodCache : AbstractReflectionCache<Method, MethodInfo>, IMethodCache
    {
        public override void Cache(MethodInfo info)
        {
            if (_keyMap.TryAdd(info, new Method()))
            {
                _keyMap[info].Name = info.Name;
                _keyMap[info].RelatedInfo = info;
                _keyMap[info].ReturnType = info.ReturnType;
                _keyMap[info].FullName = info.GetMethodName();
                _nameMap.TryAdd(_keyMap[info].FullName, _keyMap[info]);
                _keyMap[info].ParamerTypies = info.GetParameterTypes();
                _keyMap[info].Invoker = DynamicMethodDelegateFatory.CreateMethodCall(info);

                foreach (var item in info.GetParameters())
                    _keyMap[info].Parameters.Cache(item);

                foreach (var item in info.GetCustomAttributes(true))
                    _keyMap[info].Attributes.Cache((Attribute)item);

                _keyMap[info].Name = info.Name;

            }
        }

        public Method FindMethod(string name, Type parameterTypes)
        {
            throw new NotImplementedException();
        }
    }
}
