using System;

namespace Jasmine.Common
{
    public class ParameterMetaDataBase : INameFearture,ITypeFearture
    {
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public bool Nullable { get; set; }
        public bool IsAbstract { get; set; }
        public Type ImplType { get; set; }
        public int Index { get; set; }
        public object DefaultValue { get; set; }
        public bool HasDefaultValue => DefaultValue != null;

    }
}
