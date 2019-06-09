using System;

namespace Jasmine.Orm.Interfaces
{
    public interface ISqlTypeConvertor
    {
        /// <summary>
        /// convert obj to sql string 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        string ConvertToSqlString(Type type,object obj,bool nullable=true);
        /// <summary>
        /// convert object to destination type ,will do an explicit convert or  unbox  operation
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="convertTo"></param>
        /// <returns></returns>
        object FromSqlFiledValue(object value, Type convertTo);
    }
}
