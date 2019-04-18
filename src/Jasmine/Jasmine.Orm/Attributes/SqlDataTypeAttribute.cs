using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public  class SqlDataTypeAttribute:Attribute
    {
        public SqlDataTypeAttribute(string type)
        {
            SqlType = type;
        }
        public string SqlType { get; set; }
    }
}
