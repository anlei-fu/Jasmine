using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to imply a sql data type {char,nchar,varchar,boolean....}
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public  class SqlColumnTypeAttribute:OrmAttribute
    {
        public SqlColumnTypeAttribute(string type)
        {
            SqlColumnType = type;
        }
        public string SqlColumnType { get; set; }
    }
}
