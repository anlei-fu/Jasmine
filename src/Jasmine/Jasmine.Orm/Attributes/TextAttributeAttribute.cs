using System;

namespace Jasmine.Orm.Attributes
{
    public class TextAttribute:Attribute
    {
        public TextAttribute(string type,int length)
        {
            TextType = type;
            Length = length;
        }
        public int Length { get; set; }
        public string TextType { get; set; }
    }
}
