using System;

namespace Jasmine.Restful.Attributes
{
    public   class QueryStringAttribute:Attribute
    {
        public QueryStringAttribute()
        {

        }
        public QueryStringAttribute(string name)
        {
            Name = name;
        }
        public string Name { get;}
    }
}
