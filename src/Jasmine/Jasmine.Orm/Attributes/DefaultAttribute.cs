using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// default constraints
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class DefaultAttribute:Attribute
    {
        public DefaultAttribute(object value)
        {
            DefaultValue = value;
        }
        public object DefaultValue { get; set; }
    }
}
