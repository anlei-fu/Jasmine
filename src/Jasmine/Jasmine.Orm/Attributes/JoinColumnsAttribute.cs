using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use join quey or insert 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class JoinColumnsAttribute:Attribute
    {
        public JoinColumnsAttribute(string foreignKey)
        {
            ForeignKey = foreignKey;
        }

        public string ForeignKey { get; }
    }
}
