using System;

namespace Jasmine.Restful.Attributes
{
    public  class PathVariableAttribute:Attribute
    {
       public PathVariableAttribute(string name)
        {
            VaribleName = name;
        }
        public string VaribleName { get; }
    }
}
