using System;

namespace Jasmine.Restful.Attributes
{
    public  class PathAttribute:Attribute
    {
        public PathAttribute(string path)
        {
            Path = path;
        }
        public string Path { get; }
    }
}
