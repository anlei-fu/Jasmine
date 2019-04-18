using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// primary key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public    class PrimaryKeyAttribute:SqlConstraintAttribute
    {
    }
}
