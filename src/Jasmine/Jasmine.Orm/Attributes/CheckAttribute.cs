using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// check constraints
    /// </summary>
    [AttributeUsage(AttributeTargets.Property , AllowMultiple = false)]
    public  class CheckAttribute:Attribute
    {
        public CheckAttribute(string expression)
        {
            Expression = expression;
        }
        public string Expression { get;}
    }
}
