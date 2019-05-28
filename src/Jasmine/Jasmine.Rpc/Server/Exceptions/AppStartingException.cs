using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Server.Exceptions
{
  public  class AppStartingException:Exception
    {
        public AppStartingException(Exception ex):base(ex.ToString())
        {

        }
    }
}
