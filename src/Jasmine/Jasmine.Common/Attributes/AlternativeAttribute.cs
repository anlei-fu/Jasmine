using System;

namespace Jasmine.Common.Attributes
{
    public class AlternativeAttribute:Attribute
    {
        public AlternativeAttribute(string path)
        {
            Path = path;
        }
        public string Path { get; }
    }
}
