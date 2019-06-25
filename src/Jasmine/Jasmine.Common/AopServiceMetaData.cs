using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class AopServiceMetaData:ServiceMetaDataBase
    {
        public IList<Type> BeforeInterceptors { get; set; } = new List<Type>();
        public IList<Type> AfterInterceptors { get; set; } = new List<Type>();
        public IList<Type> AroundInterceptors { get; set; } = new List<Type>();
        public IList<Type> ErrorInterceptors { get; set; } = new List<Type>();
    }
}
