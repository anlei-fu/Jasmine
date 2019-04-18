using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 检查约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public  class CheckAttribute:Attribute
    {
        public CheckAttribute(string expression)
        {
            Expression = expression;
        }
        public string Expression { get; set; }
    }
}
