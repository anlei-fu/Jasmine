using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use to convert object property name to sql column name
    /// </summary>
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false)]
    public  class ColumnNameAttribute:Attribute
    {

        public ColumnNameAttribute(string columnName)
        {
<<<<<<< HEAD
            ColumnName = columnName;
=======
            ColumnName = sqlName;
>>>>>>> abd18fb1fcdd791e769188a65fdc4c0ae78ae8d4
        }
        /// <summary>
        /// 数据库字段名
        /// </summary>
<<<<<<< HEAD
        public string ColumnName { get; set; }
=======
        public string ColumnName { get;}
>>>>>>> abd18fb1fcdd791e769188a65fdc4c0ae78ae8d4
    }
}
