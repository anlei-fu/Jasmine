using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Orm.Exceptions
{
   public class AssociateConditionCanNotBeNullException:Exception
    {
        public AssociateConditionCanNotBeNullException():base($" associate  condition can not be null!")
        {

        }
    }
}
