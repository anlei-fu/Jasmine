using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// represent sql constraints
    /// unique
    /// not null
    /// primary key
    /// foreign key
    /// check
    /// default
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlConstraintAttribute : Attribute
    {
       
    }
}
