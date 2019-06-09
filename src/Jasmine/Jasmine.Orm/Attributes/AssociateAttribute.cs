using System;

namespace Jasmine.Orm.Attributes
{
    public  class AssociateAttribute:Attribute
    {
        public AssociateAttribute(string condition)
        {
            Condition = condition;
        }
        public string Condition { get; }
    }
}
