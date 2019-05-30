using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to convert object property name to sql column name
    /// </summary>
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false)]
    public  class ColumnNameAttribute:Attribute
    {

        public ColumnNameAttribute(string sqlName)
        {
            SqlName = sqlName;
        }
        /// <summary>
        /// 数据库字段名
        /// </summary>
        public string SqlName { get; set; }
    }
}
