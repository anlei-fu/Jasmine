using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// 用于绑定对象属性名与数据库字段名转换
    /// </summary>
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public  class SqlNameAttribute:Attribute
    {

        public SqlNameAttribute(string sqlName)
        {
            SqlName = sqlName;
        }
        /// <summary>
        /// 数据库字段名
        /// </summary>
        public string SqlName { get; set; }
    }
}
