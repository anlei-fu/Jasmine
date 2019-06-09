using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// default constraints
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class DefaultAttribute:SqlConstraintAttribute
    {
        public DefaultAttribute(string value)
        {
            DefaultValue = value;
        }
        public string DefaultValue { get; set; }
    }
}
