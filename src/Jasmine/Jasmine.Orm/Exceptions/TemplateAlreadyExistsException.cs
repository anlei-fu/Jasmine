using System;

namespace Jasmine.Orm.Exceptions
{
    public  class TemplateAlreadyExistsException:Exception
    {
        public TemplateAlreadyExistsException(string msg):base(msg)
        {

        }
    }
}
