using Jasmine.Reflection.Interfaces;
using Jasmine.Reflection.Models;
using System;
using System.Reflection;

namespace Jasmine.Reflection.Implements
{
    public class DefaultPropertyCache : AbstractReflectionCache<Property, PropertyInfo>, IPropertyCache
    {
        public override void Cache(PropertyInfo info)
        {
            if(_keyMap.TryAdd(info,new Property()))
            {
                _nameMap.TryAdd(info.Name, _keyMap[info]);
                _keyMap[info].RelatedInfo = info;

                if (info.CanRead)
                    _keyMap[info].Getter = DynamicMethodDelegateFatory.CreatePropertyGetter(info);

                if (info.CanWrite)
                    _keyMap[info].Setter = DynamicMethodDelegateFatory.CreatePropertySetter(info);

                _keyMap[info].PropertyType = info.PropertyType;
                _keyMap[info].OwnerType = info.DeclaringType;
                _keyMap[info].Name = info.Name;

                foreach (var item in info.GetCustomAttributes())
                    _keyMap[info].Attributes.Cache((Attribute)item);
            }
        }
    }
}
