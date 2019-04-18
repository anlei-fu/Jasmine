using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to imply sql table
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class SqlTableNameAttribute:OrmAttribute
    {
    }
}
