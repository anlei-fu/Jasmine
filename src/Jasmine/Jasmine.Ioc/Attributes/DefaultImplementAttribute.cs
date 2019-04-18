using System;

namespace Jasmine.Ioc.Attributes
{
    public class DefaultImplementAttribute:Attribute
    {
        public Type Implement { get; }
        public DefaultImplementAttribute(Type type)
        {
            Implement = type;
        }
    }
}
