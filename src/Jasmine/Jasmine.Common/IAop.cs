using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IAop
    {
      IList<Type> BeforeFilters { get;  }
       IList<Type> AfterFilters { get;  }
        IList<Type> AroundFilters { get;  }
       IList<Type> ErrorFilters { get;  }
    }
}
