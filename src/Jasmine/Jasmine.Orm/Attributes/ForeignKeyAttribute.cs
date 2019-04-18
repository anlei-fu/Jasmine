using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 外键约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public   class ForeignKeyAttribute:Attribute
    {
    }
}
