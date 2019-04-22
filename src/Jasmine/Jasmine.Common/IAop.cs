using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common
{
  public  interface IAop
    {
      IList<string> BeforeFilters { get;  }
       IList<string> AfterFilters { get;  }
        IList<string> AroundFilters { get;  }
       IList<string> ErrorFilters { get;  }
    }
}
