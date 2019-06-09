using Jasmine.Common;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class TableMetaData:ITypeFearture
    {
        public bool HasJoinTable => JoinTables.Count != 0;
        public bool HasJoinColumns => JoinColumns.Count != 0;
        public bool HasAssociateTable => AssociateTables.Count != 0;
        public DataSourceType DataSource { get; set; }
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public IDictionary<string, ColumnMetaData> Columns { get; set; } = new Dictionary<string, ColumnMetaData>();
        public Func<object> Constructor { get; set; }
        public int Count =>Columns.Count;
        public IQueryResultResolver Resolver { get; set; }
        public IDictionary<string, JoinTable> JoinTables { get; set; } = new Dictionary<string, JoinTable>();
        public IDictionary<string, JoinColumns> JoinColumns { get; set; } = new Dictionary<string, JoinColumns>();
        public IDictionary<string, AssociateTable> AssociateTables { get; set; } = new Dictionary<string, AssociateTable>();

    
    
    }

    public class RelatedTable:IValueGetterSetter
    {
        public TableMetaData Table { get; set; }
        public string PropertyName { get; set; }

        public Func<object, object> Getter { get; set; }

        public Action<object, object> Setter { get; set; }
        public Type RelatedType { get; set; }
        public Type OwnerType { get; set; }

    }
    public class AssociateTable:RelatedTable
    {
       public SqlTemplate ConditionTemplate { get; set; }
      
    }

    public class JoinColumns:RelatedTable
    {
       
    }
    public class JoinTable:RelatedTable
    {
       public string JoinKey { get; set; }
    }
}
