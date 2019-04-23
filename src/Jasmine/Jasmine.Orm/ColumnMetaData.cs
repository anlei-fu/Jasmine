using Jasmine.Common;
using Jasmine.Orm.Attributes;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public  class ColumnMetaData:IValueGetterSetter
    {
        public bool Nullable { get; set; } = true;
        public Type RelatedType { get; set; }
        public string ColumnName { get; set; }
        public IDictionary<Type, SqlConstraintAttribute> Constraints { get; set; } = new Dictionary<Type,SqlConstraintAttribute>();
        public string ModelName { get; set; }
        public string SqlType { get; set; }
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }
    }
}
