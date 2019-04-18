using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 非空约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public   class NotNullAttribute:Attribute
    {
    }
}
