using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAop
    {
      List<Type> BeforeInterceptors { get;  }
       List<Type> AfterInterceptors { get;  }
        List<Type> AroundInterceptors { get;  }
       List<Type> ErrorInterceptors { get;  }
    }
}
