using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public   class DefaultConstructor:Attribute
    {
        public DefaultConstructor(params Type[] types)
        {
            ParameterTypes = types;
        }

        public Type[] ParameterTypes { get; set; }
    }
}
