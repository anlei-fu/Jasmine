using Jasmine.Common;
using System;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public class JBoolPool : AbstractSimpleQueuedPool<JBool>
    {
        public JBoolPool(int capacity) : base(capacity)
        {
        }

        protected override JBool createNew()
        {
            throw new NotImplementedException();
        }
    }
}
