using System;

namespace Jasmine.Orm.Attributes
{
    public  class JoinTableAttribute:Attribute
    {
        public JoinTableAttribute(string inner,string outter)
        {
            Outter = outter;
            Inner = inner;
        }
        public string Inner { get; }
        public string Outter { get; }
    }
}
