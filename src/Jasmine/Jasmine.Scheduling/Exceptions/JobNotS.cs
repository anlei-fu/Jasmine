using System;

namespace Jasmine.Scheduling.Exceptions
{
    public class JobNotScheduledException:Exception
    {
        public JobNotScheduledException(string msg):base(msg)
        {

        }
    }
}
