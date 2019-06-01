using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute:Attribute
    {
        public TableNameAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
