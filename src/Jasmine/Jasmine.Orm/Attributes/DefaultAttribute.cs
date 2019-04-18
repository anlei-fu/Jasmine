using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 默认约束
    /// </summary>
    public class DefaultAttribute:Attribute
    {
        public DefaultAttribute(object value)
        {
            DefaultValue = value;
        }
        public object DefaultValue { get; set; }
    }
}
