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
                _keyMap[info].FieldInfo = info;
                _keyMap[info].Getter = DynamicMethodDelegateFatory.CreateFieldGetter(info);
                _keyMap[info].Setter = DynamicMethodDelegateFatory.CreateFiledSetter(info);

                foreach (var item in info.GetCustomAttributes())
                    _keyMap[info].Attributes.Cache(item);
            }
        }
    }
}
