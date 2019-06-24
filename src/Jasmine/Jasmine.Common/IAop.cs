using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAop
    {
      List<Type> BeforeFilters { get;  }
       List<Type> AfterFilters { get;  }
        List<Type> AroundFilters { get;  }
       List<Type> ErrorFilters { get;  }
    }
}
