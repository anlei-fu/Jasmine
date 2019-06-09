using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Extensions
{
 public static   class StringBuilderExtension
    {
        public static StringBuilder RemoveLastComa(this StringBuilder builder)
        {
            if (builder[builder.Length - 1] == ',')
                builder.Remove(builder.Length - 1, 1);

            return builder;
        }
    }
}
