using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class DefaultFieldCache : AbstractReflectionCache<Field, FieldInfo>, IFieldCache
    {
        public override void Cache(FieldInfo info)
        {
            if (_keyMap.TryAdd(info, new Field()))
            {
                _nameMap.TryAdd(info.Name, _keyMap[info]);
                _keyMap[info].RelatedInfo = info;
                _keyMap[info].Getter = DynamicMethodDelegateFatory.CreateFieldGetter(info);
                _keyMap[info].Setter = DynamicMethodDelegateFatory.CreateFiledSetter(info);
                _keyMap[info].PropertyType = info.FieldType;
                _keyMap[info].OwnerType = info.DeclaringType;
                _keyMap[info].Name = info.Name;

                foreach (var item in info.GetCustomAttributes())
                    _keyMap[info].Attributes.Cache(item);
            }
        }
    }
}
