using Jasmine.Ioc.Exceptions;
using System;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    /// <summary>
    /// use to check dependency loop throught construting
    /// </summary>
    public class DependencyCheckNode
    {
        public DependencyCheckNode(DependencyCheckNode parent,Type type)
        {
            var set = new HashSet<Type>();
            var b = parent;

            while (b!=null)
            {
                if (set.Contains(b.Type))
                    throw new DependencyLoopException(set);

                set.Add(b.Type);
                b = b.Parent;
            }

        }
        public DependencyCheckNode Parent { get; }
        public Type Type { get; }



    }
}
