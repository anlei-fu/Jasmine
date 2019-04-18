using System;
using System.Collections.Generic;

namespace Jasmine.Orm.Model
{
    public  class Column
    {
        public bool Nullable { get; set; } = true;
        public Type RelatedType { get; set; }
        public string SqlName { get; set; }
        public IDictionary<Type, Attribute> Constraints { get; set; } = new Dictionary<Type,Attribute>();
        public string ModelName { get; set; }
        public string SqlType { get; set; }
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }
    }
}
