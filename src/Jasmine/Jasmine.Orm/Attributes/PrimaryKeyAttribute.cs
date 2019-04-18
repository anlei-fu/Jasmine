using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 主键约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public    class PrimaryKeyAttribute:Attribute
    {
    }
}
