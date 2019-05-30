using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public    class PrimaryKeyAttribute:SqlConstraintAttribute
    {
    }
}
