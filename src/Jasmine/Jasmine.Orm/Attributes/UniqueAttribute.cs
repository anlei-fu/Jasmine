using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 唯一约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public   class UniqueAttribute:Attribute
    {
    }
}
