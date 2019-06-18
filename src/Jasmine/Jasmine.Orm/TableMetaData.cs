using Jasmine.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Orm
{
    public class TableMetaData:ITypeFearture
    {
        public bool HasJoinTable => JoinTables.Count != 0;
        public bool HasJoinColumns => JoinColumns.Count != 0;
        public bool HasAssociateTable => AssociateTables.Count != 0;
        public DataSource DataSource { get; set; }
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public IDictionary<string, ColumnMetaData> Columns { get; set; } = new Dictionary<string, ColumnMetaData>();
        public Func<object> Constructor { get; set; }
        public int Count =>Columns.Count;
        public IQueryResultResolver Resolver { get; set; }
        public IDictionary<string, JoinTable> JoinTables { get; set; } = new Dictionary<string, JoinTable>();
        public IDictionary<string, JoinColumns> JoinColumns { get; set; } = new Dictionary<string, JoinColumns>();
        public IDictionary<string, AssociateTable> AssociateTables { get; set; } = new Dictionary<string, AssociateTable>();
    
        public string[] GetAllColumnName(string prefix="")
        {
            var ls = new List<string>();

            prefix = prefix == string.Empty ? prefix : prefix + "_";

            foreach (var item in Columns.Keys)
            {
                ls.Add(prefix + item);
            }

            if(HasJoinColumns)
            {
                foreach (var item in JoinColumns.Values)
                {
                    ls.AddRange(item.Table.GetAllColumnName(prefix + item.PropertyName));
                }
            }

            return ls.ToArray();
        }
    }

    public abstract class RelatedTable:IValueGetterSetter,ITypeFearture
    {
        public TableMetaData Table { get; set; }
        public string PropertyName { get; set; }
        public Func<object, object> Getter { get; set; }
        public Action<object, object> Setter { get; set; }
        public Type RelatedType { get; set; }
        public Type OwnerType { get; set; }

    }
    /// <summary>
    /// associate query  ,require property is an array or a list and element type is class
    /// </summary>
    public class AssociateTable:RelatedTable
    {
        /// <summary>
        /// associate condition ,auto parse
        /// e.g
        /// 
        /// class Department
        /// {
        ///    public string Name{get;set;}
        ///    public Stuff[] Stuffs{get;set;}
        /// }
        /// class Stuff
        /// {
        ///  public string Name{get;set;}
        ///  public string Department{get;set;}
        ///  public int Age{get;set;}
        /// }
        /// 
        /// condition should be " @Name=DepartmentName "
        /// 
        /// </summary>
        public SqlTemplate ConditionTemplate { get; set; }
    }
    /// <summary>
    /// a class-type property, need save into the same one table
    /// </summary>
    public class JoinColumns:RelatedTable
    {
       
    }
    /// <summary>
    /// Left join table 
    /// </summary>
    public class JoinTable:RelatedTable
    {
        public string OutterKey { get; set; }
       public string InnerKey { get; set; }
       public string ParentKey { get; set; }
    }
}
