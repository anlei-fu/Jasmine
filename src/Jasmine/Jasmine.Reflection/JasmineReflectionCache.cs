using System;

namespace Jasmine.Reflection
{
    public class JasmineReflectionCache : AbstractReflectionCache<TypeMetaData, Type>, ITypeCache
    {
        private JasmineReflectionCache():base()
        {

        }
        public static readonly ITypeCache Instance = new JasmineReflectionCache();

        public override TypeMetaData GetItem(Type info)
        {
            if (!Contains(info))
                Cache(info);

            return base.GetItem(info);
        }

        public override void Cache(Type type)
        {
            if(_keyMap.TryAdd(type,new TypeMetaData()))
            {

                var attrs = type.GetCustomAttributes(false);

                _nameMap.TryAdd(type.Name, _keyMap[type]);

                _keyMap[type].DeclareType = type;

                foreach (var item in attrs)//attributes
                    _keyMap[type].Attributes.Cache((Attribute)item);

                foreach (var item in type.GetConstructors())//constructor
                    _keyMap[type].Constructors.Cache(item);

                foreach (var item in type.GetFields())//fileds
                        _keyMap[type].Fileds.Cache(item);

                foreach (var item in type.GetProperties())//properties
                        _keyMap[type].Properties.Cache(item);

                foreach (var item in type.GetMethods())//methods
                        _keyMap[type].Methods.Cache(item);

            }

        }
    }
}
