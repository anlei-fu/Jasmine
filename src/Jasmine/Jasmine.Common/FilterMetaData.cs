using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class FilterMetaData : INameFearture,ITypeFearture
    {
        public string Name => RelatedType.Name;
        public Type RelatedType { get; set; }
        public bool HasBeforeFilters => BeforeFilters.Count!= 0;
        public bool HasAfterFilters => AfterFilters.Count != 0;
        public bool HasAroundFilters => AroundFilters.Count != 0;
        public List<Type> BeforeFilters { get; set; } = new List<Type>();
        public List<Type> AfterFilters { get; set; } = new List<Type>();
        public List<Type> AroundFilters { get; set; } = new List<Type>();

       
    }
}
