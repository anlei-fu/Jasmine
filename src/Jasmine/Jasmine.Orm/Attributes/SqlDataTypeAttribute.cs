using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to imply a sql data type {char,nchar,varchar,boolean....}
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public  class SqlDataTypeAttribute:OrmAttribute
    {
        public SqlDataTypeAttribute(string type)
        {
            SqlType = type;
        }
        public string SqlType { get; set; }
    }
}
