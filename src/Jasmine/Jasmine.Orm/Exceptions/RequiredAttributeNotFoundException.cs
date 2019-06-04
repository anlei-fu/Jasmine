using System;

namespace Jasmine.Orm.Exceptions
{
    public  class RequiredAttributeNotFoundException:Exception
    {
        public RequiredAttributeNotFoundException(string msg):base(msg)
        {

        }
    }
}
