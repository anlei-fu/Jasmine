using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Client.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string msg) : base(msg)
        {

        }
    }
}
