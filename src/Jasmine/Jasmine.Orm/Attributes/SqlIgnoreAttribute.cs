using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to imply if the property matchs to table column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlIgnoreAttribute:Attribute
    {
    }
}
