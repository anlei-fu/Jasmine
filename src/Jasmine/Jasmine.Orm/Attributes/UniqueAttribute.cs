using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// unique constraint
    /// </summary>
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public   class UniqueAttribute:Attribute
    {
    }
}
