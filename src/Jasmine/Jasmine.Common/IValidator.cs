using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common
{
  public  interface IValidator
    {
        bool Validate(string usr, string psd);
    }
}
