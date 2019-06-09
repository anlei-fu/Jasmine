using Jasmine.Common;
using Jasmine.Orm.Attributes;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public  class ColumnMetaData:IValueGetterSetter
    {
        public bool Nullable { get; set; } = true;
        public Type OwnerType { get; set; }
        public Type RelatedType { get; set; }
        public string ColumnName { get; set; }
        public HashSet<SqlConstraintAttribute> Constraints { get; set; } = new HashSet<SqlConstraintAttribute>();
        public string PorpertyName { get; set; }
        public string SqlType { get; set; }
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }

        /// <summary>
        /// just use internal <see cref="DefaultTemplateCache.GetCreate"/>
        /// </summary>
        /// <returns></returns>
        internal ColumnMetaData Clone()
        {
            return new ColumnMetaData()
            {
                Constraints=Constraints,
                SqlType=SqlType,
                ColumnName=ColumnName
            };
        }
    }
}
