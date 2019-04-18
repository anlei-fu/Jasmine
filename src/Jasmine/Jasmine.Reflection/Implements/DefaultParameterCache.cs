using Jasmine.Reflection.Interfaces;
using Jasmine.Reflection.Models;
using System;
using System.Reflection;

namespace Jasmine.Reflection.Implements
{
    public class DefaultParameterCache : AbstractReflectionCache<Parameter, ParameterInfo>, IParameterCache
    {
        public override void Cache(ParameterInfo info)
        {
            if(_keyMap.TryAdd(info,new Parameter()))
            {
                var paraname = info.Name ?? "[]";//array's constructor  name is null
                
                _nameMap.TryAdd(paraname, _keyMap[info]);
                _keyMap[info].Name = paraname;
                _keyMap[info].ParameterType = info.ParameterType;
                _keyMap[info].Index = info.Position;

                foreach (var item in info.GetCustomAttributes(true))
                    _keyMap[info].Attributes.Cache((Attribute)item);


            }
        }
    }
}
