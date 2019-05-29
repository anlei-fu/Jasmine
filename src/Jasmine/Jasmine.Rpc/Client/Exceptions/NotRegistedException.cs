using System;

namespace Jasmine.Rpc.Client.Exceptions
{
    public  class NotRegistedException:Exception
    {
        public NotRegistedException(string msg):base(msg)
        {

        }
    }
}
