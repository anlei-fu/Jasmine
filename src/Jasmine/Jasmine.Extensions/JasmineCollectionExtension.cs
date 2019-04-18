using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Extensions
{
   public static class JasmineCollectionExtension
    {
        public static bool RemmoveIfExists<T>(this HashSet<T> set,T key)
        {
 
             return   set.Remove(key);
        }
    }
}
