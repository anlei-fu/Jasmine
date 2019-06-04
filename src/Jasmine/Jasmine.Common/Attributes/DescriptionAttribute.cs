using System;

namespace Jasmine.Common.Attributes
{
    public class DescriptionAttribute:Attribute
    {

        public DescriptionAttribute(string des)
        {
            Description = des;
        }
        public string Description { get; }
    }
}
