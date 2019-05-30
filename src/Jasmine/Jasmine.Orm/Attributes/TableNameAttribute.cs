using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute:Attribute
    {
        public string Name { get; }
    }
}
