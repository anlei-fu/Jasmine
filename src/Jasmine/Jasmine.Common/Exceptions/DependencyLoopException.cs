using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common.Exceptions
{
 public   class DependencyLoopException:Exception
    {
        public IEnumerable<Type> Types { get; }

        public DependencyLoopException(IEnumerable<Type> types)
        {
            Types = types;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var item in Types)
            {
                sb.Append(item.Name + "->");
            }

            return sb.ToString();
        }
    }
}
