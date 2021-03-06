﻿using Jasmine.Common;
using Jasmine.Reflection;
using System;

namespace Jasmine.Ioc
{
    public class IocServiceMetaData:ServiceMetaDataBase
    {

        public IocServiceMetaData(Type serviceType)
        {
            RelatedType = serviceType;
        }
     
        public ServiceScope Scope { get; internal set; }
        public string TypeName => RelatedType.Name;
        public string TypeFullName => RelatedType.FullName;
        public bool LazyLoad { get; internal set; }
        public IocMethodMetaData InitMethod { get;internal set; }

        public bool HasInitMethod => InitMethod != null;

        public bool HasDestroyMethod => DestroyMethod != null;

        public IocMethodMetaData DestroyMethod { get; internal set; }
        public IocConstructorMetaData ConstrctorMetaData { get; internal set; }
        public IocPropertyMetaData[] Properties { get; set; }
      
    }
}
