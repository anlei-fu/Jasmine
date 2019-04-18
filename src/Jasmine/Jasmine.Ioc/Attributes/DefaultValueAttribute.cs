using System;

namespace Jasmine.Ioc.Attributes
{
    public   class DefaultValueAttribute:Attribute
    {
        public object Value { get; }
        public DefaultValueAttribute(object value)
        {
            Value = value;
        }
    }
}
