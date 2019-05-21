using System;

namespace Jasmine.Common
{
    public class ServiceMetaDataBase : INameFearture, ITypeFearture
    {
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public bool IsAbstract => RelatedType.IsInterface || RelatedType.IsAbstract;
      
    }

        
}
