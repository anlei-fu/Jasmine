using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest.Grammer
{
   public static class OperatorConstraintExtension
    {
        public static bool Equles(this OperatorConstraint constraint,JType type)
        {
            return true;
        }
    }
}
