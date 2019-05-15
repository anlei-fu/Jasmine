using Jasmine.Common;
using System;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public class JNumberPool : AbstractSimpleQueuedPool<JNumber>
    {
        public JNumberPool(int capacity) : base(capacity)
        {
        }

        protected override JNumber newInstance()
        {
            throw new NotImplementedException();
        }
    }
}
