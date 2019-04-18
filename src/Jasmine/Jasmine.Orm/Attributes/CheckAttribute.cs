using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// check constraints
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public  class CheckAttribute:SqlConstraintAttribute
    {
        public CheckAttribute(string expression)
        {
            Expression = expression;
        }
        public string Expression { get; set; }
    }
}
