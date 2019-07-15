using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class AopServiceMetaData:ServiceMetaDataBase,IAop
    {
        public List<Type> BeforeInterceptors { get; set; } = new List<Type>();
        public List<Type> AfterInterceptors { get; set; } = new List<Type>();
        public List<Type> AroundInterceptors { get; set; } = new List<Type>();
        public List<Type> ErrorInterceptors { get; set; } = new List<Type>();
    }
}
