using System;

namespace Jasmine.Common.Attributes
{
    public   class QueryStringAttribute:Attribute
    {
        public QueryStringAttribute(string name)
        {
            Name = name;
        }
        public string Name { get;}
    }
}
