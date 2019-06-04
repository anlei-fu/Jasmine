using Jasmine.Common;
using Jasmine.Orm.Attributes;
using Jasmine.Orm.Interfaces;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public  class ColumnMetaData:IValueGetterSetter
    {
        public ISqlBaseTypeConvertor Convertor { get; set; }
        public bool Nullable { get; set; } = true;
        public Type OwnnerType { get; set; }
        public Type RelatedType { get; set; }
        public string ColumnName { get; set; }
        public HashSet<SqlConstraintAttribute> Constraints { get; set; } = new HashSet<SqlConstraintAttribute>();
        public string PorpertyName { get; set; }
        public string SqlType { get; set; }
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }
    }
}
