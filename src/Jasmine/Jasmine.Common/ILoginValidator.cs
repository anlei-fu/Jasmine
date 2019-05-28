using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common
{
 public   interface ILoginValidator
    {
        bool Validate(string name, string password);
    }
}
