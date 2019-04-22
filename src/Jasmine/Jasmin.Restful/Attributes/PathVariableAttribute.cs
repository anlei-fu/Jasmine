using System;

namespace Jasmine.Restful.Attributes
{
    public  class PathVariableAttribute:Attribute
    {
       public PathVariableAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
