using GrammerTest.Grammer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Spider.Grammer
{
   public class InvalidNameException:GrammerException
    {
        public InvalidNameException(string msg):base(msg)
        {

        }
    }
}
