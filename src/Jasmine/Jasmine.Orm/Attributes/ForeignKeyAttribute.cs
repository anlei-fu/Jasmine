using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 外键约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public   class ForeignKeyAttribute:Attribute
    {
    }
}
