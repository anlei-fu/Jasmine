using System;

namespace Jasmine.Common
{
    public class ParameterMetaDataBase : INameFearture,ITypeFearture
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public bool IsAbstract => RelatedType.IsAbstract || RelatedType.IsInterface;
        public bool NotNull { get; set; } = false;
        public Type ImplType { get; set; }
        public bool HasImpl => ImplType != null;
        public int Index { get; set; }
        public object DefaultValue { get; set; }
        public bool HasDefaultValue => DefaultValue != null;

    }
}
