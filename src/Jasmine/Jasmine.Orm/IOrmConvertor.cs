using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Interfaces
{
    public interface IOrmConvertor
    {
        /// <summary>
        /// conver objct to insert-into command's real parameter part
        /// ex:
        ///public class animal
        /// {
        ///   public string Name{get;set;}
        ///   publi int Age{get;set;}
        /// }
        /// var cat= new Animal
        /// {
        ///   Name="dog",
        ///   Age=10
        /// };
        /// 
        /// it will be   ('dog',10)
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        string ToSql(object item);
        object FromResult(SqlResultContext context,Type type);
    }
}
