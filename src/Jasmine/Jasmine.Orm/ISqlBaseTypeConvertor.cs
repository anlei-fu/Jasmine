using System;
using System.Data.SqlClient;

namespace Jasmine.Orm.Interfaces
{
    public interface ISqlBaseTypeConvertor
    {
        /// <summary>
        /// convert obj to sql string 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        string ToSQL(Type type,object obj,bool nullable=true);
        /// <summary>
        /// get object from reader's one row's index
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object FromSql(SqlDataReader reader, int index, Type type);
    }
}
