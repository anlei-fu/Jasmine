using Jasmine.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Server
{
    public class TrueValidator : ILoginValidator
    {
        public bool Validate(string name, string password)
        {
            return true;
        }
    }
}
